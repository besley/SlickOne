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

var rolelist = (function () {
	function rolelist() {
	}

	rolelist.pselectedRoleID = "";
	rolelist.pselectedRoleDataRow = null;

	//#region Role DataGrid
	rolelist.getRoleList = function () {
		jshelper.ajaxGet('api/RoleData/GetRoleAll', null, function (result) {
			if (result.Status === 1) {
				var divRoleGrid = document.querySelector('#myrolegrid');
				$(divRoleGrid).empty();

				var gridOptions = {
					columnDefs: [
						{ headerName: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
						{ headerName: "角色名称", field: "RoleName", width: 120, cssClass: "bg-gray" },
						{ headerName: "角色代码", field: "RoleCode", width: 160, cssClass: "bg-gray" }
					],
					rowSelection: 'single',
					onSelectionChanged: onSelectionChanged
				}

				new agGrid.Grid(divRoleGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

				function onSelectionChanged() {
					var selectedRows = gridOptions.api.getSelectedRows();
					var selectedProcessID = 0;
					selectedRows.forEach(function (selectedRow, index) {
						rolelist.pselectedRoleID = selectedRow.ID;
						rolelist.pselectedRoleDataRow = selectedRow;
					});
				}
			} else {
				$.msgBox({
					title: "Role / List",
					content: "读取角色记录失败！错误信息：" + result.Message,
					type: "error"
				});
			}
		});
	}

	rolelist.loadRole = function () {
		if (somain.activeToolButtonType === "edit"
			&& rolelist.pselectedRoleID != "") {
			var entity = rolelist.pselectedRoleDataRow;
			$("#txtRoleName").val(entity.RoleName);
			$("#txtRoleCode").val(entity.RoleCode);
		} else {
			$("#txtRoleName").val("");
			$("#txtRoleCode").val("");
		}
	}

	rolelist.editRole = function () {
		var entity = rolelist.pselectedRoleDataRow;
		if (entity == null) {
			$.msgBox({
				title: "SlickOne / Role",
				content: "请先选择角色记录！",
				type: "alert"
			});
			return false;
		}
	}

	rolelist.saveRole = function () {
		if ($("#txtRoleName").val() == ""
            || $("#txtRoleCode").val() == "") {
			$.msgBox({
				title: "SlickOne / Role",
				content: "请输入角色基本信息！",
				type: "alert"
			});
			return false;
		}

		var entity = {
            "ID": "0",
			"RoleName": $("#txtRoleName").val(),
			"RoleCode": $("#txtRoleCode").val(),
		};

        if (somain.activeToolButtonType === "edit"){
           entity.ID = rolelist.pselectedRoleID;
        } 

		roleapi.save(entity);
	}

	rolelist.delete = function () {
		$.msgBox({
			title: "Are You Sure",
			content: "确实要删除角色记录吗? ",
			type: "confirm",
			buttons: [{ value: "Yes" }, { value: "Cancel" }],
			success: function (result) {
				if (result == "Yes") {
					var entity = {
						"ID": rolelist.pselectedRoleID,
					};
					roleapi.delete(entity);
					return;
				}
			}
		});
	}

	rolelist.sure = function () {
		if (rolelist.pselecteRoleID != "") {
		}
	}

	return rolelist;
})();


var roleapi = (function () {
	function roleapi() {
	}

	roleapi.save = function (entity) {
		jshelper.ajaxPost('api/RoleData/SaveRole',
            JSON.stringify(entity),
            function (result) {
            	if (result.Status == 1) {
            		$.msgBox({
            			title: "SlickOne / Role",
            			content: "角色记录已经成功保存！",
            			type: "info"
            		});
            	} else {
            		$.msgBox({
            			title: "SlickOne / Role",
            			content: result.Message,
            			type: "error",
            			buttons: [{ value: "Ok" }],
            		});
            	}
            });
	}

	roleapi.delete = function (entity) {
		//delete the selected row
		jshelper.ajaxPost('api/RoleData/DeleteRole',
            JSON.stringify(entity),
            function (result) {
            	if (result.Status == 1) {
            		$.msgBox({
            			title: "SlickOne / Role",
            			content: "角色记录已经删除！",
            			type: "info"
            		});

            		//refresh
            		rolelist.getRoleList();
            	} else {
            		$.msgBox({
            			title: "Ooops",
            			content: result.Message,
            			type: "error",
            			buttons: [{ value: "Ok" }],
            		});
            	}
            });
	}

	return roleapi;
})();