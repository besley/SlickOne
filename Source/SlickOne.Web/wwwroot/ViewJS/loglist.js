var loglist = (function(){
    function loglist(){
    }


    //#region Log List
	loglist.getLogList = function() {
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

    return loglist;
})()