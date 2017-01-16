using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.AuthImp.Entity
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [Table("SysRole")]
    public class RoleEntity
    {
        public int ID
        {
            get;
            set;
        }

        public string RoleName
        {
            get;
            set;
        }

        public string RoleCode
        {
            get;
            set;
        }
    }
}
