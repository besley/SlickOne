using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.SignalR;
using SlickOne.WebUtility;
using SlickOne.Module.MessageImpl.Entity;
using SlickOne.Web.Models;
using SlickOne.Web.Hubs;

namespace SlickOne.Web.Controllers.WebApi
{
    /// <summary>
    /// 用户状态管理控制器
    /// </summary>
    public class IMUserController : Controller
    {
        /// <summary>
        /// 获取在线用户数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<IMUser>> GetOnlineUsers()
        {
            var result = ResponseResult<List<IMUser>>.Default();
            try
            {
                List<IMUser> onlineUsers = ChatHub2.GetOnlineUsersOnHub();
                result = ResponseResult<List<IMUser>>.Success(onlineUsers);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<IMUser>>.Error(
                    string.Format("获取在线用户列表失败：{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 注册用户连接
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public ResponseResult RegisterUserConnection(IMUser user)
        {
            var result = ResponseResult.Default();
            try
            {
                //注册为新在线用户
                ChatHub2.RegisterNewOnlineUser(user);

                //获取该用户的消息列表
                var msgModel = new MessageModel();
                msgModel.PullMessageList(user);

                result = ResponseResult.Success();
            }
            catch(System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("注册新用户失败:{0}", ex.Message)
                );
            }
            return result;
        }
    }
}
