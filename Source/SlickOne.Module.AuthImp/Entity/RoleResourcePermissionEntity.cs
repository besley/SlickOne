using System;
using System.Collections.Generic;
using System.Linq;

namespace SlickOne.Module.AuthImp.Entity
{
    /// <summary>
    /// 资源权限实体
    /// </summary>
    [Table("SysRoleResourcePermission")]
	public partial class RoleResourcePermissionEntity
	{
		public Int32 ID { get; set; }	
		public Int32 RoleID { get; set; }	
		public Int32 ResourceID { get; set; }	
		public byte PermissionType { get; set; }	
	}
}