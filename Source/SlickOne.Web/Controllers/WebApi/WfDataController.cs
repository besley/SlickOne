using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SlickOne.WebUtility;
using SlickOne.Biz.Entity;
using SlickOne.Biz.Service;

namespace SlickOne.Web.Controllers.WebApi
{
    /// <summary>
    /// 流程数据接口控制器
    /// </summary>
    public class WfDataController : ApiController
    {
        /// <summary>
        /// 获取流程记录列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<ProcessEntity>> GetProcessListSimple()
        {
            var result = ResponseResult<List<ProcessEntity>>.Default();
            try
            {
                var wfService = new WfDataService();
                var entity = wfService.GetProcessListSimple().ToList();

                result = ResponseResult<List<ProcessEntity>>.Success(entity);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<ProcessEntity>>.Error(
                    string.Format("获取流程基本信息失败！{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 获取流程实例数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<ProcessInstanceEntity>> GetProcessInstanceList()
        {
            var result = ResponseResult<List<ProcessInstanceEntity>>.Default();
            try
            {
                var wfService = new WfDataService();
                var entity = wfService.GetProcessInstanceList().ToList();

                result = ResponseResult<List<ProcessInstanceEntity>>.Success(entity);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<ProcessInstanceEntity>>.Error(
                    string.Format("获取流程实例数据失败！{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 获取活动实例数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<ActivityInstanceEntity>> GetActivityInstanceList()
        {
            var result = ResponseResult<List<ActivityInstanceEntity>>.Default();
            try
            {
                var wfService = new WfDataService();
                var entity = wfService.GetActivityInstanceList().ToList();

                result = ResponseResult<List<ActivityInstanceEntity>>.Success(entity);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<ActivityInstanceEntity>>.Error(
                    string.Format("获取流程活动实例数据失败！{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 获取任务数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<TaskEntity>> GetTaskList()
        {
            var result = ResponseResult<List<TaskEntity>>.Default();
            try
            {
                var wfService = new WfDataService();
                var entity = wfService.GetTaskList().ToList();

                result = ResponseResult<List<TaskEntity>>.Success(entity);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<TaskEntity>>.Error(
                    string.Format("获取任务实例数据失败！{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 获取日志数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<LogEntity>> GetLogList()
        {
            var result = ResponseResult<List<LogEntity>>.Default();
            try
            {
                var wfService = new WfDataService();
                var entity = wfService.GetLogList().ToList();

                result = ResponseResult<List<LogEntity>>.Success(entity);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<LogEntity>>.Error(
                    string.Format("读取{0}数据失败, 错误：{1}", "Log", ex.Message)
                );
            }
            return result;
        }
    }
}
