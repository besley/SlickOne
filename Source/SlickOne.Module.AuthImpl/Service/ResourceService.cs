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
    public class ResourceService : ServiceBase, IResourceService
    {
        #region resource basic operation
        /// <summary>
        /// get all resource record
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// save resource
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
        /// delete resource
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

        /// <summary>
        /// delete resource
        /// </summary>
        /// <param name="resourceID">资源ID</param>
        public void DeleteResource(int resourceID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@resourceID", resourceID);
                QuickRepository.ExecuteProc("pr_sys_ResourceListDeleteByID", param);
            }
            catch (System.Exception ex)
            {
                //NLogWriter.Error("删除资源数据，并且删除相关联的表中数据失败!", ex);
                throw;
            }
        }
        #endregion

        #region 获取资源节点树
        /// <summary>
        /// get resource node --TreeView for ag-grid
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
        /// get resource node iterated
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
        /// create resource node
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
                UrlAction = item.UrlAction,
                DataAction = item.DataAction,
                StyleClass = item.StyleClass,
                OrderNum = item.OrderNum
            };

            if (childrenItems.Count() > 0)
            {
                resourceNode.children = childrenItems;
                resourceNode.group = true;
            }
            else
            {
                resourceNode.group = false;
            }
            return resourceNode;
        }
        #endregion

        /// <summary>
        /// get role resource data by roleid
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<RoleResourcePermissionView> GetResourceByRoleID(int roleID)
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

        /// <summary>
        /// get resource by user id
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
    }
}
