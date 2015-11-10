using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SlickOne.WebUtility.Security
{
    /// <summary>
    /// 权限验证属性类
    /// </summary>
    public class RequireAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 用户权限列表
        /// </summary>
        public UserAuthModel[] UserAuthList
        {
            get
            {
                return AuthorizedUser.Current.UserAuthList;
            }
        }

        /// <summary>
        /// 登录用户票据
        /// </summary>
        public string UserLoginTicket
        {
            get
            {
                return AuthorizedUser.Current.UserLoginTicket;
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            ////验证是否是登录用户
            var identity = filterContext.HttpContext.User.Identity;
            if (identity.IsAuthenticated)
            {
                var actionName = filterContext.ActionDescriptor.ActionName;
                var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                //验证用户操作是否在权限列表中
                if (HasActionQulification(actionName, controllerName, identity.Name))
                    if (string.IsNullOrEmpty(UserLoginTicket))
                        //用户的Session, Cookie都过期，需要重新登录
                        filterContext.HttpContext.Response.Redirect("~/Account/Login", false);
                else
                    //虽然是登录用户，但没有该Action的权限，跳转到“未授权访问”页面
                    filterContext.HttpContext.Response.Redirect("~/Home/UnAuthorized", true);
            }
            else
            {
                //未登录用户，则判断是否是匿名访问
                var attr = filterContext.ActionDescriptor.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attr.Any(a => a is AllowAnonymousAttribute);

                if (!isAnonymous)
                    //未验证（登录）的用户, 而且是非匿名访问，则转向登录页面
                    filterContext.HttpContext.Response.Redirect("~/Account/Login", true);
            }
        }

        /// <summary>
        /// 从权限列表验证用户是否有权访问Action
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        private bool HasActionQulification(string actionName, string controllerName, string userName)
        {
            //从该用户的权限数据列表中查找是否有当前Controller和Action的item
            var auth = UserAuthList.FirstOrDefault(a =>
            {
                bool rightAction = false;
                bool rightController = a.Controller == controllerName;
                if (rightController)
                {
                    string[] actions = a.Actions.Split(',');
                    rightAction = actions.Contains(actionName);
                }
                return rightAction;
            });

            //此处可以校验用户的其它权限条件
            //var notAllowed = HasOtherLimition(userName);
            //var result = (auth != null) && notAllowed;
            //return result;

            return (auth != null);
        }
    }
}
