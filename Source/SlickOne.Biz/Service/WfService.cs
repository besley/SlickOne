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
    /// 流程数据获取服务实现
    /// </summary>
    public class WfService : IWfService
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
        /// 流程定义记录获取
        /// </summary>
        /// <returns></returns>
        public IList<ProcessEntity> GetProcessListSimple()
        {
            var sql = @"SELECT 
                            ID, 
                            ProcessGUID, 
                            ProcessName,
                            Version,
                            IsUsing,
                            CreatedDateTime
                        FROM WfProcess
                        ORDER BY ID DESC";
            var list = QuickRepository.Query<ProcessEntity>(sql).ToList();
            return list;
        }

        public IList<ActivityInstanceEntity> GetActivityInstances(int processInstanceID)
        {
            throw new NotImplementedException();
        }
    }
}
