using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// Session 管理器
    /// </summary>
    public class SessionManager
    {
        private const string WEB_LOGON_USER = "IVWEB_LOGON_USER";
        private const string WEB_LOGON_USER_ID = "IVWEB_LOGON_USER_ID";
        private const string WEB_LOGON_SESSION_GUID = "IVWEB_LOGON_SESSION_GUID";
        private const string WEB_LOGIN_IMAGE_TEXT = "IVWEB_LOGIN_IMAGE_TEXT";
        private const string WEB_LOGON_USER_TICKET = "IVWEB_LOGON_USER_TICKET";
        private const string WEB_LOGON_USER_ACCOUNT_TYPE = "IVWEB_LOGON_USER_ACCOUNT_TYPE";

        /// <summary>
        /// 写入session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Save(HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }

        /// <summary>
        /// 保存登录用户对象
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public static void SaveLogonUser(HttpSessionStateBase session, object value)
        {
            Save(session, WEB_LOGON_USER, value);
        }

        /// <summary>
        /// 保存登录用户ID
        /// </summary>
        /// <param name="session"></param>
        /// <param name="userId"></param>
        public static void SaveLogonUserID(HttpSessionStateBase session, int userId)
        {
            Save(session, WEB_LOGON_USER_ID, userId);
        }

        /// <summary>
        /// 保存登录用户票据
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ticket"></param>
        public static void SaveLogonUserTicket(HttpSessionStateBase session, string ticket)
        {
            Save(session, WEB_LOGON_USER_TICKET, ticket);
        }

        /// <summary>
        /// 保存用户账户类型
        /// </summary>
        /// <param name="session"></param>
        /// <param name="accountType"></param>
        public static void SaveLogonUserAccountType(HttpSessionStateBase session, string accountType)
        {
            Save(session, WEB_LOGON_USER_ACCOUNT_TYPE, accountType);
        }

        /// <summary>
        /// 保存登录用户Session的GUID
        /// </summary>
        /// <param name="session"></param>
        public static void SaveLogonSessionGUID(HttpSessionStateBase session)
        {
            Save(session, WEB_LOGON_SESSION_GUID, Guid.NewGuid());
        }

        /// <summary>
        /// 保存登录前图片验证字符串
        /// </summary>
        /// <param name="session"></param>
        /// <param name="text"></param>
        public static void SaveLogonImageText(HttpSessionStateBase session, string text)
        {
            Save(session, WEB_LOGIN_IMAGE_TEXT, text);
        }

        /// <summary>
        /// 取出Session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(HttpSessionStateBase session, string key)
        {
            return session[key];
        }

        /// <summary>
        /// 获取登录用户对象
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static object GetLogonUser(HttpSessionStateBase session)
        {
            return Get(session, WEB_LOGON_USER);
        }

        /// <summary>
        /// 获取登录用户ID
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int GetLogonUserID(HttpSessionStateBase session)
        {
            return (int)Get(session, WEB_LOGON_USER_ID);
        }

        /// <summary>
        /// 获取登录用户Session的GUID
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLogonUserSessionGUID(HttpSessionStateBase session)
        {
            return Get(session, WEB_LOGON_SESSION_GUID).ToString();
        }

        /// <summary>
        /// 获取登录用户票据
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLogonUserTicket(HttpSessionStateBase session)
        {
            var obj = Get(session, WEB_LOGON_USER_TICKET);
            var ticket = obj != null ? obj.ToString() : string.Empty;
            return ticket;
        }

        /// <summary>
        /// 获取登录用户账户类型
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLogonUserAccountType(HttpSessionStateBase session)
        {
            var obj = Get(session, WEB_LOGON_USER_ACCOUNT_TYPE);
            var accountType = obj != null ? obj.ToString() : string.Empty;
            return accountType;
        }

        /// <summary>
        /// 获取登录前的图片字符串
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLogonImageText(HttpSessionStateBase session)
        {
            return Get(session, WEB_LOGIN_IMAGE_TEXT).ToString();
        }
    }
}
