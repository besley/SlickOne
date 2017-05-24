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
using SlickOne.Module.AuthImpl.Entity;

namespace SlickOne.Module.AuthImpl.Service
{
    /// <summary>
    /// role service
    /// </summary>
    public interface IRoleService
    {
        IList<RoleEntity> GetRoleAll();
        void SaveRole(RoleEntity entity);
        void DeleteRole(RoleEntity entity);

        IList<UserAccountEntity> GetUserAll();
        void SaveUser(UserEntity entity);
        void DeleteUser(UserEntity entity);

        IList<RoleUserView> GetRoleUserAll();
        IList<RoleUserView> QueryUserByRole(RoleEntity query);
        void AddRoleUser(RoleUserEntity entity);
        void DeleteRoleUser(RoleUserEntity entity);

    }
}
