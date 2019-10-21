# SlickOne

SlickOne is a basic library for an enterprise information system or website project. Some new features have been 
implemenmted in the solution. The solution is designed for 3-tier distributed system, SOA based system, Repository pattern, POCO entity pattern, plugin architecture and asp.net mvc/web api architecture. There are some details  described here:

1. The 3-tier distributed layer include: data access layer, buisiness logic layer and web presentation layer.
2. Using micro-ORMapping framework Dapper/DapperExtension for database operation.
3. Using Generic repository pattern to convert data entity and business entity.
4. Using Asp.net MVC WebAPI to implement service layer/business layer, webapi is a restful style service, we make it
   to replacte wcf, the reason is that wcf would make you spend much time to maintain xml config in both server and 
   client sides.
5. All business logic are implemented from Interface, it makes concept not depended on the concrete class instance.
6. The IRepository class can be used to implement EF, NHerbinate framework which the user prefered to them.
7. MSSQL, MySQL, Oracle and other database supported by Dapper.
 
The SlickOne.Web project would give you a full tutorial how to use the SlickOne library and webapi to create a rich mvc web
application. Similarily, there are serveral key features to describe here:

1. Bootstrap3/Mvc(WebApi)/Dapper.
2. AG-Grid/zTree/Bootstrap-Dialog.
3. NavBar in top and left side.
4. Rich page demos in solution.


SlickOne 企业级Web快速开发框架，技术体系描述如下：
1. Bootstrap3/Mvc(WebApi)；
2. AG-Grid、zTree优秀开源组件；
3. Dapper 多数据库支持(MSSQL, MySQL, Oracle)；
4. WebApi 访问支持前后端分离；
5. SSO多站点统一登录验证实现；
6. 多租户\SAAS平台基础数据框架支持；


# SlickOne 1.7.0 版本发布：

1. 用户登录身份验证，随机加密算法，密码加盐(Salt)单向验证实现；
2. SSO多站点统一登录验证，一次登录即可访问集成的多站点；
3. Mvc页面安全访问；
4. WebAPI的安全访问；
5. 集成流程设计器，表单设计器；
6. 基础数据（公司/角色/用户）支持多集团，多租户和SAAS平台系统要求；
7. .NETCore 2.1 支持

SlickOne--DEMO地址：

1. 演示地址：http://gc.slickflow.com/sfadmin/ 
2. 用户名和密码：admin/123456
3. 流程设计器：http://gc.slickflow.com/sfd/ 
4. 表单设计器：http://gc.slickflow.com/smd/ 

设计介绍：

1. 博客文章：http://www.cnblogs.com/slickflow/p/7867712.html


说明：

1. Demo仅作为功能演示使用，如需获取产品完整源代码和开发文档，请申请企业版商业授权。
2. QQ群：151650479
3. EMail: sales@ruochisoft.com




﻿# SlickOne 0.1.2 版本发布：
===========

1. Bootstrap3.3.5版本升级；
2. AG-Grid 替代SlickGrid，同样强大功能的数据控件，有完善开源社区支持；
3. 左侧导航菜单折叠隐藏功能实现；
4. 用户添加, 角色维护等功能示例实现。


项目描述

基于MVC, WebApi, Dapper的3层分布式架构开发框架，其特点是：

1. 采用Dapper微ORMapping框架，性能接近原生SQL；
2. 采用Repository模式；
3. 采用面向Interface接口编程规范；
4. 采用WebApi实现服务总线;
5. 前端AG-Grid数据控件展现，Web富交互功能实现；
6. 前端Bootstrap框架布局在线演示Demo实现；
7. 多数据库的支持，默认SQLSERVER，支持Oracle, MySQL, KingBase（人大金仓） 等数据库；


SlickOne框架在线DEMO及项目应用演示案例：

1. Web前端框架在线演示：
http://demo.slickflow.com/soweb/
2. 基于SlickOne的MVC应用：
http://demo.slickflow.com/sfmvc/
3. 基于SlickOne的流程设计器：
http://demo.slickflow.com/sfd/
4. 基于SlickOne的表单设计器：
http://demo.slickflow.com/smd/


QQ交流群：
151650479

  

