using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.MessageImpl.Entity
{
    /// <summary>
    /// 消息接收人对象
    /// </summary>
    public class RecieverEntity
    {
        public int RecieverID { get; set; }
        public string Reciever { get; set; }
    }

    /// <summary>
    /// 消息通知对象
    /// </summary>
    public class MessageNotification : MessageEntity
    {
        public List<RecieverEntity> Recievers { get; set; }
    }
}
