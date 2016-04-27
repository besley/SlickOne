var somain = (function () {
	somain.tablist = [];
	somain.activeTab = null;
	somain.tabname = [];
	somain.tabname["process"] = "流程记录";
	somain.tabname["form"] = "表单记录";
	somain.tabname["processinstance"] = "流程实例";
	somain.tabname["activityinstance"] = "活动实例";
	somain.tabname["task"] = "任务记录";
	somain.tabname["log"] = "系统日志";

	function somain() {
	}

	somain.initPage = function () {
		//show tab
		$("#myTab").on("click", "a", function (e) {
			e.preventDefault();
			$(this).tab('show');
		});

		//remove tab
		$('#myTab').on('click', ' li a .close', function () {
			var tabId = $(this).parents('li').children('a').attr('href');
			//window.console.log(tabId);

			$(this).parents('li').remove('li');
			$('#myTab a:first').tab('show');
		});

		$("#myTab").tab();
	}

	//#region create tab dynamicaly
	//somain.createTab = function (name) {
	//	$("#tabDesktop").removeClass("active");

	//	var newTab = null;
	//	if (somain.activeTab !== null) {
	//		somain.activeTab.removeClass("active");
	//	}

	//	if (somain.tablist[name] === undefined) {
	//		//create new tab
	//		var newTab = $('<li><a href="#' + name + '_" data-toggle="tab">' + somain.tabname[name]
	//			+ '<div class="mdiDivClose" style="background-position: center top;"></div></a></li>').appendTo("#tabSet");
	//		newTab.addClass("active");
	//		somain.activeTab = newTab;
	//		somain.tablist[name] = 1;

	//		//render tab content
	//		var newTabContent = $('<div class="tab-pane" style="height:600px;width:100%;" id="' + name + '_"></div>')
	//			.appendTo("#divTabContentContainer");
	//		var newTabGrid = $('<div id="' + 'my' + name + 'grid' + '" class="grid-container" style="width:100%;height:300px;float:left;"></div>')
	//			.appendTo(newTabContent);
				
	//		getTabGridDataByName(name);
	//	}
	//}
	//#endregion

	somain.showTab = function (name) {
		var tab = $("a[href='#" + name + "_'");
		if (tab.length === 0) {
			$('#myTab').append(
			$('<li><a href="#' + name + '_">' +
			somain.tabname[name] +
			'<button class="close" type="button" ' +
			'title="Remove this page">×</button>' +
			'</a></li>'));

			$('#myTab a:last').tab('show');

			readGridDataByTabName(name);
		}
	}

	function readGridDataByTabName(name) {
		if (name === "process") {
			getProcessList();
		} else if (name === "form") {
			getFormList();
		} else if (name === "processinstance") {
			getProcessInstanceList();
		} else if (name === "activityinstance") {
			getActivityInstanceList();
		} else if (name === "task") {
			getTaskList();
		} else if (name === "log") {
			getLogList();
		}
	}

	function datetimeFormatter(row, cell, value, columnDef, dataContext) {
		if (value != null && value != "") {
			return value.substring(0, 10);
		}
	}

	//#region Process List
	function getProcessList() {
		$('#loading-indicator').show();
		jshelper.ajaxGet('api/WfData/GetProcessListSimple', null, function (result) {
			if (result.Status === 1) {
				var columnProcess = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
                    { id: "ProcessGUID", name: "流程GUID", field: "ProcessGUID", width: 120, cssClass: "bg-gray" },
                    { id: "ProcessName", name: "流程名称", field: "ProcessName", width: 160, cssClass: "bg-gray" },
                    { id: "Version", name: "版本", field: "Version", width: 40, cssClass: "bg-gray" },
                    { id: "IsUsing", name: "使用状态", field: "IsUsing", width: 60, cssClass: "bg-gray" },
                    {
                    	id: "CreatedDateTime", name: "创建日期", field: "CreatedDateTime", width: 120, cssClass: "bg-gray",
                    	formatter: datetimeFormatter
                    },
				];

				var optionsProcess = {
					editable: true,
					enableCellNavigation: true,
					enableColumnReorder: true,
					asyncEditorLoading: true,
					forceFitColumns: false,
					topPanelHeight: 25
				};

				var dsProcess = result.Entity;

				var dvProcess = new Slick.Data.DataView({ inlineFilters: true });
				var gridProcess = new Slick.Grid("#myprocessgrid", dvProcess, columnProcess, optionsProcess);

				dvProcess.onRowsChanged.subscribe(function (e, args) {
					gridProcess.invalidateRows(args.rows);
					gridProcess.render();
				});

				dvProcess.onRowCountChanged.subscribe(function (e, args) {
					gridProcess.updateRowCount();
					gridProcess.render();
				});

				dvProcess.beginUpdate();
				dvProcess.setItems(dsProcess, "ID");
				gridProcess.setSelectionModel(new Slick.RowSelectionModel());
				dvProcess.endUpdate();

				//rows change event
				gridProcess.onSelectedRowsChanged.subscribe(function (e, args) {
					var selectedRowIndex = args.rows[0];
					var row = dvProcess.getItemByIdx(selectedRowIndex);
					if (row) {
						//marked and returned selected row info
						processlist.pselectedProcessGUID = row.ProcessGUID;
						processlist.pselectedProcessDataRow = row;
					}
				});


				$('#loading-indicator').hide();
			}
		});
	}
	//#endregion

	//#region Form List
	function getFormList() {
		$('#loading-indicator').show();

		jshelper.ajaxGet("api/FormMaster/GetEntityDefList2", null, function (result) {
			if (result.Status == 1) {
				var columnEntityDef = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
                    { id: "EntityTitle", name: "标题", field: "EntityTitle", width: 120, cssClass: "bg-gray" },
                    { id: "EntityName", name: "表单名称", field: "EntityName", width: 120, cssClass: "bg-gray" },
                    { id: "EntityCode", name: "表单编码", field: "EntityCode", width: 160, cssClass: "bg-gray" },
                    { id: "Description", name: "描述", field: "Description", width: 120, cssClass: "bg-gray" },
                    { id: "CreatedDate", name: "创建时间", field: "CreatedDate", width: 200, cssClass: "bg-gray", formatter: datetimeFormatter }
				];

				var optionsEntityDef = {
					editable: true,
					enableCellNavigation: true,
					enableColumnReorder: true,
					asyncEditorLoading: true,
					forceFitColumns: false,
					topPanelHeight: 25
				};

				var dsEntityDef = result.Entity;
				var dvEntityDef = new Slick.Data.DataView({ inlineFilters: true });
				var gridEntityDef = new Slick.Grid("#myformgrid", dvEntityDef, columnEntityDef, optionsEntityDef);

				dvEntityDef.onRowsChanged.subscribe(function (e, args) {
					gridEntityDef.invalidateRows(args.rows);
					gridEntityDef.render();

				});

				dvEntityDef.onRowCountChanged.subscribe(function (e, args) {
					gridEntityDef.updateRowCount();
					gridEntityDef.render();
				});

				dvEntityDef.beginUpdate();
				dvEntityDef.setItems(dsEntityDef, "ID");
				gridEntityDef.setSelectionModel(new Slick.RowSelectionModel());
				dvEntityDef.endUpdate();

				gridEntityDef.onSelectedRowsChanged.subscribe(function (e, args) {
					var selectionRowIndex = args.rows[0];
					var row = dvEntityDef.getItemByIdx(selectionRowIndex);

					if (row) {
						;
					}
				});

				$('#loading-indicator').hide();
			} else {
				$.msgBox({
					title: "Master / Entity",
					content: "读取表单定义记录失败！错误信息：" + result.Message,
					type: "error"
				});
			}
		});
	}
	//#endregion

	//#region ProcessInstance List
	function getProcessInstanceList() {
		$('#loading-indicator').show();
		jshelper.ajaxGet('api/WfData/GetProcessInstanceList', null, function (result) {
			if (result.Status === 1) {
				var columnProcess = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
                    { id: "ProcessName", name: "流程名称", field: "ProcessName", width: 160, cssClass: "bg-gray" },
					{ id: "AppName", name: "应用名称", field: "AppName", width: 120, cssClass: "bg-gray" },
                    { id: "ProcessState", name: "状态", field: "ProcessState", width: 40, cssClass: "bg-gray" },
					{
					    id: "CreatedDateTime", name: "创建日期", field: "CreatedDateTime", width: 120, cssClass: "bg-gray",
					    formatter: datetimeFormatter
					},
                    { id: "CreatedByUserName", name: "创建用户", field: "CreatedByUserName", width: 60, cssClass: "bg-gray" },
					{
						id: "EndedDateTime", name: "完成日期", field: "EndedDateTime", width: 120, cssClass: "bg-gray",
					    formatter: datetimeFormatter
					},
					{ id: "EndedByUserName", name: "完成用户", field: "EndedByUserName", width: 60, cssClass: "bg-gray" },
				];

				var optionsProcess = {
					editable: true,
					enableCellNavigation: true,
					enableColumnReorder: true,
					asyncEditorLoading: true,
					forceFitColumns: false,
					topPanelHeight: 25
				};

				var dsProcess = result.Entity;

				var dvProcess = new Slick.Data.DataView({ inlineFilters: true });
				var gridProcess = new Slick.Grid("#myprocessinstancegrid", dvProcess, columnProcess, optionsProcess);

				dvProcess.onRowsChanged.subscribe(function (e, args) {
					gridProcess.invalidateRows(args.rows);
					gridProcess.render();
				});

				dvProcess.onRowCountChanged.subscribe(function (e, args) {
					gridProcess.updateRowCount();
					gridProcess.render();
				});

				dvProcess.beginUpdate();
				dvProcess.setItems(dsProcess, "ID");
				gridProcess.setSelectionModel(new Slick.RowSelectionModel());
				dvProcess.endUpdate();

				//rows change event
				gridProcess.onSelectedRowsChanged.subscribe(function (e, args) {
					var selectedRowIndex = args.rows[0];
					var row = dvProcess.getItemByIdx(selectedRowIndex);
					if (row) {
						//marked and returned selected row info
						processlist.pselectedProcessGUID = row.ProcessGUID;
						processlist.pselectedProcessDataRow = row;
					}
				});


				$('#loading-indicator').hide();
			}
		});
	}
	//#endregion

	//#region ActivityInstance List
	function getActivityInstanceList() {
		$('#loading-indicator').show();
		jshelper.ajaxGet('api/WfData/GetActivityInstanceList', null, function (result) {
			if (result.Status === 1) {
				var columnProcess = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
					{ id: "AppName", name: "应用名称", field: "AppName", width: 120, cssClass: "bg-gray" },
					{ id: "ActivityName", name: "应用名称", field: "ActivityName", width: 120, cssClass: "bg-gray" },
					{ id: "ActivityState", name: "状态", field: "ActivityState", width: 40, cssClass: "bg-gray" },
					{ id: "ActivityType", name: "类型", field: "ActivityType", width: 40, cssClass: "bg-gray" },
					{ id: "AssignedToUserNames", name: "分配用户", field: "AssignedToUserNames", width: 120, cssClass: "bg-gray" },
                    
					{
						id: "CreatedDateTime", name: "创建日期", field: "CreatedDateTime", width: 120, cssClass: "bg-gray",
						formatter: datetimeFormatter
					},
                    { id: "CreatedByUserName", name: "创建用户", field: "CreatedByUserName", width: 60, cssClass: "bg-gray" },
					{
						id: "EndedDateTime", name: "完成日期", field: "EndedDateTime", width: 120, cssClass: "bg-gray",
						formatter: datetimeFormatter
					},
					{ id: "EndedByUserName", name: "完成用户", field: "EndedByUserName", width: 60, cssClass: "bg-gray" },
				];

				var optionsProcess = {
					editable: true,
					enableCellNavigation: true,
					enableColumnReorder: true,
					asyncEditorLoading: true,
					forceFitColumns: false,
					topPanelHeight: 25
				};

				var dsProcess = result.Entity;

				var dvProcess = new Slick.Data.DataView({ inlineFilters: true });
				var gridProcess = new Slick.Grid("#myactivityinstancegrid", dvProcess, columnProcess, optionsProcess);

				dvProcess.onRowsChanged.subscribe(function (e, args) {
					gridProcess.invalidateRows(args.rows);
					gridProcess.render();
				});

				dvProcess.onRowCountChanged.subscribe(function (e, args) {
					gridProcess.updateRowCount();
					gridProcess.render();
				});

				dvProcess.beginUpdate();
				dvProcess.setItems(dsProcess, "ID");
				gridProcess.setSelectionModel(new Slick.RowSelectionModel());
				dvProcess.endUpdate();

				//rows change event
				gridProcess.onSelectedRowsChanged.subscribe(function (e, args) {
					var selectedRowIndex = args.rows[0];
					var row = dvProcess.getItemByIdx(selectedRowIndex);
					if (row) {
						//marked and returned selected row info
						processlist.pselectedProcessGUID = row.ProcessGUID;
						processlist.pselectedProcessDataRow = row;
					}
				});


				$('#loading-indicator').hide();
			}
		});
	}
	//#endregion

	//#region Task List
	function getTaskList() {
		$('#loading-indicator').show();
		jshelper.ajaxGet('api/WfData/GetTaskList', null, function (result) {
			if (result.Status === 1) {
				var columnProcess = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
					{ id: "AppName", name: "应用名称", field: "AppName", width: 120, cssClass: "bg-gray" },
					{ id: "TaskState", name: "状态", field: "TaskState", width: 40, cssClass: "bg-gray" },
					{ id: "TaskType", name: "类型", field: "TaskType", width: 40, cssClass: "bg-gray" },
					{ id: "AssignedToUserName", name: "分配用户", field: "AssignedToUserName", width: 120, cssClass: "bg-gray" },

					{
						id: "CreatedDateTime", name: "创建日期", field: "CreatedDateTime", width: 120, cssClass: "bg-gray",
						formatter: datetimeFormatter
					},
                    { id: "CreatedByUserName", name: "创建用户", field: "CreatedByUserName", width: 60, cssClass: "bg-gray" },
					{
						id: "EndedDateTime", name: "完成日期", field: "EndedDateTime", width: 120, cssClass: "bg-gray",
						formatter: datetimeFormatter
					},
					{ id: "EndedByUserName", name: "完成用户", field: "EndedByUserName", width: 60, cssClass: "bg-gray" },
				];

				var optionsProcess = {
					editable: true,
					enableCellNavigation: true,
					enableColumnReorder: true,
					asyncEditorLoading: true,
					forceFitColumns: false,
					topPanelHeight: 25
				};

				var dsProcess = result.Entity;

				var dvProcess = new Slick.Data.DataView({ inlineFilters: true });
				var gridProcess = new Slick.Grid("#mytaskgrid", dvProcess, columnProcess, optionsProcess);

				dvProcess.onRowsChanged.subscribe(function (e, args) {
					gridProcess.invalidateRows(args.rows);
					gridProcess.render();
				});

				dvProcess.onRowCountChanged.subscribe(function (e, args) {
					gridProcess.updateRowCount();
					gridProcess.render();
				});

				dvProcess.beginUpdate();
				dvProcess.setItems(dsProcess, "ID");
				gridProcess.setSelectionModel(new Slick.RowSelectionModel());
				dvProcess.endUpdate();

				//rows change event
				gridProcess.onSelectedRowsChanged.subscribe(function (e, args) {
					var selectedRowIndex = args.rows[0];
					var row = dvProcess.getItemByIdx(selectedRowIndex);
					if (row) {
						//marked and returned selected row info
						processlist.pselectedProcessGUID = row.ProcessGUID;
						processlist.pselectedProcessDataRow = row;
					}
				});


				$('#loading-indicator').hide();
			}
		});
	}
	//#endregion

	//#region Log List
	function getLogList() {
		$('#loading-indicator').show();
		jshelper.ajaxGet('api/WfData/GetLogList', null, function (result) {
			if (result.Status === 1) {
				var columnProcess = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
                    { id: "EventTypeID", name: "类型", field: "EventTypeID", width: 40, cssClass: "bg-gray" },
					{ id: "Priority", name: "优先级", field: "Priority", width: 40, cssClass: "bg-gray" },
                    { id: "Severity", name: "紧急", field: "Severity", width: 40, cssClass: "bg-gray" },
					{ id: "Title", name: "标题", field: "Title", width: 120, cssClass: "bg-gray" },
					{ id: "Message", name: "消息", field: "Message", width: 160, cssClass: "bg-gray" },
					{
						id: "Timestamp", name: "创建时间", field: "Timestamp", width: 120, cssClass: "bg-gray",
						formatter: datetimeFormatter
					},
				];

				var optionsProcess = {
					editable: true,
					enableCellNavigation: true,
					enableColumnReorder: true,
					asyncEditorLoading: true,
					forceFitColumns: false,
					topPanelHeight: 25
				};

				var dsProcess = result.Entity;

				var dvProcess = new Slick.Data.DataView({ inlineFilters: true });
				var gridProcess = new Slick.Grid("#myloggrid", dvProcess, columnProcess, optionsProcess);

				dvProcess.onRowsChanged.subscribe(function (e, args) {
					gridProcess.invalidateRows(args.rows);
					gridProcess.render();
				});

				dvProcess.onRowCountChanged.subscribe(function (e, args) {
					gridProcess.updateRowCount();
					gridProcess.render();
				});

				dvProcess.beginUpdate();
				dvProcess.setItems(dsProcess, "ID");
				gridProcess.setSelectionModel(new Slick.RowSelectionModel());
				dvProcess.endUpdate();

				//rows change event
				gridProcess.onSelectedRowsChanged.subscribe(function (e, args) {
					var selectedRowIndex = args.rows[0];
					var row = dvProcess.getItemByIdx(selectedRowIndex);
					if (row) {
						//marked and returned selected row info
						processlist.pselectedProcessGUID = row.ProcessGUID;
						processlist.pselectedProcessDataRow = row;
					}
				});


				$('#loading-indicator').hide();
			}
		});
	}
	//#endregion

	return somain;
})();