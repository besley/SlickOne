
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SlickOne.WebUtility.Security
{
    /// <summary>
    /// 经过授权的用户对象
    /// </summary>
    public class AuthorizedUser
    {
        /// <summary>
        /// 获取当前会话
        /// </summary>
        /// <returns>会话</returns>
        private ISession GetSession()
        {
            var session = HttpContextHelper.GetCurrentSession();
            return session;
        }

        /// <summary>
        /// 用户权限列表
        /// </summary>
        public UserAuthModel[] UserAuthList
        {
            get
            {
                var session = GetSession();
                var auths = session.GetString("USER_AUTHORITIES");
                var authModels = JsonConvert.DeserializeObject<UserAuthModel[]>(auths);

                return authModels;
            }
        }

        /// <summary>
        /// 登录用户票据
        /// </summary>
        public string UserLoginTicket
        {
            get
            {
                var session = GetSession();
                var logonTicket = session.GetString("USER_LOGON_TICKET");
                if (!string.IsNullOrEmpty(logonTicket))
                    return logonTicket;
                else
                {
                    var httpContext = HttpContextHelper.GetCurrentContext();
                    var cookie = httpContext.Request.Cookies["USER_LOGON_TICKET"];
                    if (cookie != null)
                    {
                        //Cookie未过期时，读取cookie，重新写Session
                        session.SetString("USER_LOGON_TICKET", cookie.ToString());
                        return cookie.ToString();
                    }
                    else
                    {
                        //Session, Cookie都过期，重新登录
                        return string.Empty;
                    }
                }
            }
        }

        private AuthorizedUser()
        {
        }

        public static AuthorizedUser Current
        {
            get
            {
                return new AuthorizedUser();
            }
        }
    }
}