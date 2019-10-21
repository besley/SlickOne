using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DapperExtensions;
using SlickOne.Data;
using SlickOne.Module.MessageImpl.Entity;


namespace SlickOne.Module.MessageImpl.Service
{
    /// <summary>
    /// MessageService 数据服务
    /// </summary>
	public class MessageService : IMessageService
	{
        private Repository quickRepository = null;
        private Repository QuickRepository
        {
            get
            {
                if (quickRepository == null)
                {
                    quickRepository = new Repository();
                }
                return quickRepository;
            }
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="message"></param>
        private void Insert(DataTable dt, MessageNotification message)
        {
            DataRow dr = null;
            var recievers = message.Recievers;

            try
            {
                foreach (var r in recievers)
                {
                    dr = dt.NewRow();
                    dr["MsgType"] = message.MsgType;
                    dr["Title"] = message.Title;
                    dr["Content"] = message.Content;
                    dr["Status"] = message.Status;
                    dr["MsgGUID"] = message.MsgGUID;
                    dr["SenderID"] = message.SenderID;
                    dr["Sender"] = message.Sender;
                    dr["SendTime"] = message.SendTime;
                    dr["RecieverID"] = r.RecieverID;
                    dr["Reciever"] = r.Reciever;

                    if (message.RecievedTime != null)
                        dr["RecievedTime"] = message.RecievedTime;

                    dr["AppName"] = message.AppName;
                    dr["AppInstanceID"] = message.AppInstanceID;
                    dr["FormRef"] = message.FormRef;
                    dr["FormCode"] = message.FormCode;
                    dr["FormText"] = message.FormText;
                    dt.Rows.Add(dr);
                }
            }
            catch
            {
                ;
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="msgList"></param>
        public void BulkInsert(List<MessageNotification> msgList)
        {
            var msgTable = GetTableSchema();
            foreach (var msg in msgList)
            {
                Insert(msgTable, msg);
            }
            BulkCopyUtility.BulkCopy(msgTable, "dbo.SysMessage");
        }

        /// <summary>
        /// 根据guid置消息状态
        /// </summary>
        /// <param name="msgGUID"></param>
        /// <returns></returns>
        public void SetRead(string msgGUID)
        {
            var session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                string updSql = @"UPDATE 
                                    SysMessage 
                                SET Status = 2 
                                WHERE MsgGUID = @msgGUID 
                                    AND Status = 1";
                QuickRepository.Execute(session.Connection, updSql, 
                    new { 
                        msgGUID = msgGUID 
                    }, 
                    session.Transaction);

                session.Commit();
            }
            catch (System.Exception)
            {
                session.Rollback();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }

        /// <summary>
        /// 根据ID获取消息实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageEntity Get(int id)
        {
            return QuickRepository.GetById<MessageEntity>(id);
        }

        /// <summary>
        /// 根据消息GUID获取消息实体
        /// </summary>
        /// <param name="msgGUID"></param>
        /// <returns></returns>
        public MessageEntity GetByGUID(string msgGUID)
        {
            MessageEntity entity = null;
            var list = QuickRepository.GetByName<MessageEntity>("MsgGUID", msgGUID)
                .ToList<MessageEntity>();

            if (list != null && list.Count() == 1)
            {
                entity = list.FirstOrDefault();
            }
            return entity;
        }

        /// <summary>
        /// 获取通知消息列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<MessageEntity> GetListPaged(MessageQuery query, out int count)
        {
            var param = new DynamicParameters();
            param.Add("@recieverID", query.RecieverID);
            param.Add("@msgType", query.MsgType);
            param.Add("@status", query.Status);
            param.Add("@pageIndex", query.PageIndex);
            param.Add("@pageSize", query.PageSize);
            param.Add("@field", query.Field);
            param.Add("@order", query.Order);
            param.Add("@rowsCount", null, DbType.Int32, ParameterDirection.Output);

            List<MessageEntity> msgList = null;
            try
            {
                msgList = QuickRepository.ExecProcQuery<MessageEntity>( 
                    "pr_sys_MessageListPagedQuery", 
                    param).ToList<MessageEntity>();

                count = param.Get<int>("@rowsCount");

                return msgList;
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 获取数据表Schema
        /// </summary>
        /// <returns></returns>
        private DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("ID", typeof(Int32)),
                new DataColumn("MsgType", typeof(Int32)),
                new DataColumn("Title", typeof(string)),
                new DataColumn("Content", typeof(string)),
                new DataColumn("Status", typeof(byte)),
                new DataColumn("MsgGUID", typeof(string)),
                new DataColumn("SenderID", typeof(Int32)),
                new DataColumn("Sender", typeof(string)),
                new DataColumn("SendTime", typeof(DateTime)),
                new DataColumn("RecieverID", typeof(Int32)),
                new DataColumn("Reciever", typeof(string)),
                new DataColumn("RecievedTime", typeof(DateTime)),
                new DataColumn("AppName", typeof(string)),
                new DataColumn("AppInstanceID", typeof(string)),
                new DataColumn("FormRef", typeof(string)),
                new DataColumn("FormCode", typeof(string)),
                new DataColumn("FormText", typeof(string))
            });
            return dt;
        }

        //根据消息唯一标识查询消息
        public List<MessageEntity> GetMessageByMsgGUID(string msgGUID)
        {
            List<MessageEntity> list = new List<MessageEntity>();
            var sql = "SELECT * FROM SysMessage WHERE MsgGUID=@msgGUID";
            try
            {
                list = QuickRepository.Query<MessageEntity>(sql, new { msgGUID = msgGUID }).ToList<MessageEntity>();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //分页查询消息
        public List<MessageEntity> QueryMessage(Pager page)
        {
            int startPage=(page.PageIndex-1)*page.PageSize-1;
            int endPage = page.PageIndex * page.PageSize;
            int recieverID = Convert.ToInt32(page.StrWhere);
            List<MessageEntity> list = new List<MessageEntity>();
            var sql = @"SELECT TEMP.ID,TEMP.MsgType,TEMP.Title,TEMP.Content,
                        TEMP.Status,TEMP.MsgGUID,TEMP.SenderID,TEMP.Sender,TEMP.SendTime,
                       TEMP.RecieverID,TEMP.Reciever,TEMP.RecievedTime,TEMP.AppName,
                       TEMP.AppInstanceID,TEMP.FormRef,TEMP.FormCode,TEMP.FormText FROM
                         (
                         select
                         ROW_NUMBER() OVER(ORDER BY Status asc,SendTime desc ) rowNum,*
                         from SysMessage
                         WHERE RecieverID=@recieverID
                        )AS TEMP
                         WHERE TEMP.rowNum BETWEEN @startPage AND @endPage";
            try
            {
                list = QuickRepository.Query<MessageEntity>(sql, new { startPage = startPage, endPage = endPage, recieverID = recieverID }).ToList<MessageEntity>();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //根据接收人ID获取数据条数
        public int GetCountByRecieverID(int ID)
        {
            var sql = "SELECT COUNT(*) FROM SysMessage WHERE RecieverID=" + ID + "";
            try
            {
                using (var conn = SessionFactory.CreateConnection())
                {
                    int result = QuickRepository.Count<MessageEntity>(conn, sql);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 消息实体插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(MessageEntity entity)
        {
            return QuickRepository.Insert<MessageEntity>(entity);
        }
    }
}