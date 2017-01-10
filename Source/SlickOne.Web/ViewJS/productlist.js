var productlist = (function () {
    productlist.mselectedProductID = 0;
    productlist.mselectedProductRow = null;

    function productlist() {
    }

    productlist.load = function () {
        jshelper.ajaxGet("/api/product/GetProductList", null, function (result) {
            if (result.Status == 1) {
                fillData(result.Entity);
            } else {
                $.msgBox({
                    title: "Product / List",
                    content: result.Message,
                    type: "error",
                    buttons: [{ value: "Ok" }],
                });
            }
        });
    }

    productlist.query = function () {
        var query = {};
        query.ProductType = $("#ddlProductTypeQuery").val();
        if (query.ProductType == null || query.ProductType == "default") return;


        jshelper.ajaxPost("/soneweb/api/product/query", JSON.stringify(query), function (result) {
            if (result.Status == 1) {
                fillData(result.Entity);

                $("#modelProductQueryForm").modal("hide");
            } else {
                $.msgBox({
                    title: "Product / Query",
                    content: result.Message,
                    type: "error",
                    buttons: [{ value: "Ok" }],
                });
            }
        });

        function datetimeFormatter(row, cell, value, columnDef, dataContext) {
            if (value != null && value != "") {
                return value.substring(0, 10);
            }
        }
    }

    function fillData(dataSource) {
    	$('#loading-indicator').show();

        var divProductGrid = document.querySelector('#myProductGrid');
        $(divProductGrid).empty();

        var gridOptions = {
        	columnDefs: [
				{ headerName: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
                { headerName: "名称", field: "ProductName", width: 120, cssClass: "bg-gray" },
                { headerName: "编码", field: "ProductCode", width: 120, cssClass: "bg-gray" },
                { headerName: "类型", field: "ProductType", width: 160, cssClass: "bg-gray" },
                { headerName: "单价", field: "UnitPrice", width: 160, cssClass: "bg-gray" },
                { headerName: "创建时间", field: "CreatedDate", width: 200, cssClass: "bg-gray", formatter: datetimeFormatter },
        	],
        	rowSelection: 'single',
        	onSelectionChanged: onSelectionChanged
        }

        new agGrid.Grid(divProductGrid, gridOptions);
        gridOptions.api.setRowData(dataSource);

        function onSelectionChanged() {
        	var selectedRows = gridOptions.api.getSelectedRows();
        	var selectedProcessID = 0;
        	selectedRows.forEach(function (selectedRow, index) {
        		rolelist.pselectedProductID = selectedRow.ID;
        		rolelist.pselectedProductDataRow = selectedRow;
        	});
        }

        $('#loading-indicator').hide();
    }

    productlist.getProductByID = function () {
        if (productlist.mselectedProductID > 0) {
            jshelper.ajaxGet("/soneweb/api/product/Get/" + productlist.mselectedProductID,
                null, function (result) {
                    if (result.Status == 1) {
                        var entity = result.Entity;

                        $("#txtProductName").val(entity.ProductName);
                        $("#ddlProductType").val(entity.ProductType);
                        $("#txtProductCode").val(entity.ProductCode);
                        $("#txtUnitPrice").val(entity.UnitPrice);
                        $("#txtNotes").val(entity.Notes);
                    } else {
                        $.msgBox({
                            title: "Product / Edit",
                            content: result.Message,
                            type: "error",
                            buttons: [{ value: "Ok" }],
                        });
                    }
                });
        }
    }

    productlist.sure = function () {
        var entity = {};
        entity.ID = productlist.mselectedProductID;
        entity.ProductName = $("#txtProductName").val();
        entity.ProductType = $("#ddlProductType").val();
        entity.ProductCode = $("#txtProductCode").val();
        entity.UnitPrice = $("#txtUnitPrice").val();
        entity.Notes = $("#txtNotes").val();

        jshelper.ajaxPost("/soneweb/api/product/save",
            JSON.stringify(entity), function (result) {
                if (result.Status == 1) {
                    $.msgBox({
                        title: "Product / Edit",
                        content: "产品记录保存成功！",
                        type: "info"
                    });

                    //refresh
                    productlist.load();
                } else {
                    $.msgBox({
                        title: "Product / Save",
                        content: result.Message,
                        type: "error",
                        buttons: [{ value: "Ok" }],
                    });
                }
        });
        $("#modelProductEditForm").modal("hide");
    }

    productlist.delete = function () {
        if (productlist.mselectedProductID == 0) {
            return;
        }

        $.msgBox({
            title: "Are You Sure",
            content: "确定要删除产品数据记录吗? ",
            type: "confirm",
            buttons: [{ value: "Yes" }, { value: "Cancel" }],
            success: function (result) {
                if (result == "Yes") {
                    jshelper.ajaxGet("/soneweb/api/product/Delete/" + productlist.mselectedProductID,
                        null, function (result) {
                            if (result.Status == 1) {
                                $.msgBox({
                                    title: "Biz / Product",
                                    content: "产品记录已经删除！",
                                    type: "info"
                                });

                                //refresh
                                productlist.load();
                            } else {
                                $.msgBox({
                                    title: "Product / Delete",
                                    content: result.Message,
                                    type: "error",
                                    buttons: [{ value: "Ok" }],
                                });
                            }
                        });
                } 
            }
        });
    }

    return productlist;
})();