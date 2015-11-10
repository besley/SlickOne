using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickOne.Data;
using SlickOne.Biz.Entity;

namespace SlickOne.Biz.Service
{
    public class ProductService : IProductService
    {
        #region 基本属性
        private Repository _quickRepository;
        public Repository QuickReporsitory
        {
            get
            {
                if (_quickRepository == null) _quickRepository = new Repository();
                return _quickRepository;
            }
        }
        #endregion

        /// <summary>
        /// 获取列表数据(示例查询方法，实际会用到分页，此处用于演示。)
        /// </summary>
        /// <returns></returns>
        public List<ProductEntity> GetProductList()
        {
            var sql = @"SELECT ID, 
                            ProductName, 
                            ProductCode, 
                            ProductType, 
                            UnitPrice, 
                            CreatedDate 
                        FROM PrdProduct";
            var list = QuickReporsitory.Query<ProductEntity>(sql, null)
                        .ToList();
            return list;
        }

    }
}
