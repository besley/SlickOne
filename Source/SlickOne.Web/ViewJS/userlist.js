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

var userlist = (function () {
	function userlist() {
	}

	userlist.pselectedUserID = "";
	userlist.pselectedUserDataRow = null;

	userlist.initListForm = function () {
		$('#modelDialogForm').on('hidden', function () {
			$(this).removeData('modal').find(".modal-body").empty();;
		});
	}

	//#region User DataGrid
	userlist.getUserList = function() {
		$('#loading-indicator').show();
		jshelper.ajaxGet('api/RoleData/GetUserAll', null, function (result) {
			if (result.Status === 1) {
				var columnUser = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
                    { id: "UserName", name: "用户名称", field: "UserName", width: 120, cssClass: "bg-gray" },
				];

				var optionsUser = {
					editable: true,
					enableCellNavigation: true,
					enableColumnReorder: true,
					asyncEditorLoading: true,
					forceFitColumns: false,
					topPanelHeight: 25
				};

				var dsUser = result.Entity;

				var dvUser = new Slick.Data.DataView({ inlineFilters: true });
				var gridUser = new Slick.Grid("#myusergrid", dvUser, columnUser, optionsUser);

				dvUser.onRowsChanged.subscribe(function (e, args) {
					gridUser.invalidateRows(args.rows);
					gridUser.render();
				});

				dvUser.onRowCountChanged.subscribe(function (e, args) {
					gridUser.updateRowCount();
					gridUser.render();
				});

				dvUser.beginUpdate();
				dvUser.setItems(dsUser, "ID");
				gridUser.setSelectionModel(new Slick.RowSelectionModel());
				dvUser.endUpdate();

				//rows change event
				gridUser.onSelectedRowsChanged.subscribe(function (e, args) {
					var selectedRowIndex = args.rows[0];
					var row = dvUser.getItemByIdx(selectedRowIndex);
					if (row) {
						//marked and returned selected row info
						userlist.pselectedUserID = row.ID;
						userlist.pselectedUserDataRow = row;
					}
				});


				$('#loading-indicator').hide();
			}
		});
	}

	userlist.loadUser = function () {
		if (somain.activeToolButtonType === "edit" 
			&& userlist.pselectedUserID != "") {
			var entity = userlist.pselectedUserDataRow;
			$("#txtUserName").val(entity.UserName);
		} else {
			$("#txtUserName").val("");
		}
	}

	userlist.initEditForm = function () {
		$('#modelDialogForm').on('hidden', function () {
			$(this).removeData('modal').find(".modal-body").empty();;
		});
	}

	userlist.editUser = function () {
		var entity = userlist.pselectedUserDataRow;
		if (entity == null) {
			$.msgBox({
				title: "SlickOne / User",
				content: "请先选择用户记录！",
				type: "alert"
			});
			return false;
		}

		$("#modelDialogForm").modal({
			remote: "user/edit"
		});
	}

	userlist.saveUser = function () {
		if ($("#txtUserName").val() == ""
            || $("#txtUserCode").val() == "") {
			$.msgBox({
				title: "SlickOne / User",
				content: "请输入角色基本信息！",
				type: "alert"
			});
			return false;
		}

		var entity = {
			"ID": userlist.pselectedUserID,
			"UserName": $("#txtUserName").val(),
			"UserCode": $("#txtUserCode").val(),
		};

		userapi.save(entity);

		userlist.getUserList();

		$("#modelDialogForm").modal("hide");
	}

	userlist.delete = function () {
		$.msgBox({
			title: "Are You Sure",
			content: "确实要删除用户记录吗? ",
			type: "confirm",
			buttons: [{ value: "Yes" }, { value: "Cancel" }],
			success: function (result) {
				if (result == "Yes") {
					var entity = {
						"ID": userlist.pselectedUserID,
					};
					userapi.delete(entity);
					return;
				}
			}
		});
	}

	userlist.sure = function () {
		$("#modelRoleListForm").modal("hide");
		if (userlist.pselecteUserID != "") {
		}
	}

	return userlist;
})()

var userapi = (function () {
	function userapi() {
	}

	userapi.save = function (entity) {
		jshelper.ajaxPost('api/RoleData/SaveUser',
            JSON.stringify(entity),
            function (result) {
            	if (result.Status == 1) {
            		$.msgBox({
            			title: "SlickOne / User",
            			content: "用户记录成功保存！",
            			type: "info"
            		});
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

	userapi.delete = function (entity) {
		//delete the selected row
		jshelper.ajaxPost('api/RoleData/DeleteUser',
            JSON.stringify(entity),
            function (result) {
            	if (result.Status == 1) {
            		$.msgBox({
            			title: "SlickOne / User",
            			content: "用户记录已经删除！",
            			type: "info"
            		});

            		//refresh
            		userlist.getUserList();
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

	return userapi;
})()