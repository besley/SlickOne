using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.AuthImp.Entity
{
    /// <summary>
    /// 资源实体
    /// </summary>
    [Table("SysResource")]
    public class ResourceEntity
    {
        public int ID { get; set; }
        public short ResourceTypeID { get; set; }
        public int ParentID { get; set; }
        public string ResourceCode { get; set; }
        public string ResourceName { get; set; }
        public string PageUrl { get; set; }
        public string TagID { get; set; }
        public string StyleClass { get; set; }
        public short OrderNum { get; set; }
        public byte CanNotBeDeleted { get; set; }
    }
}
