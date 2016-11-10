using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Biz.Entity
{
    [Table("SysRoleUser")]
    public class RoleUserEntity
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}
