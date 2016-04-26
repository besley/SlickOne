using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SlickOne.WebUtility;
using SlickMaster.Builder.Entity;
using SlickMaster.Builder.Service;

namespace SlickOne.Web.Controllers.WebApi
{
    /// <summary>
    /// 表单接口控制器
    /// </summary>
    public class FormMasterController : ApiController
    {
        /// <summary>
        /// 获取前10条数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<EntityDefEntity>> GetEntityDefList2()
        {
            var result = ResponseResult<List<EntityDefEntity>>.Default();
            try
            {
                var fbmasterService = new FBMasterService();
                var list = fbmasterService.GetEntityDefList2();
                result = ResponseResult<List<EntityDefEntity>>.Success(list);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<EntityDefEntity>>.Error(
                    string.Format("读取{0}数据失败, 错误：{1}", "EntityDefList", ex.Message)
                );
            }
            return result;
        }
    }
}
