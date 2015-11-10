using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace SlickOne.WebUtility
{
    public class CookieHelper
    {
        public static void AddCookie(HttpResponseBase response, object userObject, string cookieName, int days)
        {
            var json = JsonConvert.SerializeObject(userObject);
            var userCookie = new HttpCookie(cookieName, json);

            userCookie.Expires.AddDays(days);
            response.Cookies.Add(userCookie);
        }

        public static void RemoveCookie(HttpContextBase context,  string cookieName)
        {
            if (context.Request.Cookies[cookieName] != null)
            {
                var user = new HttpCookie(cookieName)
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };
                context.Response.Cookies.Add(user);
            }
        }
    }
}
