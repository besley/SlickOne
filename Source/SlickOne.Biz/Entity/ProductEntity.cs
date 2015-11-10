using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Biz.Entity
{
    /// <summary>
    /// 产品实体对象
    /// </summary>
    [Table("PrdProduct")]
    public class ProductEntity
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductType { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Notes { get; set; }
    }
}
