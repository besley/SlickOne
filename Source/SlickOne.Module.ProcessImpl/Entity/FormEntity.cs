using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.ProcessImpl.Entity
{
    [Table("EavEntityDef")]
    public class FormEntity
    {
        public int ID { get; set; }
        public string EntityName { get; set; }
        public string EntityTitle { get; set; }
        public string EntityCode { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
