using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickOne.Biz.Entity;

namespace SlickOne.Biz.Service
{
    public interface IWfDataService
    {
        IList<ProcessEntity> GetProcessListSimple();
        IList<ProcessInstanceEntity> GetProcessInstanceList();
        IList<ActivityInstanceEntity> GetActivityInstanceList();
        IList<ActivityInstanceEntity> GetActivityInstanceList(int processInstanceID);
        IList<TaskEntity> GetTaskList();
        IList<FormEntity> GetEntityDefListSimple();
        IList<LogEntity> GetLogList();
    }
}
