using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.AuthImpl.Entity
{
    /// <summary>
    /// 角色资源权限视图
    /// </summary>
    public class RoleResourcePermissionView 
    {
        public Int32 ID { get; set; }
        public Int32 RoleID { get; set; }
        public Int32 ResourceTypeID { get; set; }
        public Int32 ParentID { get; set; }
        public String ResourceCode { get; set; }
        public String ResourceName { get; set; }
        public bool PermissionAllow { get; set; }
        public bool PermissionDeny { get; set; }
    }
}
