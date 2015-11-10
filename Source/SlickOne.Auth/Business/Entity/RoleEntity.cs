using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatOne.Auth.Business.Entity
{
    [Table("SysRole")]
    public class RoleEntity
    {
        public int ID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }
}
