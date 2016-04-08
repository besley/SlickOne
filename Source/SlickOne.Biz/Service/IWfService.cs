using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickOne.Biz.Entity;

namespace SlickOne.Biz.Service
{
    public interface IWfService
    {
        IList<ProcessEntity> GetProcessListSimple();
        IList<ActivityInstanceEntity> GetActivityInstances(int processInstanceID);
    }
}
