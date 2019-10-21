using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.MessageImpl.Entity
{
    /// <summary>
    /// 消息状态
    /// </summary>
    public enum MsgStatusEnum
    {
        /// <summary>
        /// 消息还未读取
        /// </summary>
        Unread = 1,

        /// <summary>
        /// 消息已经读取
        /// </summary>
        Read = 2
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgTypeEnum
    {
        /// <summary>
        /// 通知类消息
        /// </summary>
        Notification = 1,

        /// <summary>
        /// 对聊
        /// </summary>
        Peer = 2,

        /// <summary>
        /// 群消息
        /// </summary>
        Group = 3,

        /// <summary>
        /// 广播
        /// </summary>
        Broadcast = 4
    }
}
