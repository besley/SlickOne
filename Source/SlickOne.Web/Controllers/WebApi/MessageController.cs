using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.SignalR;
using SlickOne.WebUtility;
using SlickOne.Module.MessageImpl.Entity;
using SlickOne.Module.MessageImpl.Service;

namespace SlickOne.Web.Controllers.WebApi
{
    /// <summary>
    /// 消息数据控制器
    /// </summary>
    public class MessageController : Controller
    {
        #region 消息服务
        private IMessageService _service;
        protected IMessageService MessageService
        {
            get
            {
                if (_service == null)
                {
                    _service = new MessageService();
                }
                return _service;
            }
        }
        #endregion

        [HttpGet]
        public ResponseResult<MessageEntity> Get(int id)
        {
            var result = ResponseResult<MessageEntity>.Default();
            try
            {
                var msg = MessageService.Get(id);
                result = ResponseResult<MessageEntity>.Success(msg);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<MessageEntity>.Error(
                    string.Format("读取消息记录失败，错误描述:{0}", ex.Message)
                );
            }
            return result;
        }

        [HttpPost]
        public ResponseResult<List<MessageEntity>> GetPagedList(MessageQuery query)
        {
            var count = 0;
            var result = ResponseResult<List<MessageEntity>>.Default();
            try
            {
                var list = MessageService.GetListPaged(query, out count);
                result = ResponseResult<List<MessageEntity>>.Success(list);
                result.TotalRowsCount = count;
                result.TotalPages = (count + query.PageSize - 1) / query.PageSize;
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<MessageEntity>>.Error(
                    string.Format("获取消息数据失败:{0}", ex.Message)
                );
            }
            return result;
        }

        [HttpPost]
        public ResponseResult SetRead(string msgGUID)
        {
            var result = ResponseResult.Default();
            try
            {
                MessageService.SetRead(msgGUID);
                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(string.Format("设置消息已读状态失败，错误描述:{0}", ex.Message));
            }
            return result;
        }
    }
}
