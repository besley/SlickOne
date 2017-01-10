using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickOne.Module.Auth
{
    /// <summary>
    /// RBAC Role Base Accessed Control 
    /// 授权服务接口
    /// </summary>
    public interface IAuthService
    {
        IList<Role> GetRoleAll();
        IList<User> GetUserListByRoles(string[] roleIDs);
        IList<Role> FillUsersIntoRoles(string[] roleIDs);
        IList<User> GetUserListByRole(string roleID);
        IList<User> GetUserListByRoleCode(string roleCode);
        IList<User> GetUserListByRole(string roleID, string curUserID, int receiverType);
    }
}
