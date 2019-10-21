using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.MessageImpl.Entity
{
    /// <summary>
    /// 消息查询实体
    /// </summary>
    public class MessageQuery : QueryBase
    {
        public int SenderID { get; set; }
        public string Sender { get; set; }
        public int RecieverID { get; set; }
        public string Reciever { get; set; }
        public MsgTypeEnum MsgType { get; set; }
        public MsgStatusEnum Status { get; set; }
    }
}
