using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatOne.Auth.Common
{
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceTypeEnum
    {
        /// <summary>
        /// 系统
        /// </summary>
        System = 1,

        /// <summary>
        /// 模块
        /// </summary>
        Module = 2,

        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 3,

        /// <summary>
        /// 表单
        /// </summary>
        Form = 4,

        /// <summary>
        /// 按钮
        /// </summary>
        Button = 5,

        /// <summary>
        /// 函数方法
        /// </summary>
        Function = 6
    }
}
