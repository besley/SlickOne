using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.AuthImpl.Entity
{
    /// <summary>
    /// 角色用户查询
    /// </summary>
    public class RoleUserQuery
    {
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}
