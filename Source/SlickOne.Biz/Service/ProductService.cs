using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickOne.Data;
using SlickOne.Biz.Entity;

namespace SlickOne.Biz.Service
{
    /// <summary>
    /// 产品接口服务实现
    /// </summary>
    public class ProductService : IProductService
    {
        #region 基本属性
        private Repository _quickRepository;
        public Repository QuickRepository
        {
            get
            {
                if (_quickRepository == null) _quickRepository = new Repository();
                return _quickRepository;
            }
        }
        #endregion

        /// <summary>
        /// 根据ID获取产品实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductEntity Get(int id)
        {
            return QuickRepository.GetById<ProductEntity>(id);
        }

        /// <summary>
        /// 获取列表数据(示例查询方法，实际会用到分页，此处用于演示。)
        /// </summary>
        /// <returns></returns>
        public List<ProductEntity> GetProductList()
        {
            var sql = @"SELECT TOP 1000 
                            *
                        FROM PrdProduct
                        ORDER BY ID DESC";
            var list = QuickRepository.Query<ProductEntity>(sql, null)
                        .ToList();
            return list;
        }

        /// <summary>
        /// 产品数据查询
        /// </summary>
        /// <param name="query">查询实体</param>
        /// <returns>产品列表</returns>
        public List<ProductEntity> Query(ProductQuery query)
        {
            var sql = @"SELECT TOP 1000 
                            *
                        FROM PrdProduct
                        WHERE ProductType=@productType";
            var list = QuickRepository.Query<ProductEntity>(sql, new { productType=query.ProductType })
                        .ToList();
            return list;
        }

        /// <summary>
        /// 产品属性保存操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ProductEntity Save(ProductEntity entity)
        {
            ProductEntity returnEntity = null;
            if (entity.ID == 0)
            {
                entity.CreatedDate = System.DateTime.Now;
                var productID = QuickRepository.Insert<ProductEntity>(entity);
                entity.ID = productID;

                returnEntity = entity;
            }
            else
            {
                var updEntity = QuickRepository.GetById<ProductEntity>(entity.ID);
                updEntity.ProductName = entity.ProductName;
                updEntity.ProductType = entity.ProductType;
                updEntity.ProductCode = entity.ProductCode;
                updEntity.UnitPrice = entity.UnitPrice;
                updEntity.Notes = entity.Notes;
                QuickRepository.Update<ProductEntity>(updEntity);

                returnEntity = updEntity;
            }
            return returnEntity;
        }

        /// <summary>
        /// 产品数据删除操作
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            QuickRepository.Delete<ProductEntity>(id);
        }

    }
}
