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
    public class WfDataService : IWfDataService
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

        public IList<ProcessInstanceEntity> GetProcessInstanceList()
        {
            var sql = @"SELECT TOP 100
                            ID, 
                            ProcessName,
                            AppName,
                            ProcessState,
                            CreaedDateTime,
                            CreatedByUserName
                        FROM WfProcessInstance
                        ORDER BY ID DESC";
            var list = QuickRepository.Query<ProcessInstanceEntity>(sql).ToList();
            return list;
        }

        public IList<ActivityInstanceEntity> GetActivityInstanceList()
        {
            var sql = @"SELECT TOP 100
                            ID, 
                            ProcessName,
                            AppName,
                            ActivityName,
                            ActivityType,
                            ActivityState,
                            AssignedToUserNames,
                            CreatedDateTime,
                            CreatedByUserName,
                            EndedDateTime,
                            EndedByUserName
                        FROM WfActivityInstance
                        ORDER BY ID DESC";
            var list = QuickRepository.Query<ActivityInstanceEntity>(sql).ToList();
            return list;
        }

        public IList<ActivityInstanceEntity> GetActivityInstanceList(int processInstanceID)
        {
            var sql = @"SELECT TOP 100
                            ID, 
                            ProcessName,
                            AppName,
                            ActivityName,
                            ActivityType,
                            ActivityState,
                            AssignedToUserNames,
                            CreatedDateTime,
                            CreatedByUserName,
                            EndedDateTime,
                            EndedByUserName
                        FROM WfActivityInstance
                        WHERE ProcessInstanceID=@processInstanceID
                        ORDER BY ID DESC";
            var list = QuickRepository.Query<ActivityInstanceEntity>(sql, 
                new { processInstanceID=processInstanceID}).ToList();
            return list;
        }


        public IList<TaskEntity> GetTaskList()
        {
            var sql = @"SELECT TOP 100
                            ID, 
                            AppName,
                            ActivityName,
                            TaskType,
                            TaskState,
                            CreaedDateTime,
                            CreatedByUserName,
                            AssignedToUserNames,
                            EndedDateTime,
                            EndedByUserName
                        FROM WfTasks
                        ORDER BY ID DESC";
            var list = QuickRepository.Query<TaskEntity>(sql).ToList();
            return list;
        }

        public IList<LogEntity> GetLogList()
        {
            var sql = @"SELECT TOP 100
                            ID, 
                            EventTypeID,
                            Priority,
                            Severity,
                            Title,
                            Message,
                            Timestamp
                        FROM WfLog
                        ORDER BY ID DESC";
            var list = QuickRepository.Query<LogEntity>(sql).ToList();
            return list;
        }
    }
}
