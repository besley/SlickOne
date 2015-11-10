using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SlickOne.WebUtility.Security
{
    /// <summary>
    /// 管理员级别用户的操作权限验证
    /// </summary>
    public class AdminAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ////验证是否是登录用户
            var identity = filterContext.HttpContext.User.Identity;
            if (identity.IsAuthenticated)
            {
                var actionName = filterContext.ActionDescriptor.ActionName;
                var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                if (identity.Name.ToLower() != "ivadmin")
                {
                    //虽然是登录用户，但没有该Action的权限，跳转到“未授权访问”页面
                    filterContext.HttpContext.Response.Redirect("~/Home/UnAuthorized", true);
                }
            }
            else
            {
                filterContext.HttpContext.Response.Redirect("~/Account/Login", true);
            }
        }
    }
}
