using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.MessageImpl.Entity
{
    /// <summary>
    /// 用于即时消息的用户对象
    /// </summary>
    public class IMUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ConnectionID { get; set; }
    }
}
