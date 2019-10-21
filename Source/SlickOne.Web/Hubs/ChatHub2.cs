using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;
using SlickOne.Web.Models;
using SlickOne.Module.MessageImpl.Entity;

namespace SlickOne.Web.Hubs
{
    /// <summary>
    /// 用户在线状态
    /// </summary>
    public enum StatusEnum
    {
        Offline = 0,
        Online = 1
    }

    /// <summary>
    /// 聊天Hub对象
    /// </summary>
    //[HubName("ChatHub2")]
    public class ChatHub2 : Hub
    {
        //private static Timer _timer;

        static ChatHub2()
        {
        }

        #region 静态属性及方法
        /// <summary>
        /// 用户连接列表维护，key：UserName，Value：User
        /// </summary>
        public static readonly ConcurrentDictionary<string, IMUser> Users
            = new ConcurrentDictionary<string, IMUser>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// 获取当前在线用户的列表
        /// </summary>
        /// <returns></returns>
        public static List<IMUser> GetOnlineUsersOnHub()
        {
            var clients = Users.Select(x => x.Value).ToList();
            return clients;
        }

        /// <summary>
        /// 把登录用户的名称信息添加到在线用户列表
        /// </summary>
        /// <param name="user"></param>
        public static void RegisterNewOnlineUser(IMUser user)
        {
            //先删除该用户名称下的所有ConnectionIds
            IMUser removedUser;
            Users.TryRemove(user.UserName, out removedUser);

            //添加新连接用户
            var newOnlineUser = new IMUser();
            newOnlineUser.UserID = user.UserID;
            newOnlineUser.UserName = user.UserName;
            newOnlineUser.ConnectionID = user.ConnectionID;

            Users.GetOrAdd(newOnlineUser.UserName, newOnlineUser);
        }
        #endregion

        #region 用户连接处理方法
        /// <summary>
        /// 当用户连接，维护connectionid字典
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            //通知客户端，用户已经连接，视为在线用户
            string connectionId = Context.ConnectionId;
            //Clients.Client(connectionId).onUserConntected(connectionId);

            return base.OnConnectedAsync();
        }

        ///// <summary>
        ///// 当用户退出连接，从在线用户列表中删除用户
        ///// </summary>
        ///<param name="exception"></param>
        ///// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            List<string> items = Users
                .Where(d => d.Value.ConnectionID == connectionId)
                .Select(x => x.Key)
                .ToList();

            IMUser removedUser;
            foreach (var item in items)
            {
                Users.TryRemove(item, out removedUser);
            }
            return base.OnDisconnectedAsync(exception);
        }


        /// <summary>
        /// 由用户名称获取用户对象
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private IMUser GetUserByConnectionId(string connectionId)
        {
            List<string> items = Users
                .Where(d => d.Value.ConnectionID == connectionId)
                .Select(x => x.Key)
                .ToList();

            var item = items[0];

            IMUser user;
            Users.TryGetValue(item, out user);  //item : =username

            return user;
        }

        private IMUser GetUserByName(string userName)
        {
            IMUser user;
            Users.TryGetValue(userName, out user);

            return user;
        }
        #endregion
    }
}