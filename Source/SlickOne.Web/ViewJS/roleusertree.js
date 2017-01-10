/*
* SlickOne WEB快速开发框架遵循LGPL协议，也可联系作者商业授权并获取技术支持；
* 除此之外的使用则视为不正当使用，请您务必避免由此带来的商业版权纠纷。

The SlickOne project.
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

var roleusertree = (function () {
	function roleusertree() {
    }

    roleusertree.pmztree = null;

	function addHoverDom(treeId, treeNode) {
		if (treeNode.type === "root") return;

		var sObj = $("#" + treeNode.tId + "_span");
		var iconStr = "";
		var addBtn = null, rmvBtn = null;

		if (treeNode.type === "role") {
			if (treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0) return;
			iconStr += "<span class='button add' id='addBtn_" + treeNode.tId + "' title='添加用户' ></span>";

			sObj.after(iconStr);
			addBtn = $("#addBtn_" + treeNode.tId);
			if (addBtn) {
				addBtn.bind("click", function () {
					openUserDialog(treeNode.roleId);
				})
			}

			if (treeNode.editNameFlag || $("#rmvBtn_" + treeNode.tId).length > 0) return;
			iconStr = "<span class='button remove' id='rmvBtn_" + treeNode.tId
				+ "' title='全部删除用户' onfocus='this.blur();'></span>";

			sObj.after(iconStr);
			rmvBtn = $("#rmvBtn_" + treeNode.tId);
			if (rmvBtn) rmvBtn.bind("click", function () {
				deleteRoleUserAll(treeNode.roleId);
			})
		} else if (treeNode.type === "user") {
			if (treeNode.editNameFlag || $("#rmvBtn_" + treeNode.tId).length > 0) return;
			iconStr = "<span class='button remove' id='rmvBtn_" + treeNode.tId
				+ "' title='删除用户' onfocus='this.blur();'></span>";

			sObj.after(iconStr);
			rmvBtn = $("#rmvBtn_" + treeNode.tId);
			if (rmvBtn) rmvBtn.bind("click", function () {
				deleteRoleUser(treeNode.userId, treeNode.roleId);
			})
		}

	};

	function removeHoverDom(treeId, treeNode) {
		$("#addBtn_" + treeNode.tId).unbind().remove();
		$("#rmvBtn_" + treeNode.tId).unbind().remove();
	};

	function getZTreeSetting() {
		var setting = {
			check: {
				enable: true
			},
			view: {
				addHoverDom: addHoverDom,
				removeHoverDom: removeHoverDom,
				dblClickExpand: false,
				showLine: true,
				selectedMulti: false
			},
			data: {
				simpleData: {
					enable: true,
					idKey: "id",
					pIdKey: "pId",
					rootPId: ""
				}
			},
			callback: {
				beforeClick: function (treeId, treeNode) {
					var zTree = $.fn.zTree.getZTreeObj("myroleusertree");
					if (treeNode.isParent) {
						zTree.expandNode(treeNode);
						return false;
					} else {
						return true;
					}
				},
				onClick: function (event, treeId, treeNode) {
					;
				},
			}
		};
		return setting;
	}

	roleusertree.getRoleUserTree = function () {
		var zNodes = [
			 { id: 0, pId: -1, name: "角色列表", type: "root", open: true },
		];

		jshelper.ajaxGet('api/RoleData/GetRoleUserAll', null, function (result) {
			if (result.Status == 1) {
				var roleNode = null, userNode = null;
				var lastRoleID = 0;
				var roleUserList = result.Entity;
				$.each(roleUserList, function (i, o) {
					var rid = "r" + o.RoleID;
					if (lastRoleID !== rid) {
						roleNode = {
							id: rid,
							pId: 0,
							roleId: o.RoleID,
							name: o.RoleName,
							type: "role",
							open: false
						};
						zNodes.push(roleNode);
						lastRoleID = rid;
					}

					if (o.ID !== 0) {
						userNode = {
							id: "ru" + o.ID,
							pId: lastRoleID,
							userId: o.UserID,
							name: o.UserName,
							roleId: o.RoleID,
							type: "user",
							open: false
						};

						zNodes.push(userNode);
					}
				});

				//render zTree
				var t = $("#myroleusertree");
                roleusertree.pmztree = $.fn.zTree.init(t, getZTreeSetting(), zNodes);
                userlistdialog.onUserSelected4Adding.subscribe(beforeAddUserIntoRole);
			}
		});
    }

    function beforeAddUserIntoRole(event, args) {
        if (args.RoleID > 0 && args.UserID > 0) {
            roleuserapi.addRoleUser(args);
        }
    }

	function openUserDialog(roleId) {
		userlistdialog.pselectedRoleID = roleId;

		BootstrapDialog.show({
			title: "User",
			message: $('<div></div>').load("User/List")
		});
	}

	function deleteRoleUserAll(roleId) {
		$.msgBox({
			title: "Are You Sure",
			content: "确实要删除角色下的全部用户记录吗? 请您慎重操作!!!",
			type: "confirm",
			buttons: [{ value: "Yes" }, { value: "Cancel" }],
			success: function (result) {
				if (result == "Yes") {
					var entity = {
						"UserID": -1,
						"RoleID": roleId
					};
					roleuserapi.delete(entity);
					return;
				}
			}
		});
	}

	function deleteRoleUser(userId, roleId) {
		$.msgBox({
			title: "Are You Sure",
			content: "确实要删除角色下的用户记录吗? ",
			type: "confirm",
			buttons: [{ value: "Yes" }, { value: "Cancel" }],
			success: function (result) {
				if (result == "Yes") {
					var entity = {
						"UserID": userId,
						"RoleID": roleId
					};
					roleuserapi.delete(entity);
					return;
				}
			}
		});
    }

	return roleusertree;
})()


var roleuserapi = (function () {
	function roleuserapi() {
	}

    roleuserapi.addRoleUser = function (entity) {
		jshelper.ajaxPost('api/RoleData/AddRoleUser',
            JSON.stringify(entity),
            function (result) {
            	if (result.Status == 1) {
            		$.msgBox({
            			title: "SlickOne / Role",
            			content: "已经成功添加用户到该角色！",
            			type: "info"
            		});

            		//refresh
            		roleusertree.getRoleUserTree();

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

	roleuserapi.delete = function (entity) {
		//delete the selected row
		jshelper.ajaxPost('api/RoleData/DeleteRoleUser',
            JSON.stringify(entity),
            function (result) {
            	if (result.Status == 1) {
            		$.msgBox({
            			title: "SlickOne / Role",
            			content: "角色下的用户记录已经删除！",
            			type: "info"
            		});

            		//refresh
            		roleusertree.getRoleUserTree();
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

	return roleuserapi;
})()