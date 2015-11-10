using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatOne.Auth.Business.Entity
{
    [Table("SysResource")]
    public class ResourceEntity
    {
        public int ID { get; set; }
        public short ResourceType { get; set; }
        public int ParentResourceID { get; set; }
        public string ResourceCode { get; set; }
        public string ResourceName { get; set; }
        public short OrderNo { get; set; }
    }
}
