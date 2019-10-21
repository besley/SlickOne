using System;
using System.Collections.Generic;
using System.Linq;

namespace SlickOne.Module.MessageImpl.Entity
{
    /// <summary>
    /// MessageEntity 业务实体类
    /// </summary>
    [Table("SysMessage")]
	public partial class MessageEntity
	{
		public Int32 ID { get; set; }	
		public Int32 MsgType { get; set; }
		
		public String Title { get; set; }	
		public String Content { get; set; }	
		public Byte Status { get; set; }
		
		public String MsgGUID { get; set; }	
		public Int32 SenderID { get; set; }
		
		public String Sender { get; set; }	
		public Nullable<DateTime> SendTime { get; set; }
		
		public Int32 RecieverID { get; set; }
		public String Reciever { get; set; }
        public Nullable<DateTime> RecievedTime { get; set; }
		public String AppName { get; set; }	
		public Nullable<Int32> AppInstanceID { get; set; }
        public String FormRef { get; set; }
        public String FormCode { get; set; }
        public String FormText { get; set; }
		
	}
}