using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.AuthImpl.Entity
{
    /// <summary>
    /// 角色用户实体
    /// </summary>
    [Table("SysRoleUser")]
    public class RoleUserEntity
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}
