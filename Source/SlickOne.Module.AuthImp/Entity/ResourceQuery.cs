using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.AuthImp.Entity
{
    /// <summary>
    /// 资源查询
    /// </summary>
    public class ResourceQuery : QueryBase
    {
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}
