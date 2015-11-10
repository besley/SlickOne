using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// 成功登录后，保存在客户端的信息封装
    /// </summary>
    public class LogonResult
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Ticket { get; set; }
        public string ReturnUrl { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string WebApiUrl { get; set; }
    }
}
