using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SlickOne.Data;
using SlickOne.WebUtility;
using SlickOne.Biz.Entity;
using SlickOne.Biz.Service;

namespace SlickOne.Web.Controllers.WebApi
{
    /// <summary>
    /// 产品WebApi服务控制器
    /// </summary>
    public class ProductController : ApiController
    {
        #region 属性对象
        private Repository _quickRepository;
        public Repository QuickReporsitory
        {
            get
            {
                if (_quickRepository == null) _quickRepository = new Repository();
                return _quickRepository;
            }
        }

        private IProductService _productService;
        public IProductService ProductService
        {
            get
            {
                if (_productService == null) _productService = new ProductService();
                return _productService;
            }
        }
        #endregion

        [HttpGet]
        public ResponseResult<ProductEntity> Get(int id)
        {
            var result = ResponseResult<ProductEntity>.Default();
            try
            {
                var newEntity = ProductService.Get(id);
                result = ResponseResult<ProductEntity>.Success(newEntity);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<ProductEntity>.Error(
                    string.Format("获取产品数据失败, 错误：{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<ProductEntity>> GetProductList()
        {
            var result = ResponseResult<List<ProductEntity>>.Default();
            try
            {
                var list = ProductService.GetProductList();
                result = ResponseResult<List<ProductEntity>>.Success(list);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<ProductEntity>>.Error(
                    string.Format("读取{0}数据失败, 错误：{1}", "产品列表", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 保存产品数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<ProductEntity> Save(ProductEntity entity)
        {
            var result = ResponseResult<ProductEntity>.Default();
            try
            {
                var newEntity = ProductService.Save(entity);
                result = ResponseResult<ProductEntity>.Success(newEntity);
            }
            catch(System.Exception ex)
            {
                result = ResponseResult<ProductEntity>.Error(
                    string.Format("保存产品数据失败, 错误：{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 产品数据删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult Delete(int id)
        {
            var result = ResponseResult.Default();
            try
            {
                ProductService.Delete(id);
                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(
                    string.Format("删除产品数据失败, 错误：{0}", ex.Message)
                );
            }
            return result;
        }
    }
}