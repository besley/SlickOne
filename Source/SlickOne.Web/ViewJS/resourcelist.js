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

var resourcelist = (function (){
    function resourcelist(){
    }

    resourcelist.getResourceList = function(){
        jshelper.ajaxGet('api/ResourceData/GetResourceNodeAll', null, function (result) {
			if (result.Status === 1) {
				var gridOptions = {
					columnDefs: [
						{ headerName: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
						{ headerName: "资源名称", field: "ResourceName", width: 160, cssClass: "bg-gray", 
                            cellRenderer: 'group',
                            cellRendererParams: {
                                innerRenderer: innerCellRenderer
                            }
                        },
						{ headerName: "资源代码", field: "ResourceCode", width: 200, cssClass: "bg-gray" },
                        { headerName: "页面URL", field: "PageUrl", width: 120, cssClass: "bg-gray" },
						{ headerName: "标签ID", field: "TagID", width: 120, cssClass: "bg-gray" },
						{ headerName: "样式", field: "StyleClass", width: 200, cssClass: "bg-gray" },
                        { headerName: "排序", field: "OrderNum", width: 60, cssClass: "bg-gray" },
					],
					rowSelection: 'single',
                    enableColResize: true,
                    enableSorting: true,
                    animateRows: true,
                    rowHeight: 30,
                    getNodeChildDetails: function(node) {
                        if (node.group) {
                            return {
                                group: true,
                                name: node.ResourceName,
                                children: node.children,
                                expanded: node.ResourceTypeID < 3 ? "true" : "false"
                            };
                        } else {
                            return null;
                        }
                    },
                    onRowClicked: rowClicked,
				}

				var divResourceGrid = document.querySelector('#myresourcegrid');
				$(divResourceGrid).empty();

                var rowData = [];
                rowData.push(result.Entity);

				new agGrid.Grid(divResourceGrid, gridOptions);
                gridOptions.api.setRowData(rowData);

                function innerCellRenderer(params) {
                    return params.node.data.ResourceName;
                }

                function rowClicked(params) {
                    var node = params.node;
                    resourcelist.pselectedResourceID = node.data.ID;
                    resourcelist.pselectedResourceDataRow = node.data;
                }
			} else {
				$.msgBox({
					title: "Role / List",
					content: "读取资源记录失败！错误信息：" + result.Message,
					type: "error"
				});
			}
		});
    }

    return resourcelist;
})()

var resourcetree = (function () {
	function resourcetree() {
    }

    resourcetree.pmztree = null;

	function addHoverDom(treeId, treeNode) {
		if (treeNode.type === "root") return;

		var sObj = $("#" + treeNode.tId + "_span");
		var iconStr = "";
		var addBtn = null, rmvBtn = null;

		if (treeNode.type === "resource") {
			if (treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0) return;
			iconStr += "<span class='button add' id='addBtn_" + treeNode.tId + "' title='添加资源' ></span>";

			sObj.after(iconStr);
			addBtn = $("#addBtn_" + treeNode.tId);
			if (addBtn) {
				addBtn.bind("click", function () {
					openResourceDialog(treeNode.roleId);
				})
			}

			if (treeNode.editNameFlag || $("#rmvBtn_" + treeNode.tId).length > 0) return;
			iconStr = "<span class='button remove' id='rmvBtn_" + treeNode.tId
				+ "' title='删除资源' onfocus='this.blur();'></span>";

			sObj.after(iconStr);
			rmvBtn = $("#rmvBtn_" + treeNode.tId);
			if (rmvBtn) rmvBtn.bind("click", function () {
				deleteResource(treeNode.roleId);
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
					var zTree = $.fn.zTree.getZTreeObj("myresourcetree");
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

	resourcetree.getResourceTree = function () {
		var zNodes = [
			 { id: 0, pId: -1, name: "资源列表", type: "root", open: true },
		];

		jshelper.ajaxGet('api/ResourceData/GetResourceAll', null, function (result) {
			if (result.Status == 1) {
				var resourceNode = null;
				var lastResourceID = 0;
				var resourceList = result.Entity;
				$.each(resourceList, function (i, o) {
					var rid = "r" + o.ID;
					if (lastResourceID !== rid) {
						resourceNode = {
							id: rid,
							pId: 0,
							resourceId: o.ID,
							name: o.ResourceName,
							type: "resource",
							open: false
						};
						zNodes.push(resourceNode);
						lastResourceID = rid;
					}
				});

				//render zTree
				var t = $("#myresourcetree");
                resourcetree.pmztree = $.fn.zTree.init(t, getZTreeSetting(), zNodes);
			}
		});
    }

    function beforeAddUserIntoRole(event, args) {
        if (args.RoleID > 0 && args.UserID > 0) {
            resourceapi.addResource(args);
        }
    }

	function openResourceDialog(roleId) {
		userlistdialog.pselectedRoleID = roleId;

		BootstrapDialog.show({
			title: "Resource",
			message: $('<div></div>').load("Resource/Edit")
		});
	}

	function deleteResource(resourceId) {
		$.msgBox({
			title: "Are You Sure",
			content: "确实要删除资源记录吗? 请您慎重操作!!!",
			type: "confirm",
			buttons: [{ value: "Yes" }, { value: "Cancel" }],
			success: function (result) {
				if (result == "Yes") {
					var entity = {
						"ResourceID": resourceId
					};
					resourceapi.delete(entity);
					return;
				}
			}
		});
	}

	return resourcetree;
})()


var resourceapi = (function () {
	function resourceapi() {
	}

    resourceapi.addResource = function (entity) {
		jshelper.ajaxPost('api/ResourceData/SaveResource',
            JSON.stringify(entity),
            function (result) {
            	if (result.Status == 1) {
            		$.msgBox({
            			title: "SlickOne / Resource",
            			content: "已经成功保存资源数据！",
            			type: "info"
            		});

            		//refresh
            		resourcetree.getResourceTree();

            	} else {
            		$.msgBox({
            			title: "SlickOne / Resource",
            			content: result.Message,
            			type: "error",
            			buttons: [{ value: "Ok" }],
            		});
            	}
            });
	}

	resourceapi.delete = function (entity) {
		//delete the selected row
		jshelper.ajaxPost('api/ResourceData/DeleteResource',
            JSON.stringify(entity),
            function (result) {
            	if (result.Status == 1) {
            		$.msgBox({
            			title: "SlickOne / Resource",
            			content: "资源记录已经删除！",
            			type: "info"
            		});

            		//refresh
            		resourcetree.getResourceTree();
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

	return resourceapi;
})()