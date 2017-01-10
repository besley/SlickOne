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


var somain = (function () {
	somain.tablist = [];
	somain.activeTabName = "";
	somain.activeToolButtonType = "";

	function somain() {
	}

    somain.init = function(){
        var trigger = $('.hamburger'),
                isClosed = false;

        trigger.click(function () {
            hamburger_cross();
        });

        function hamburger_cross() {
            if (isClosed == true) {
                trigger.removeClass('is-open');
                trigger.addClass('is-closed');
                isClosed = false;
            } else {
                trigger.removeClass('is-closed');
                trigger.addClass('is-open');
                isClosed = true;
            }
        }

        $('[data-toggle="offcanvas"]').click(function () {
            $('#wrapper').toggleClass('toggled');
        });
    }

	//#region init, button
	somain.initPage = function () {
		//show tab
		$("#myTab").on("click", "a", function (e) {
			e.preventDefault();
			$(this).tab('show');
		});

		//remove tab
		$('#myTab').on('click', ' li a .close', function () {
			var tabId = $(this).parents('li').children('a').attr('href');

			$(this).parents('li').remove('li');
			$('#myTab a:first').tab('show');
		});

		$("#myTab").tab();
	}
	//#endregion

    somain.showTab = function (name) {
        if (name === "mydashboard") {
            $('#myTab a:first').tab('show');
            return;
        }

        var tabName = name + "Tab";
		var tab = $("a[href='#" + tabName + "_'");

		if (tab.length === 0) {
            var className = '';
            if (name.slice(-4) === "grid") 
                className = "ag-bootstrap";
            else if(name.slice(-4) === "tree")
                className = "ztree";

            
			$('#myTab').append(
			    $('<li><a href="#' + tabName + '_">' +
			    soconfig.tabname[name] +
			    '<button class="close" type="button" ' +
			    'title="Remove this page"> ×</button>' +
			    '</a></li>'));

			var newTabContent = $('<div class="tab-pane" style="height:700px;width:100%;margin-top:10px;" id="' + tabName + '_"></div>')
				.appendTo("#divTabContentContainer");
			var newTabGrid = $('<div id="' + name + '" class="' + className + '" style="width:100%;height:700px;float:left;"></div>')
				.appendTo(newTabContent);

			readGridDataByTabName(name);

			$('#myTab a:last').tab('show');

		} else {
			//tab already exist
			tab.tab('show');
		}

		//make the current tab actived
		somain.activeTabName = name;
	}

	function readGridDataByTabName(name) {
		if (name === "myrolegrid") {
			rolelist.getRoleList();
		} else if (name === "myusergrid") {
			userlist.getUserList();
		} else if (name === "myroleusertree") {
			roleusertree.getRoleUserTree();
		} else if (name === "functionpermission") {
			getFunctionPermissionList();
		} else if (name === "datapermission") {
			getDataPermissionList();
		} else if (name === "permissionquery") {
			getPermissionQueryList();
		} else if (name === "department") {
			getDepartmentList();
		} else if (name === "employee") {
			getEmployeeList();
		} else if (name === "deptemp") {
			getDeptEmpList();
		} else if (name === "myprocessgrid") {
			processlist.getProcessList();
		} else if (name === "myformgrid") {
			processlist.getFormList();
		} else if (name === "myprocessinstancegrid") {
			processlist.getProcessInstanceList();
		} else if (name === "myactivityinstancegrid") {
			processlist.getActivityInstanceList();
		} else if (name === "myloggrid") {
			processlist.getLogList();
		}
	}

	function datetimeFormatter(row, cell, value, columnDef, dataContext) {
		if (value != null && value != "") {
			return value.substring(0, 10);
		}
	}
	//#endregion

	//#region toolbutton
	somain.addrecord = function (e) {
		somain.activeToolButtonType = "add";
		openDialogForm(somain.activeToolButtonType);
	}

	somain.editrecord = function () {
		somain.activeToolButtonType = "edit";
		openDialogForm(somain.activeToolButtonType);
	}

	somain.deleterecord = function () {
		somain.activeToolButtonType = "delete";
		if (soconfig.toolbutton[somain.activeToolButtonType][somain.activeTabName]) {
			soconfig.toolbutton[somain.activeToolButtonType][somain.activeTabName]();
		}
	}

    somain.refreshrecord = function (){
        somain.activeToolButtonType = "refresh";
        readGridDataByTabName(somain.activeTabName);
    }

	function openDialogForm(buttonType) {
		var url = soconfig.toolbutton[buttonType][somain.activeTabName];

		if (url !== undefined) {
			$('#loading-indicator').show();
            
			BootstrapDialog.show({
				title: somain.activeTabName,
				message: $('<div></div>').load(url)
			});

			$('#loading-indicator').hide();
		} else {
            ;
		}
	}
	//#endregion

	return somain;
})();