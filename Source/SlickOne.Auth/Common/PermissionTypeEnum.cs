using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatOne.Auth.Common
{
    /// <summary>
    /// 资源权限许可类型
    /// </summary>
    public enum PermissionTypeEnum
    {
        /// <summary>
        /// 允许
        /// </summary>
        Allow = 1,

        /// <summary>
        /// 拒绝
        /// </summary>
        Deny = 2
    }
}
