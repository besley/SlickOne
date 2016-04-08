/*
* Slickflow 工作流引擎遵循LGPL协议，也可联系作者商业授权并获取技术支持；
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

var processlist = (function () {
    function processlist() {
    }

    processlist.pselectedProcessGUID = "";
    processlist.pselectedProcessDataRow = null;

    processlist.initListForm = function () {
        $('#modelProcessListForm').on('hidden', function () {
            $(this).removeData('modal').find(".modal-body").empty();;
        });
    }

    //#region Process DataGrid
    processlist.getProcessList = function () {
        $('#loading-indicator').show();
        jshelper.ajaxGet('../api/WfProcess/GetProcessListSimple', null, function (result) {
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
                var gridProcess = new Slick.Grid("#myProcessGrid", dvProcess, columnProcess, optionsProcess);

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

                gridProcess.onDblClick.subscribe(function (e, args) {
                    processlist.editProcess();
                });

                $('#loading-indicator').hide();
            }
        });

        function datetimeFormatter(row, cell, value, columnDef, dataContext) {
            if (value != null && value != "") {
                return value.substring(0, 10);
            }
        }
    }

    return processlist;
})()