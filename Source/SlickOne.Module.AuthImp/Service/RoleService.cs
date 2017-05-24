/*
* SlickOne 企业级WEB快速开发框架遵循LGPL协议，也可联系作者获取商业授权
* 和技术支持服务；除此之外的使用，则视为不正当使用，请您务必避免由此带来的
* 商业版权纠纷。
*
The SlickOne Product.
Copyright (C) 2017  .NET Authorization Framework Software

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, you can access the official
web page about lgpl: https://www.gnu.org/licenses/lgpl.html
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using SlickOne.Data;
using SlickOne.Module.AuthImpl.Entity;

namespace SlickOne.Module.AuthImpl.Service
{
    /// <summary>
    /// role data service implementation
    /// </summary>
    public class RoleService : ServiceBase, IRoleService
    {
        /// <summary>
        /// get all role
        /// </summary>
        /// <returns></returns>
        public IList<RoleEntity> GetRoleAll()
        {
            var sql = @"SELECT 
                            *
                        FROM SysRole
                        ORDER BY RoleName";
            var list = QuickRepository.Query<RoleEntity>(sql).ToList();
            return list;
        }

        /// <summary>
        /// save role data
        /// </summary>
        /// <param name="entity"></param>
        public void SaveRole(RoleEntity entity)
        {
            var param = new DynamicParameters();
            param.Add("@roleID", entity.ID);
            param.Add("@roleName", entity.RoleName);
            param.Add("@roleCode", entity.RoleCode);

            using (var conn = SessionFactory.CreateConnection())
            {
                QuickRepository.ExecuteProc(conn, "pr_sys_RoleSave", param);
            }
        }

        /// <summary>
        /// delete role
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteRole(RoleEntity entity)
        {
            var param = new DynamicParameters();
            param.Add("@roleID", entity.ID);

            using (var conn = SessionFactory.CreateConnection())
            {
                QuickRepository.ExecuteProc(conn, "pr_sys_RoleDelete", param);
            }
        }

        /// <summary>
        /// get user all data
        /// </summary>
        /// <returns></returns>
        public IList<UserAccountEntity> GetUserAll()
        {
            var sql = @"SELECT 
                            *
                        FROM SysUser
                        WHERE AccountType <> -1
                        ORDER BY ID DESC";
            var list = QuickRepository.Query<UserAccountEntity>(sql).ToList();
            return list;
        }

        /// <summary>
        /// save user
        /// </summary>
        /// <param name="entity"></param>
        public void SaveUser(UserEntity entity)
        {
            var param = new DynamicParameters();
            param.Add("@userID", entity.ID);
            param.Add("@userName", entity.UserName);

            using (var conn = SessionFactory.CreateConnection())
            {
                QuickRepository.ExecuteProc(conn, "pr_sys_UserSave", param);
            }
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteUser(UserEntity entity)
        {
            var param = new DynamicParameters();
            param.Add("@userID", entity.ID);

            using (var conn = SessionFactory.CreateConnection())
            {
                QuickRepository.ExecuteProc(conn, "pr_sys_UserDelete", param);
            }
        }

        /// <summary>
        /// get role user data
        /// </summary>
        /// <returns></returns>
        public IList<RoleUserView> GetRoleUserAll()
        {
            var strSQL = @"SELECT 
                               ID,
                               RoleID,
                               RoleName,
                               RoleCode,
                               UserID,
                               UserName
                           FROM vw_SysRoleUserView
                           ORDER BY RoleID, UserID";
            
            var list = QuickRepository.Query<RoleUserView>(strSQL).ToList();
            return list;
        }

        /// <summary>
        /// query user of role
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<RoleUserView> QueryUserByRole(RoleEntity query)
        {
            var strSQL = @"SELECT 
                               ID,
                               RoleID,
                               RoleName,
                               RoleCode,
                               UserID,
                               UserName
                           FROM vw_SysRoleUserView
                           WHERE RoleID=@roleID
                           ORDER BY RoleID";

            var list = QuickRepository.Query<RoleUserView>(strSQL, new { roleID = query.ID }).ToList();
            return list;
        }

        /// <summary>
        /// add user into role
        /// </summary>
        /// <param name="entity"></param>
        public void AddRoleUser(RoleUserEntity entity)
        {
            QuickRepository.Insert<RoleUserEntity>(entity);
        }

        /// <summary>
        /// delete user from role
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteRoleUser(RoleUserEntity entity)
        {
            var param = new DynamicParameters();
            param.Add("@userID", entity.UserID);
            param.Add("@roleID", entity.RoleID);

            using (var conn = SessionFactory.CreateConnection())
            {
                QuickRepository.ExecuteProc(conn, "pr_sys_RoleUserDelete", param);
            }
        }
    }
}
