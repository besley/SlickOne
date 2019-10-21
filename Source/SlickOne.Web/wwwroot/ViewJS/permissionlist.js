var permissionlist = (function () {
    function permissionlist() {
    }

    permissionlist.getRoleList = function(){
        jshelper.ajaxGet('api/RoleData/GetRoleAll', null, function (result) {
			if (result.Status === 1) {
				var divRolePermissionGrid = document.querySelector('#myrolepermissiongrid');
				$(divRolePermissionGrid).empty();

				var gridOptions = {
					columnDefs: [
						{ headerName: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
						{ headerName: "角色名称", field: "RoleName", width: 140, cssClass: "bg-gray" },
						{ headerName: "角色代码", field: "RoleCode", width: 140, cssClass: "bg-gray" }
					],
					rowSelection: 'single',
                    onRowClicked: rowClicked,
				}

				new agGrid.Grid(divRolePermissionGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

                function rowClicked(params) {
                    var node = params.node;
                    permissionlist.pselectedRoleID = node.data.ID;
                    permissionlist.pselectedResourceDataRow = node.data;

                    //load role resource tree view
                    getRoleResourceTree(permissionlist.pselectedRoleID);
                }

			} else {
				$.msgBox({
					title: "Permission / List",
					content: "读取角色记录失败！错误信息：" + result.Message,
					type: "error"
				});
			}
		});
    }

    function getRoleResourceTree(roleID){  
        $("#treeContainer").show();    
        var query = {"RoleID": roleID};

        jshelper.ajaxPost('api/ResourceData/GetRoleResourceList', JSON.stringify(query), function (result) {
			if (result.Status === 1) {
				var zNodes = [
			         { id: 0, pId: -1, name: "权限列表", type: "root", open: true },
		        ];
                var permissionNode = null;
                var resourceList = result.Entity;
                $.each(resourceList, function(i, o){
                    permissionNode = {
                        id: o.ID,
                        pId: o.ParentID,
                        name: o.ResourceName,
                        open: true
                    };
                    zNodes.push(permissionNode);
                });

				//render zTree
				var t = $("#myroleresourcetree");
                permissionlist.pmztree = $.fn.zTree.init(t, getZTreeSetting(), zNodes);
			} else {
				$.msgBox({
					title: "Permission / List",
					content: "读取资源权限数据失败！错误信息：" + result.Message,
					type: "error"
				});
			}
		});
    }

    function getZTreeSetting() {
		var setting = {
			check: {
				enable: true
			},
			view: {
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
					var zTree = $.fn.zTree.getZTreeObj("myroleresourcetree");
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

    permissionlist.savePermission = function(){
        var roleID = permissionlist.pselectedRoleID;
        var entity = {"RoleID": roleID};

        jshelper.ajaxPost('api/ResourceData/SaveRoleResource', JSON.stringify(entity), function (result) {
            if (result.Status === 1) {
				$.msgBox({
					title: "Permission / List",
					content: "保存角色资源权限数据成功(DEMO)!",
					type: "info"
				});
			} else {
				$.msgBox({
					title: "Permission / List",
					content: "保存角色资源权限数据失败！错误信息：" + result.Message,
					type: "error"
				});
			}
        });
    }

    permissionlist.cancel = function(){
        $("#treeContainer").hide(); 
    }

    return permissionlist;
})()