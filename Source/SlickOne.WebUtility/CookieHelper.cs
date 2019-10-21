using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// Cookie帮助类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="response">http响应</param>
        /// <param name="userObject">用户对象</param>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="days">天数</param>
        public static void AddCookie(HttpResponse response, object userObject, string cookieName, int days)
        {
            var json = JsonConvert.SerializeObject(userObject);
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(days);

            response.Cookies.Append(cookieName, json, cookieOptions);
        }

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="cookieName">Cookie名称</param>
        public static void RemoveCookie(HttpContext context,  string cookieName)
        {
            if (context.Request.Cookies[cookieName] != null)
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies.Delete(cookieName, cookieOptions);
            }
        }
    }
}
