using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatOne.Auth.Business.Entity
{
    [Table("SysUserResource")]
    public class UserResourceEntity
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ResourceID { get; set; }
    }
}
