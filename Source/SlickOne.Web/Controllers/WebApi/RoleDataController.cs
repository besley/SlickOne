using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SlickOne.WebUtility;
using SlickOne.Module.AuthImp.Entity;
using SlickOne.Module.AuthImp.Service;


namespace SlickOne.Web.Controllers.WebApi
{
    /// <summary>
    /// 资源权限控制器
    /// </summary>
    public class RoleDataController : ApiController
    {
        /// <summary>
        /// 获取所有角色数据集
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<RoleEntity>> GetRoleAll()
        {
            var result = ResponseResult<List<RoleEntity>>.Default();
            try
            {
                var roleService = new RoleDataService();
                var roleList = roleService.GetRoleAll().ToList();

                result = ResponseResult<List<RoleEntity>>.Success(roleList);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<RoleEntity>>.Error(
                    string.Format("获取角色数据失败！{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 保存角色数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult SaveRole(RoleEntity entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var roleService = new RoleDataService();
                roleService.SaveRole(entity);

                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(string.Format("保存角色数据失败!{0}", ex.Message));
            }
            return result;
        }

        [HttpPost]
        public ResponseResult DeleteRole(RoleEntity entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var roleService = new RoleDataService();
                roleService.DeleteRole(entity);

                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(string.Format("删除角色数据失败!{0}", ex.Message));
            }
            return result;
        }

        /// <summary>
        /// 获取所有角色数据集
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<UserEntity>> GetUserAll()
        {
            var result = ResponseResult<List<UserEntity>>.Default();
            try
            {
                var roleService = new RoleDataService();
                var userList = roleService.GetUserAll().ToList();

                result = ResponseResult<List<UserEntity>>.Success(userList);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<UserEntity>>.Error(
                    string.Format("获取用户数据失败！{0}", ex.Message)
                );
            }
            return result;
        }

        [HttpPost]
        public ResponseResult SaveUser(UserEntity entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var roleService = new RoleDataService();
                roleService.SaveUser(entity);

                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(string.Format("保存用户数据失败!{0}", ex.Message));
            }
            return result;
        }

        [HttpPost]
        public ResponseResult DeleteUser(UserEntity entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var roleService = new RoleDataService();
                roleService.DeleteUser(entity);

                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(string.Format("删除用户数据失败!{0}", ex.Message));
            }
            return result;
        }

        /// <summary>
        /// 获取所有角色数据集
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<RoleUserView>> GetRoleUserAll()
        {
            var result = ResponseResult<List<RoleUserView>>.Default();
            try
            {
                var roleService = new RoleDataService();
                var userList = roleService.GetRoleUserAll().ToList();

                result = ResponseResult<List<RoleUserView>>.Success(userList);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<RoleUserView>>.Error(
                    string.Format("获取角色用户数据失败！{0}", ex.Message)
                );
            }
            return result;
        }

        /// <summary>
        /// 添加用户到角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult AddRoleUser(RoleUserEntity entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var roleService = new RoleDataService();
                roleService.AddRoleUser(entity);

                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(string.Format("添加用户到角色失败!{0}", ex.Message));
            }
            return result;
        }

        /// <summary>
        /// 删除角色下用户
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult DeleteRoleUser(RoleUserEntity entity)
        {
            var result = ResponseResult.Default();
            try
            {
                var roleService = new RoleDataService();
                roleService.DeleteRoleUser(entity);

                result = ResponseResult.Success();
            }
            catch (System.Exception ex)
            {
                result = ResponseResult.Error(string.Format("删除角色下用户失败!{0}", ex.Message));
            }
            return result;
        }

    }
}
