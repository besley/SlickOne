using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// JsTree 属性
    /// </summary>
    public class JsTreeAttribute
    {
        public string id;
        public bool selected;
    }

    /// <summary>
    /// JsTreeModel 
    /// </summary>
    public class JsTreeModel
    {
        public string data;
        public JsTreeAttribute attr;
        public JsTreeModel[] children;
    }
}


