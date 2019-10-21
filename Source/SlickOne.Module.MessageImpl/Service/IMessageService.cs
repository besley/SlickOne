using System;
using System.Collections.Generic;
using System.Linq;
using SlickOne.Data;
using SlickOne.Module.MessageImpl.Entity;

namespace SlickOne.Module.MessageImpl.Service
{
    /// <summary>
    /// IMessageInterface 接口
    /// </summary>
	public partial interface IMessageService
	{
        MessageEntity Get(int id);
        MessageEntity GetByGUID(string msgGUID);
        int Insert(MessageEntity entity);
        void BulkInsert(List<MessageNotification> msgList);
        void SetRead(string msgGUID);
        List<MessageEntity> GetListPaged(MessageQuery query, out int count);
        List<MessageEntity> GetMessageByMsgGUID(string msgGUID);
        List<MessageEntity> QueryMessage(Pager page);
        int GetCountByRecieverID(int ID);
	}
}