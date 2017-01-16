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
    /// 资源权限许可服务实现类
    /// </summary>
    public class ResourceDataService : ServiceBase, IResourceDataService
    {
        #region 获取资源基本数据
        public List<ResourceEntity> GetResourceAll()
        {
            List<ResourceEntity> list = new List<ResourceEntity>();
            try
            {
                list = QuickRepository.GetAll<ResourceEntity>().ToList<ResourceEntity>();
                return list;
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("获取所有应用资源信息失败!", ex);
                throw;
            }
        }
        #endregion

        #region 获取资源节点树
        /// <summary>
        /// 获取资源节点列表--TreeView
        /// </summary>
        /// <returns></returns>
        public ResourceNode GetResourceNodeAll()
        {
            try
            {
                var rootNode = new ResourceNode { ID = 0, ParentID = -1, ResourceName = "资源列表", group = true };
                var resourceList = QuickRepository.GetAll<ResourceEntity>().ToList<ResourceEntity>();
                var rootItems = from a in resourceList
                                where a.ParentID == 0
                                select a;
                int index = 0;
                ResourceNode[] resourceTreeTop = new ResourceNode[rootItems.Count()];
                foreach (var item in rootItems)
                {
                    ResourceNode[] childrenItems = GetResourceNodeListIteratedly(item.ID, resourceList);
                    resourceTreeTop[index++] = CreateResourceNodeSingle(item, childrenItems);
                }
                rootNode.children = resourceTreeTop;
                return rootNode;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 递归获取资源数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private ResourceNode[] GetResourceNodeListIteratedly(int parentID, List<ResourceEntity> list)
        {
            var filterList = from a in list
                             where a.ParentID == parentID
                             select a;
            int index = 0;
            ResourceNode[] resourceTreeInner = new ResourceNode[filterList.Count()];
            foreach (var item in filterList)
            {
                ResourceNode[] childrenItems = GetResourceNodeListIteratedly(item.ID, list);
                resourceTreeInner[index++] = CreateResourceNodeSingle(item, childrenItems);
            }
            return resourceTreeInner;
        }

        /// <summary>
        /// 构建资源节点数据
        /// </summary>
        /// <param name="item"></param>
        /// <param name="childrenItems"></param>
        /// <returns></returns>
        private ResourceNode CreateResourceNodeSingle(ResourceEntity item, ResourceNode[] childrenItems)
        {
            ResourceNode resourceNode = null;
            resourceNode = new ResourceNode
            {
                ID = item.ID,
                ParentID = item.ParentID,
                ResourceTypeID = item.ResourceTypeID,
                ResourceName = item.ResourceName,
                ResourceCode = item.ResourceCode,
                PageUrl = item.PageUrl,
                TagID = item.TagID,
                StyleClass = item.StyleClass
            };

            if (childrenItems.Count() > 0)
            {
                resourceNode.children = childrenItems;
                resourceNode.group = true;
            } else
            {
                resourceNode.group = false;
            }
            return resourceNode;
        }
        #endregion

        #region 单个角色资源许可操作--TreeView
        /// <summary>
        /// 根据角色获取资源许可列表
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<RoleResourcePermissionView> GetRoleResourceList(int roleID)
        {
            List<RoleResourcePermissionView> list = null;
            try
            {
                var param = new DynamicParameters();
                param.Add("@roleID", roleID);

                list = QuickRepository.ExecProcQuery<RoleResourcePermissionView>("pr_sys_RoleResourceListGetByRole",
                    param).ToList<RoleResourcePermissionView>();

                return list;
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("根据角色获取资源许可失败!", ex);
                throw;
            }
        }
        #endregion

        /// <summary>
        /// 根据角色或用户获取资源列表
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ResourceEntity> GetResourceListByUserRole(int roleID, int userID)
        {
            List<ResourceEntity> list = null;
            try
            {
                var param = new DynamicParameters();
                param.Add("@roleID", roleID);
                param.Add("@userID", userID);

                list = QuickRepository.ExecProcQuery<ResourceEntity>("pr_sys_ResourceListAllowedGetByUserOrRole",
                    param).ToList<ResourceEntity>();

                return list;
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("根据角色获取资源失败!", ex);
                throw;
            }
        }

        /// <summary>
        /// 根据用户获取资源
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<ResourceEntity> GetResourceByUserID(int userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@userID", userID);

                var list = QuickRepository.ExecProcQuery<ResourceEntity>("pr_sys_ResourceListGetByUserID",
                     param).ToList<ResourceEntity>();

                return list;
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("根据UserID获取资源列表(用于用户登录,保存可操作资源)失败!", ex);
                throw;
            }
        }

        ///// <summary>
        ///// 获取左侧导航菜单
        ///// </summary>
        ///// <returns></returns>
        //public ResourceNode GetLeftMenu(int userID)
        //{
        //    var list = GetLeftMenuList(userID);
        //    var bv = new ResourceNode
        //    {
        //        ID = 0,
        //        ResourceCode = "LEFT_MENU_ROOT",
        //        ResourceName = "左侧导航树"
        //    };

        //    bv.children = GetChildren(0, list);

        //    return bv;
        //}


        /// <summary>
        /// 查询左侧导航树数据
        /// </summary>
        /// <returns></returns>
        public List<ResourceEntity> GetLeftMenuList(int userID)
        {
            List<ResourceEntity> list = null;
            try
            {
                var param = new DynamicParameters();

                param.Add("@userID", userID);
                list = QuickRepository.ExecProcQuery<ResourceEntity>("pr_sys_ResourceLeftMenuGetByUser", param)
                    .ToList<ResourceEntity>();
                return list;
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("查询左侧导航树数据失败!", ex);
                throw;
            }
        }

        /// <summary>
        /// 递归封装子级节点
        /// </summary>
        /// <param name="partentID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private ResourceNode[] GetChildren(int partentID, List<ResourceNode> list)
        {
            //获取子节点列表
            var children = (from a in list
                            where a.ParentID == partentID
                            select a).ToList<ResourceNode>();
            var count = children.Count();

            ResourceNode[] bvArray = new ResourceNode[count];
            ResourceNode bv = null;
            ResourceNode entity = null;

            for (var i = 0; i < count; i++)
            {
                entity = children[i] as ResourceNode;
                bv = new ResourceNode();
                bv.ResourceCode = entity.ResourceCode;
                bv.ResourceName = entity.ResourceName;
                bv.ResourceTypeID = entity.ResourceTypeID;
                bv.ParentID = entity.ParentID;

                //递归获取子级节点
                bv.children = GetChildren(entity.ID, list);
                bvArray[i] = bv;
            }
            return bvArray;
        }

        ///// <summary>
        ///// 根据资源ID，从临时列表获取资源
        ///// </summary>
        ///// <param name="resourceID"></param>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //private ResourceNode GetResourceByID(int resourceID, List<ResourceNode> list)
        //{
        //    var node = (from a in list
        //                where a.ID == resourceID
        //                select a).FirstOrDefault<ResourceNode>();
        //    return node;
        //}

        /// <summary>
        /// 获取用户资源权限列表(用于用户分配资源权限)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RoleResourcePermissionView> GetResourcePermission(RoleUserQuery query)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@userID", query.UserID);
                param.Add("@roleID", query.RoleID);

                var list = QuickRepository.ExecProcQuery<RoleResourcePermissionView>("pr_sys_ResourcePermissionGetListByUserRole",
                     param).ToList<RoleResourcePermissionView>();

                return list;
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("获取用户资源权限列表(用于用户分配资源权限)失败!", ex);
                throw;
            }
        }

        /// <summary>
        ///  获取用户资源权限列表(用于用户管理，显示有效权限)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<RoleResourcePermissionView> GetResourcePermissionAllowed(int userID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@userID", userID);

                var list = QuickRepository.ExecProcQuery<RoleResourcePermissionView>("pr_sys_ResourceListAllowedGetByUserOrRole",
                     param).ToList<RoleResourcePermissionView>();

                return list;
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("获取用户资源权限列表(用于用户管理，显示有效权限)失败!", ex);
                throw;
            }
        }

        /// <summary>
        /// 保存资源数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResourceEntity SaveResource(ResourceEntity entity)
        {
            try
            {
                if (entity.ID > 0)
                {
                    QuickRepository.Update<ResourceEntity>(entity);
                }
                else
                {
                    var newID = QuickRepository.Insert<ResourceEntity>(entity);
                    entity.ID = newID;
                }
                return entity;
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("删除资源数据，并且删除相关联的表中数据失败!", ex);
                throw;
            }
        }

        /// <summary>
        /// 删除资源数据，并且删除相关联的表中数据
        /// </summary>
        /// <param name="xmlEntity"></param>
        public void DeleteResource(XmlTransferEntity xmlEntity)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@strXml", xmlEntity.xmlBody);
                QuickRepository.ExecuteProc("pr_prd_DeleteResourceBeth", param);
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("删除资源数据，并且删除相关联的表中数据失败!", ex);
                throw;
            }
        }
    }
}
