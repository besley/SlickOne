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

        //load left menu tree
        loadLeftMenuList();
    }

    function loadLeftMenuList(){
         jshelper.ajaxGet('api/ResourceData/GetResourceNodeAll', null, function (result) {
			if (result.Status === 1) {
                var menuData = result.Entity;

            } else {
				$.msgBox({
					title: "Menu / List",
					content: "读取菜单记录失败！错误信息：" + result.Message,
					type: "error"
				});
            }
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
			$('#myTab').append(
			    $('<li><a href="#' + tabName + '_">' +
			    soconfig.sideBar[name].tabName +
			    '<button class="close" type="button" ' +
			    'title="Remove this page"> ×</button>' +
			    '</a></li>'));

			var newTabContent = $('<div class="tab-pane" style="height:700px;width:100%;margin-top:10px;" id="' + tabName + '_"></div>')
				.appendTo("#divTabContentContainer");
            somain.activeTabControl = newTabContent;

			loadGridDaTaByTabName(newTabContent, name);

			$('#myTab a:last').tab('show');
		} else {
			//tab already exist
			tab.tab('show');
		}

		//make the current tab actived
		somain.activeTabName = name;
	}

	function loadGridDaTaByTabName(newTabContent, name) {
        $(newTabContent).empty();
        if (soconfig.sideBar[name] 
            && soconfig.sideBar[name].pageUrl !== ""){

		    //waiting...
		    showProgressBar();

            $(newTabContent).load(soconfig.sideBar[name].pageUrl);
        } else {
            window.console.log("未定义页面URL，名称信息：" + name);
        }
	}

	function datetimeFormatter(row, cell, value, columnDef, dataContext) {
		if (value != null && value != "") {
			return value.substring(0, 10);
		}
	}
	//#endregion

	//#region toolbutton
	somain.addrecord = function () {
		somain.activeToolButtonType = "add";
		openDialogForm(somain.activeToolButtonType);
	}

	somain.editrecord = function () {
		somain.activeToolButtonType = "edit";
		openDialogForm(somain.activeToolButtonType);
	}

	somain.deleterecord = function () {
		somain.activeToolButtonType = "delete";
        window.console.log(somain.activeTabName);
		if (soconfig.toolbutton[somain.activeToolButtonType][somain.activeTabName]) {
			soconfig.toolbutton[somain.activeToolButtonType][somain.activeTabName]();
		}
	}

    somain.refreshrecord = function (){
        somain.activeToolButtonType = "refresh";
        loadGridDaTaByTabName(somain.activeTabControl, somain.activeTabName);
    }

	function openDialogForm(buttonType) {
		var url = soconfig.toolbutton[buttonType][somain.activeTabName];
		if (url !== undefined) {
			$('#loading-indicator').show();
            
			BootstrapDialog.show({
				title: soconfig.sideBar[somain.activeTabName].tabName,
				message: $('<div></div>').load(url)
			});

			$('#loading-indicator').hide();
		} else {
            ;
		}
	}
	//#endregion

    //#region preparation
	function showProgressBar() {
		$('.progress .progress-bar').progressbar({
			transition_delay: 200
		});

		var $modal = $('.js-loading-bar'),
            $bar = $modal.find('.bar');

		$modal.modal('show');

		setTimeout(function () {
			$modal.modal('hide');
		}, 500);
	}
	//#endregion

	return somain;
})();