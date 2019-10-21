using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SlickOne.WebUtility;
using SlickOne.Web.Models;
using SlickOne.Module.MessageImpl.Entity;

namespace SlickOne.Web.Controllers.WebApi
{
    /// <summary>
    /// 消息通知控制器
    /// </summary>
    public class NotificationController : Controller
    {
        /// <summary>
        /// 单条消息通知
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult Notify(MessageNotification message)
        {
            var result = ResponseResult.Default();
            try
            {
                var msgModel = new MessageModel();
                msgModel.Notify(message);
                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("通知消息推送失败，错误描述:{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 多条消息通知
        /// </summary>
        /// <param name="msgList"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult BulkNotify(List<MessageNotification> msgList)
        {
            var result = ResponseResult.Default();
            try
            {
                var msgModel = new MessageModel();
                msgModel.BatchNotify(msgList);
                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("批量通知消息推送失败，错误描述:{0}", ex.Message)
                );
            }
            return result;
        }
    }
}
