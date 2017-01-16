/*
* SlickOne WEB快速开发框架遵循LGPL协议，也可联系作者商业授权并获取技术支持；
* 除此之外的使用则视为不正当使用，请您务必避免由此带来的商业版权纠纷。

The SlickOne project.
Copyright (C) 2016  .NET Web Framwork Library

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

var soconfig = (function () {
	function soconfig() {
	}

	//#region tab configuration
	soconfig.tabname = [];
	soconfig.tabname["myrolegrid"] = "角色记录";
	soconfig.tabname["myusergrid"] = "用户记录";
	soconfig.tabname["myroleusertree"] = "角色用户维护";
	soconfig.tabname["myroleusergrid"] = "角色用户视图";
	soconfig.tabname["myresourcegrid"] = "资源数据";
    soconfig.tabname["myrolepermissiongrid"] = "角色授权";
	soconfig.tabname["datapermission"] = "数据权限";
	soconfig.tabname["permissionquery"] = "权限查询";
	soconfig.tabname["department"] = "部门数据";
	soconfig.tabname["employee"] = "员工激励";
	soconfig.tabname["deptemp"] = "部门员工视图";
	soconfig.tabname["myprocessgrid"] = "流程记录";
	soconfig.tabname["myformgrid"] = "表单记录";
	soconfig.tabname["myprocessinstancegrid"] = "流程实例";
	soconfig.tabname["myactivityinstancegrid"] = "活动实例";
	soconfig.tabname["task"] = "任务记录";
	soconfig.tabname["myloggrid"] = "系统日志";
	//#endregion

	//#region toolbutton configuration
	soconfig.toolbutton = [];
	soconfig.toolbutton["add"] = [];
	soconfig.toolbutton["edit"] = [];
	soconfig.toolbutton["delete"] = [];
	soconfig.toolbutton["query"] = [];

	soconfig.toolbutton["add"]["myrolegrid"] = "role/edit";
	soconfig.toolbutton["add"]["myusergrid"] = "user/edit";
    soconfig.toolbutton["add"]["myresourcegrid"] = "resource/edit";

	soconfig.toolbutton["edit"]["myrolegrid"] = "role/edit";
	soconfig.toolbutton["edit"]["myusergrid"] = "user/edit";

	soconfig.toolbutton["query"]["role"] = "role/query";
	soconfig.toolbutton["query"]["user"] = "user/query";

	//delete method
	soconfig.toolbutton["delete"]["myrolegrid"] = rolelist.delete;
	soconfig.toolbutton["delete"]["myusergrid"] = userlist.delete;

	//#endregion

	return soconfig;
})()