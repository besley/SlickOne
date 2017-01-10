using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickOne.Module.AuthImp.Entity;

namespace SlickOne.Module.AuthImp.Service
{
    public interface IRoleDataService
    {
        IList<RoleEntity> GetRoleAll();
        void SaveRole(RoleEntity entity);
        void DeleteRole(RoleEntity entity);
        IList<UserEntity> GetUserAll();
        void SaveUser(UserEntity entity);
        void DeleteUser(UserEntity entity);
        IList<RoleUserView> GetRoleUserAll();
        IList<RoleUserView> QueryUserByRole(RoleEntity query);
        void AddRoleUser(RoleUserEntity entity);
        void DeleteRoleUser(RoleUserEntity entity);

    }
}
