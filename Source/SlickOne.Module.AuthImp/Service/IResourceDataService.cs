using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickOne.Module.AuthImp.Entity;

namespace SlickOne.Module.AuthImp.Service
{
    /// <summary>
    /// 资源权限许可接口
    /// </summary>
    public interface IResourceDataService
    {
        List<ResourceEntity> GetResourceListByUserRole(int roleID, int userID);
        List<ResourceEntity> GetResourceByUserID(int userID);
        ResourceEntity SaveResource(ResourceEntity entity);
        void DeleteResource(XmlTransferEntity xmlEntity);
        List<ResourceEntity> GetResourceAll();
        ResourceNode GetResourceNodeAll();

        List<ResourceEntity> GetLeftMenuList(int userID);

        //List<ResourcePermissionView> GetResourcePermission(int userID);
        List<RoleResourcePermissionView> GetRoleResourceList(int roleID);
        List<RoleResourcePermissionView> GetResourcePermissionAllowed(int userID);
        List<RoleResourcePermissionView> GetResourcePermission(RoleUserQuery query);
    }
}
