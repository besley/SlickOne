using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickOne.Module.ProcessImpl.Entity;

namespace SlickOne.Module.ProcessImpl.Service
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
