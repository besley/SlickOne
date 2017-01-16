using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.AuthImp.Entity
{
    /// <summary>
    /// 资源节点类
    /// </summary>
    public class ResourceNode
    {
        public int ID { get; set; }
        public int ResourceTypeID { get; set; }
        public int ParentID { get; set; }
        public string ResourceName { get; set; }
        public string ResourceCode { get; set; }
        public string PageUrl { get; set; }
        public string TagID { get; set; }
        public string StyleClass { get; set; }
        public ResourceNode[] children { get; set; }
        public Boolean group { get; set; }
    }
}
