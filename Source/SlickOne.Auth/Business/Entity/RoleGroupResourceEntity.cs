using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatOne.Auth.Business.Entity
{
    [Table("SysRoleGroupResource")]
    public class RoleGroupResourceEntity
    {
        public int ID { get; set; }
        public byte RgType { get; set; }
        public int RgID { get; set; }
        public int ResourceID { get; set; }
        public byte PermissionType { get; set; }
    }
}
