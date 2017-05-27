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

var filelist = (function () {
	function filelist() {
	}
    
    //import xml file
    filelist.initXmlImport = function(){
        var restrictedUploader = new qq.FineUploader({
            element: document.getElementById("fine-uploader-validation"),
            template: 'qq-template-validation',
            request: {
                endpoint: 'api/FineUpload/import',
                params: {
                    extraParam1: "1",
                    extraParam2: "2"
                }
            },
            thumbnails: {
                placeholders: {
                    waitingPath: 'Content/fineuploader/waiting-generic.png',
                    notAvailablePath: 'Content/fineuploader/not_available-generic.png'
                }
            },
            validation: {
                allowedExtensions: ['xml', 'txt'],
                itemLimit: 1,
                sizeLimit: 51200 // 50 kB = 50 * 1024 bytes
            },
            callbacks: {
                onComplete: function (id, fileName, result) {
                    if (result.success == true) {
                        $.msgBox({
            			    title: "Designer / Process",
            			    content: result.Message,
            			    type: "info",
            			    buttons: [{ value: "Ok" }],
            		    });
                    }
                    else {
            		    $.msgBox({
            			    title: "Designer / Process",
            			    content: result.ExceptionMessage,
            			    type: "error",
            			    buttons: [{ value: "Ok" }],
            		    });
                    }
                }
            }
        });
    }
    
    return filelist;
})()