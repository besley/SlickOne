/*
* SlickOne WEB快速开发框架遵循LGPL协议，也可联系作者商业授权并获取技术支持；
* 除此之外的使用则视为不正当使用，请您务必避免由此带来的商业版权纠纷。

The Slickflow Designer project.
Copyright (C) 2014  .NET Workflow Engine Library

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

var roleuserlist = (function () {
	function roleuserlist() {
	}

	roleuserlist.pselectedRoleUserID = "";
	roleuserlist.pselectedRoleUserDataRow = null;
	roleuserlist.pmztree = null;

	roleuserlist.initListForm = function () {
		$('#modelDialogForm').on('hidden', function () {
			$(this).removeData('modal').find(".modal-body").empty();;
		});
	}

	//#region Role DataGrid
	roleuserlist.getRoleUserList = function () {
		$('#loading-indicator').show();
		jshelper.ajaxGet('api/RoleData/GetRoleUserAll', null, function (result) {
			if (result.Status === 1) {
				var columnRoleUser = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
					{ id: "RoleID", name: "角色ID", field: "RoleID", width: 60, cssClass: "bg-gray" },
                    { id: "RoleCode", name: "角色代码", field: "RoleCode", width: 120, cssClass: "bg-gray" },
					{ id: "RoleName", name: "角色名称", field: "RoleName", width: 120, cssClass: "bg-gray" },
					{ id: "UserID", name: "用户ID", field: "UserID", width: 60, cssClass: "bg-gray" },
                    { id: "UserName", name: "用户名称", field: "UserName", width: 160, cssClass: "bg-gray" }
				];

				var optionsRoleUser = {
					editable: true,
					enableCellNavigation: true,
					enableColumnReorder: true,
					asyncEditorLoading: true,
					forceFitColumns: false,
					topPanelHeight: 25
				};

				var dsRoleUser = result.Entity;

				var dvRoleUser = new Slick.Data.DataView({ inlineFilters: true });
				var gridRoelUser = new Slick.Grid("#myroleusergrid", dvRoleUser, columnRoleUser, optionsRoleUser);

				dvRoleUser.onRowsChanged.subscribe(function (e, args) {
					gridRoelUser.invalidateRows(args.rows);
					gridRoelUser.render();
				});

				dvRoleUser.onRowCountChanged.subscribe(function (e, args) {
					gridRoelUser.updateRowCount();
					gridRoelUser.render();
				});

				dvRoleUser.beginUpdate();
				dvRoleUser.setItems(dsRoleUser, "ID");
				gridRoelUser.setSelectionModel(new Slick.RowSelectionModel());
				dvRoleUser.endUpdate();

				//rows change event
				gridRoelUser.onSelectedRowsChanged.subscribe(function (e, args) {
					var selectedRowIndex = args.rows[0];
					var row = dvRoleUser.getItemByIdx(selectedRowIndex);
					if (row) {
						//marked and returned selected row info
						roleuserlist.pselectedRoleUserID = row.ID;
						roleuserlist.pselectedRoleUserDataRow = row;
					}
				});


				$('#loading-indicator').hide();
			}
		});
	}



	roleuserlist.deleteRoleUser = function () {
		$.msgBox({
			title: "Are You Sure",
			content: "确实要删除角色下的用户记录吗? ",
			type: "confirm",
			buttons: [{ value: "Yes" }, { value: "Cancel" }],
			success: function (result) {
				if (result == "Yes") {
					var entity = {
						"ID": roleuserlist.pselectedRoleUserID,
					};
					roleuserapi.delete(entity);
					return;
				}
			}
		});
	}

	roleuserlist.sure = function () {
		$("#modelDialogForm").modal("hide");
		if (roleuserlist.pselecteRoleID != "") {
		}
	}

	return roleuserlist;
})()
