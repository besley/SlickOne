using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.AuthImpl.Entity
{
    /// <summary>
    /// paged query
    /// </summary>
    public abstract class QueryBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Field { set; get; }       //order by field
        public string Order { set; get; }       //order asc/desc
        public int TotalRowsCount { get; set; }
        public int TotalPages { get; set; }
    }
}
