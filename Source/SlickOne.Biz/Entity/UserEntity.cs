using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Biz.Entity
{
    /// <summary>
    /// 用户实体
    /// </summary>
    [Table("SysUser")]
    public class UserEntity
    {
        public string ID { get; set; }
        public string UserName { get; set; }
    }
}
