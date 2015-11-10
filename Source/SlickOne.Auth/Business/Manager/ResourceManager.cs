using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatOne.Data;
using PlatOne.Auth.Business.Entity;

namespace PlatOne.Auth.Business.Manager
{
    /// <summary>
    /// 资源管理类
    /// </summary>
    internal class ResourceManager : ManagerBase
    {
        /// <summary>
        /// 获取用户授权的资源
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        internal List<ResourceEntity> GetUserResource(int userID)
        {
            var sql = @"SELECT 
                            B.*
                        FROM SysUserResource A
                        INNER JOIN SysResource B
                            ON A.ResourceID=B.ID
                        WHERE A.UserID=@userID 
                        ORDER BY B.ID";
            var resourceList = Repository.Query<ResourceEntity>(sql,
                new { 
                    userID = userID 
                }).ToList() ;

            return resourceList;
        }
    }
}
