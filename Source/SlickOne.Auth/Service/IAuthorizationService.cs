using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatOne.Auth.Business.Entity;

namespace PlatOne.Auth.Service
{
    /// <summary>
    /// 资源权限授权服务接口
    /// </summary>
    public interface IAuthorizationService
    {
        IList<ResourceEntity> GetResourceByRole(int roleID);
        IList<ResourceEntity> GetResourceByUser(int userID);
    }
}
