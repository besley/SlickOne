using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// 网络工具
    /// </summary>
    public class NetworkUtility
    {
        public static string GetIPAddress(HttpRequest request)
        {
            var remoteIpAddress = request.HttpContext.Connection.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }
    }
}
