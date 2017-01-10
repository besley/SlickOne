using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using SlickOne.Data;
using SlickOne.Module.AuthImp.Entity;

namespace SlickOne.Module.AuthImp.Service
{
    /// <summary>
    /// 权限数据服务
    /// </summary>
    public class RoleDataService : IRoleDataService
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
        /// 获取所有角色数据
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
        /// 保存角色数据
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
        /// 删除角色
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
        /// 获取所有用户数据
        /// </summary>
        /// <returns></returns>
        public IList<UserEntity> GetUserAll()
        {
            var list = QuickRepository.GetAll<UserEntity>().ToList();
            return list;
        }

        /// <summary>
        /// 保存用户数据
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
        /// 删除用户
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
        /// 获取角色下用户列表
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
        /// 根据角色查询用户
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
        /// 添加用户到角色
        /// </summary>
        /// <param name="entity"></param>
        public void AddRoleUser(RoleUserEntity entity)
        {
            QuickRepository.Insert<RoleUserEntity>(entity);
        }

        /// <summary>
        /// 删除角色下的用户
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
