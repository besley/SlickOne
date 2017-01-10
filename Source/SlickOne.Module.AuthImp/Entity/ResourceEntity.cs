using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.AuthImp.Entity
{
    [Table("SysResource")]
    public class ResourceEntity
    {
        public int ID { get; set; }
        public short TypeID { get; set; }
        public int ParentID { get; set; }
        public string TagID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string StyleClass { get; set; }
        public short OrderNo { get; set; }
    }
}
