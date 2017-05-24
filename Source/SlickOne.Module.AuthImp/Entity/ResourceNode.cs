using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.AuthImpl.Entity
{
    /// <summary>
    /// resource node
    /// </summary>
    public class ResourceNode
    {
        public int ID { get; set; }
        public int ResourceTypeID { get; set; }
        public int ParentID { get; set; }
        public string ResourceName { get; set; }
        public string UrlAction { get; set; }
        public string DataAction { get; set; }
        public string StyleClass { get; set; }
        public short OrderNum { get; set; }
        public ResourceNode[] children { get; set; }
        public Boolean group { get; set; }
    }
}
