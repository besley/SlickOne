/*
*  SlickOne 企业级Web快速开发框架遵循LGPL协议，也可联系作者商业授权并获取技术支持；
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

var processlist = (function () {
    function processlist() {
    }

    //#region Process List
    processlist.getProcessList = function () {
        jshelper.ajaxGet('api/WfData/GetProcessListSimple', null, function (result) {
            if (result.Status === 1) {
            	var divProcessGrid = document.querySelector('#myprocessgrid');
				$(divProcessGrid).empty();

				var gridOptions = {
					columnDefs: [
						{ headerName: 'ID', field: 'ID', width: 50 },
						{ headerName: '流程GUID', field: 'ProcessGUID', width: 120 },
						{ headerName: '流程名称', field: 'ProcessName', width: 160 },
						{ headerName: '版本', field: 'Version', width: 40 },
						{ headerName: '状态', field: 'IsUsing', width: 60 },
						{ headerName: '创建日期', field: 'CreatedDateTime', width: 120 }
					],
					rowSelection: 'single',
					onSelectionChanged: onSelectionChanged,
				};

				new agGrid.Grid(divProcessGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

				function onSelectionChanged() {
					var selectedRows = gridOptions.api.getSelectedRows();
					selectedRows.forEach(function (selectedRow, index) {
						processlist.pselectedProcessGUID = selectedRow.ProcessGUID;
						processlist.pselectedProcessDataRow = selectedRow;
					});
				}
            } else {
            	$.msgBox({
            		title: "Process / List",
            		content: "读取流程定义记录失败！错误信息：" + result.Message,
            		type: "error"
            	});
            }
        });
    }
    //#endregion

    //#region ProcessInstance List
	processlist.getProcessInstanceList = function() {
		jshelper.ajaxGet('api/WfData/GetProcessInstanceList', null, function (result) {
			if (result.Status === 1) {
                var divProcessGrid = document.querySelector('#myprocessinstancegrid');
				$(divProcessGrid).empty();

				var gridOptions = {
					columnDefs: [
						{ headerName: 'ID', field: 'ID', width: 50 },
						{ headerName: '流程名称', field: 'ProcessName', width: 160 },
						{ headerName: '应用名称', field: 'AppName', width: 120 },
						{ headerName: '状态', field: 'ProcessState', width: 40 },
						{ headerName: '创建日期', field: 'CreatedDateTime', width: 120 },
						{ headerName: '创建用户', field: 'CreatedByUserName', width: 60 },
                        { headerName: '完成日期', field: 'EndedDateTime', width: 120 },
                        { headerName: '完成用户', field: 'EndedByUserName', width: 60 },
					],
					rowSelection: 'single',
					onSelectionChanged: onSelectionChanged,
				};

				new agGrid.Grid(divProcessGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

				function onSelectionChanged() {
					var selectedRows = gridOptions.api.getSelectedRows();
					selectedRows.forEach(function (selectedRow, index) {
						processinstancelist.pselectedProcessInstanceID = selectedRow.ID;
						processinstancelist.pselectedProcessInstanceDataRow = selectedRow;
					});
				}
            } else {
            	$.msgBox({
            		title: "ProcessInstance / List",
            		content: "读取流程实例记录失败！错误信息：" + result.Message,
            		type: "error"
            	});
            }
		});
	}
	//#endregion

    //#region ActivityInstance List
    processlist.getActivityInstanceList = function() {
		jshelper.ajaxGet('api/WfData/GetActivityInstanceList', null, function (result) {
			if (result.Status === 1) {
                var divActivityGrid = document.querySelector('#myactivityinstancegrid');
				$(divActivityGrid).empty();

				var gridOptions = {
					columnDefs: [
						{ headerName: 'ID', field: 'ID', width: 50 },
						{ headerName: '应用名称', field: 'AppName', width: 120 },
						{ headerName: '活动名称', field: 'ActivityName', width: 60 },

						{ headerName: '状态', field: 'ActivityState', width: 40 },
                        { headerName: '类型', field: 'ActivityType', width: 40 },
                        { headerName: '分配用户', field: 'AssignedToUserNames', width: 160 },
						{ headerName: '创建日期', field: 'CreatedDateTime', width: 120 },
						{ headerName: '创建用户', field: 'CreatedByUserName', width: 60 },

                        { headerName: '完成日期', field: 'EndedDateTime', width: 120 },
                        { headerName: '完成用户', field: 'EndedByUserName', width: 60 },
					],
					rowSelection: 'single',
					onSelectionChanged: onSelectionChanged,
				};

				new agGrid.Grid(divActivityGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

				function onSelectionChanged() {
					var selectedRows = gridOptions.api.getSelectedRows();
					selectedRows.forEach(function (selectedRow, index) {
						activityinstancelist.pselectedActivityInstanceID = selectedRow.ID;
						activityinstancelist.pselectedActivityInstanceDataRow = selectedRow;
					});
				}
            } else {
            	$.msgBox({
            		title: "ActivityInstance / List",
            		content: "读取活动实例记录失败！错误信息：" + result.Message,
            		type: "error"
            	});
            }
		});
	}


    //#region Form List
    processlist.getFormList = function () {
        jshelper.ajaxGet('api/WfData/GetFormListSimple', null, function (result) {
            if (result.Status === 1) {
            	var divFormGrid = document.querySelector('#myformgrid');
				$(divFormGrid).empty();

				var gridOptions = {
					columnDefs: [
						{ headerName: 'ID', field: 'ID', width: 50 },
						{ headerName: '表单名称', field: 'EntityName', width: 120 },
						{ headerName: '表单标题', field: 'EntityTitle', width: 160 },
						{ headerName: '表单编码', field: 'EntityCode', width: 80 },
						{ headerName: '描述', field: 'Description', width: 160 },
						{ headerName: '创建日期', field: 'CreatedDate', width: 120 }
					],
					rowSelection: 'single',
					onSelectionChanged: onSelectionChanged,
				};

				new agGrid.Grid(divFormGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

				function onSelectionChanged() {
					var selectedRows = gridOptions.api.getSelectedRows();
					selectedRows.forEach(function (selectedRow, index) {
						processlist.pselectedFormID = selectedRow.ID;
						processlist.pselecteFormDataRow = selectedRow;
					});
				}
            } else {
            	$.msgBox({
            		title: "Form / List",
            		content: "读取表单定义记录失败！错误信息：" + result.Message,
            		type: "error"
            	});
            }
        });
    }
    //#endregion

    //#region Log List
	processlist.getLogList = function() {
		jshelper.ajaxGet('api/WfData/GetLogList', null, function (result) {
			if (result.Status === 1) {
                var divLogGrid = document.querySelector('#myloggrid');
				$(divLogGrid).empty();

				var gridOptions = {
					columnDefs: [
						{ headerName: 'ID', field: 'ID', width: 50 },
						{ headerName: '类型', field: 'EventTypeID', width: 60 },
						{ headerName: '优先级', field: 'Priority', width: 60 },
						{ headerName: '紧急', field: 'Severity', width: 60 },
						{ headerName: '标题', field: 'Title', width: 160 },
						{ headerName: '信息', field: 'Message', width: 160 },
						{ headerName: '创建日期', field: 'Timestamp', width: 120 }
					],
					rowSelection: 'single',
					onSelectionChanged: onSelectionChanged,
				};

				new agGrid.Grid(divLogGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

				function onSelectionChanged() {
					var selectedRows = gridOptions.api.getSelectedRows();
					selectedRows.forEach(function (selectedRow, index) {
						processlist.pselectedLogID = selectedRow.ID;
						processlist.pselecteLogDataRow = selectedRow;
					});
				}
            } else {
            	$.msgBox({
            		title: "Form / List",
            		content: "读取日志记录失败！错误信息：" + result.Message,
            		type: "error"
            	});
            }
		});
	}
	//#endregion


    return processlist;
})()
