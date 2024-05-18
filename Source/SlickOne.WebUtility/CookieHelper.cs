using System;
using Microsoft.AspNetCore.Http;

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
        /// <param name="value">用户对象</param>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="days">天数</param>
        public static void AddCookie(HttpResponse response, string cookieName, string value, int days)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(days);

            response.Cookies.Append(cookieName, value, cookieOptions);
        }

        /// <summary>
        /// 读取Cookie
        /// </summary>
        /// <param name="request">Http请求</param>
        /// <param name="cookieName">Cookie名称</param>
        /// <returns>Cookie值</returns>
        public static string GetCookie(HttpRequest request, string cookieName)
        {
            var value = request.Cookies[cookieName];
            return value;
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
