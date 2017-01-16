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

var userlistdialog = (function () {
	function userlistdialog() {
	}

	userlistdialog.pselectedRoleID = "";
	userlistdialog.pselectedUserID = "";
    userlistdialog.pselectedUserDataRow = null;
    userlistdialog.onUserSelected4Adding = new slick.Event();

	//#region User DataGrid
	userlistdialog.getUserList = function () {
		jshelper.ajaxGet('api/RoleData/GetUserAll', null, function (result) {
			if (result.Status === 1) {
                var divUserGrid = document.querySelector('#myuserlistdialoggrid');
				$(divUserGrid).empty();

				var gridOptions = {
					columnDefs: [
					    { headerName: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
						{ headerName: "用户名称", field: "UserName", width: 120, cssClass: "bg-gray" },
					],
					rowSelection: 'single',
					onSelectionChanged: onSelectionChanged
				}
				
				new agGrid.Grid(divUserGrid, gridOptions);
				gridOptions.api.setRowData(result.Entity);

				function onSelectionChanged() {
					var selectedRows = gridOptions.api.getSelectedRows();
					var selectedProcessID = 0;
					selectedRows.forEach(function (selectedRow, index) {
						userlistdialog.pselectedUserID = selectedRow.ID;
						userlistdialog.pselectedUserDataRow = selectedRow;
					});
				}
			}
		});
	}

    userlistdialog.sure = function () {
        slick.trigger(userlistdialog.onUserSelected4Adding, {
            "UserID": userlistdialog.pselectedUserID,
            "RoleID": userlistdialog.pselectedRoleID
        });
	}

	userlistdialog.cancel = function () {
		userlistdialog.pselectedUserID = "";
		userlistdialog.pselectedUserDataRow = null;
	}

	return userlistdialog;
})()

