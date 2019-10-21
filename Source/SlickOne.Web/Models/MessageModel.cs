using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.SignalR;
using SlickOne.Module.MessageImpl.Entity;
using SlickOne.Module.MessageImpl.Service;
using SlickOne.Web.Hubs;

namespace SlickOne.Web.Models
{
    /// <summary>
    /// 消息模型
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 登录用户的消息拉取服务
        /// </summary>
        /// <param name="user"></param>
        public void PullMessageList(IMUser user)
        {
            var msgService = new MessageService();
            var msgQuery = new MessageQuery
            {
                PageIndex = 0,
                PageSize = 100,
                Reciever = user.UserName,
                RecieverID = user.UserID,
                Status = MsgStatusEnum.Unread
            };

            List<MessageEntity> unreadMsgList = null;
            //var hub = GlobalHost.ConnectionManager.GetHubContext<ClientPushHub>();
            //hub.Clients.Client(user.ConnectionID).onPullingMessage(unreadMsgList);
        }

        /// <summary>
        /// 插入消息记录
        /// </summary>
        /// <param name="msg"></param>
        public void InsertMessage(MessageEntity msg)
        {
            var msgService = new MessageService();
            msgService.Insert(msg);
        }

        #region 消息推送服务
        /// <summary>
        /// 通知单条消息
        /// </summary>
        /// <param name="msg"></param>
        public void Notify(MessageNotification msg)
        {
            List<MessageNotification> msgList = new List<MessageNotification>();
            msgList.Add(msg);
            BatchNotify(msgList);
        }

        /// <summary>
        /// 多条消息列表推送
        /// </summary>
        /// <param name="msgList"></param>
        public void BatchNotify(List<MessageNotification> msgList)
        {
            //批量插入消息
            var msgService = new MessageService();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    msgService.BulkInsert(msgList);
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            });

            //var hub = GlobalHost.ConnectionManager.GetHubContext<ClientPushHub>();
            //var onlineUsers = ChatHub2.GetOnlineUsersOnHub();

            ////发送消息
            //Parallel.ForEach<MessageNotification>(msgList, (msg) =>
            //{
            //    PushMessage(msg, onlineUsers, hub);
            //});
        }

        /// <summary>
        /// 单条消息推送服务
        /// </summary>
        /// <param name="message">消息数据</param>
        private void PushMessage(MessageNotification msg, List<IMUser> onlineUsers, IHubContext<Hub> hub)
        {
            IMUser user = null;
            List<RecieverEntity> recievers = msg.Recievers;
            foreach (var reciever in recievers)
            {
                user = onlineUsers.SingleOrDefault(a => a.UserName == reciever.Reciever);
                if (user != null)
                {
                    //hub.Clients.Client(user.ConnectionID).onPushingMessage(new 
                    //{
                    //    MsgType = 2,
                    //    Title = msg.Title,
                    //    Content = msg.Content,
                    //    Status = msg.Status,
                    //    MsgGUID = msg.MsgGUID,
                    //    SenderID = msg.SenderID,
                    //    Sender = msg.Sender,
                    //    SendTime = msg.SendTime,
                    //    AppName = msg.AppName,
                    //    AppInstanceID = msg.AppInstanceID,
                    //    FormRef = msg.FormRef,
                    //    FormCode = msg.FormCode,
                    //    FormText = msg.FormText
                    //});
                }
            }
        }
        #endregion
    }
}