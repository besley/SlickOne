using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// HttpContext 帮助类
    /// </summary>
    public class HttpContextHelper
    {
        /// <summary>
        /// 获取当前Session
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns>会话</returns>
        public static ISession GetCurrentSession()
        {
            return null;
        }

        /// <summary>
        /// 获取当前上下文
        /// </summary>
        /// <returns>上下文</returns>
        public static HttpContext GetCurrentContext()
        {
            return null;
        }
    }
}
