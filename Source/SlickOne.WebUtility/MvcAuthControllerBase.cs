using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// 权限验证控制器基类
    /// </summary>
    [Authorize]
    public abstract class MvcAuthControllerBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //未登录用户，则判断是否是匿名访问
            var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var actionAttributes = actionDescriptor.MethodInfo.GetCustomAttributes(inherit: true);
            bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousFilter);

            if (!isAnonymous)
            {
                var claim = this.User;
                var user = claim.Identity;

                if (user == null)
                {
                    //未验证（登录）的用户, 而且是非匿名访问，则转向登录页面
                    context.HttpContext.Response.Redirect("Account/Login", true);
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
