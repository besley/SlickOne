# SlickOne
===========
SlickOne 企业级Web快速开发框架，技术体系描述如下：Bootstrap3/Mvc(WebApi)Dapper，AG-Grid/zTree优秀开源组件，Dapper针对MSSQL, MySQL, Oracle等多数据库的实现，丰富代码示例。

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

  

