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
        /// <param name="session">会话</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Save(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 保存登录用户对象
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="value">值</param>
        public static void SaveLogonUser(ISession session, object value)
        {
            Save(session, WEB_LOGON_USER, value);
        }

        /// <summary>
        /// 保存登录用户ID
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="userId">用户ID</param>
        public static void SaveLogonUserID(ISession session, int userId)
        {
            Save(session, WEB_LOGON_USER_ID, userId);
        }

        /// <summary>
        /// 保存登录用户票据
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="ticket">票据</param>
        public static void SaveLogonUserTicket(ISession session, string ticket)
        {
            Save(session, WEB_LOGON_USER_TICKET, ticket);
        }

        /// <summary>
        /// 保存用户账户类型
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="accountType">账号类型</param>
        public static void SaveLogonUserAccountType(ISession session, string accountType)
        {
            Save(session, WEB_LOGON_USER_ACCOUNT_TYPE, accountType);
        }

        /// <summary>
        /// 保存登录用户Session的GUID
        /// </summary>
        /// <param name="session">会话</param>
        public static void SaveLogonSessionGUID(ISession session)
        {
            Save(session, WEB_LOGON_SESSION_GUID, Guid.NewGuid());
        }

        /// <summary>
        /// 保存登录前图片验证字符串
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="text">文本</param>
        public static void SaveLogonImageText(ISession session, string text)
        {
            Save(session, WEB_LOGIN_IMAGE_TEXT, text);
        }

        /// <summary>
        /// 取出Session
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="key">键</param>
        /// <returns>字符串</returns>
        public static string Get(ISession session, string key)
        {
            return session.GetString(key);
        }

        /// <summary>
        /// 取出Session
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="session">会话</param>
        /// <param name="key">键</param>
        /// <returns>类型</returns>
        public static T Get<T>(ISession session, string key) where T:class
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 获取登录用户对象
        /// </summary>
        /// <param name="session">会话</param>
        /// <returns>对象</returns>
        public static object GetLogonUser(ISession session)
        {
            return Get(session, WEB_LOGON_USER);
        }

        /// <summary>
        /// 获取登录用户ID
        /// </summary>
        /// <param name="session">会话</param>
        /// <returns>用户ID</returns>
        public static int GetLogonUserID(ISession session)
        {
            int userID = 0;
            var strUserID = Get(session, WEB_LOGON_USER_ID);
            int.TryParse(strUserID, out userID);
            return userID;
        }

        /// <summary>
        /// 获取登录用户Session的GUID
        /// </summary>
        /// <param name="session">会话</param>
        /// <returns>会话GUID</returns>
        public static string GetLogonUserSessionGUID(ISession session)
        {
            return Get(session, WEB_LOGON_SESSION_GUID).ToString();
        }

        /// <summary>
        /// 获取登录用户票据
        /// </summary>
        /// <param name="session">会话</param>
        /// <returns>票据</returns>
        public static string GetLogonUserTicket(ISession session)
        {
            var obj = Get(session, WEB_LOGON_USER_TICKET);
            var ticket = obj != null ? obj.ToString() : string.Empty;
            return ticket;
        }

        /// <summary>
        /// 获取登录用户账户类型
        /// </summary>
        /// <param name="session">会话</param>
        /// <returns>账号类型</returns>
        public static string GetLogonUserAccountType(ISession session)
        {
            var obj = Get(session, WEB_LOGON_USER_ACCOUNT_TYPE);
            var accountType = obj != null ? obj.ToString() : string.Empty;
            return accountType;
        }

        /// <summary>
        /// 获取登录前的图片字符串
        /// </summary>
        /// <param name="session">会话</param>
        /// <returns>文本</returns>
        public static string GetLogonImageText(ISession session)
        {
            return Get(session, WEB_LOGIN_IMAGE_TEXT).ToString();
        }
    }
}
