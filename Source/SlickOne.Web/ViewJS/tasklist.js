/*
* SlickOne 基于Asp.NET MVC 的快速开发框架，用户在遵循MIT协议下自由修改分发，
* 也可联系作者获取商业授权和技术支持，高级特性功能由SlickOne 企业版提供，可以进行商业授权；
* 除此之外的使用则视为不正当使用，请您务必避免由此带来的商业版权纠纷。

The SlickOne project.
Copyright (C) 2016  .NET Asp.NET MVC Express Framework


*/

var tasklist = (function () {
	function tasklist() {
	}

	//#region Task List
	function getTaskList() {
		$('#loading-indicator').show();

		jshelper.ajaxGet('api/WfData/GetTaskList', null, function (result) {
			if (result.Status === 1) {
				var divTaskGrid = document.querySelector('#myTaskGrid');
				$(divTaskGrid).empty();

				var gridOptions = {
					columnDefs: [
						{ headerName: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
						{ headerName: "应用名称", field: "AppName", width: 120, cssClass: "bg-gray" },
						{ headerName: "状态", field: "TaskState", width: 40, cssClass: "bg-gray" },
						{ headerName: "类型", field: "TaskType", width: 40, cssClass: "bg-gray" },
						{ headerName: "分配用户", field: "AssignedToUserName", width: 120, cssClass: "bg-gray" },
						{
							headerName: "创建日期", field: "CreatedDateTime", width: 120, cssClass: "bg-gray",							
						},
						{ headerName: "创建用户", field: "CreatedByUserName", width: 60, cssClass: "bg-gray" },
						{
							headerName: "完成日期", field: "EndedDateTime", width: 120, cssClass: "bg-gray",
						},
						{ headerName: "完成用户", field: "EndedByUserName", width: 60, cssClass: "bg-gray" },
					],
					rowSelection: 'single',
					onSelectionChanged: onSelectionChanged
				}

				new agGrid.Grid(divTaskGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

				function onSelectionChanged() {
					var selectedRows = gridOptions.api.getSelectedRows();
					var selectedProcessID = 0;
					selectedRows.forEach(function (selectedRow, index) {
						userlist.pselectedTaskID = selectedRow.ID;
						userlist.pselectedTaskDataRow = selectedRow;
					});
				}

				$('#loading-indicator').hide();
			} else {
				$.msgBox({
					title: "Task / List",
					content: "读取任务记录失败！错误信息：" + result.Message,
					type: "error"
				});
			}
		});
	}

	return tasklist;
})()
