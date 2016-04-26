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
        productlist.mselectedProductID = 0;

        var columnProduct = [
                    { id: "ID", name: "ID", field: "ID", width: 40, cssClass: "bg-gray" },
                    { id: "ProductName", name: "名称", field: "ProductName", width: 120, cssClass: "bg-gray" },
                    { id: "ProductCode", name: "编码", field: "ProductCode", width: 120, cssClass: "bg-gray" },
                    { id: "ProductType", name: "类型", field: "ProductType", width: 160, cssClass: "bg-gray" },
                    { id: "UnitPrice", name: "单价", field: "UnitPrice", width: 160, cssClass: "bg-gray" },
                    { id: "CreatedDate", name: "创建时间", field: "CreatedDate", width: 200, cssClass: "bg-gray", formatter: datetimeFormatter },
        ];

        var optionsProduct = {
            editable: true,
            enableCellNavigation: true,
            enableColumnReorder: true,
            asyncEditorLoading: true,
            forceFitColumns: false,
            topPanelHeight: 25
        };

        var dsProduct = dataSource;
        var dvProduct = new Slick.Data.DataView({ inlineFilters: true });
        var gridProduct = new Slick.Grid("#myProductGrid", dvProduct, columnProduct, optionsProduct);

        dvProduct.onRowsChanged.subscribe(function (e, args) {
            gridProduct.invalidateRows(args.rows);
            gridProduct.render();

        });

        dvProduct.onRowCountChanged.subscribe(function (e, args) {
            gridProduct.updateRowCount();
            gridProduct.render();
        });

        dvProduct.beginUpdate();
        dvProduct.setItems(dsProduct, "ID");
        gridProduct.setSelectionModel(new Slick.RowSelectionModel());
        dvProduct.endUpdate();

        gridProduct.onSelectedRowsChanged.subscribe(function (e, args) {
            var selectionRowIndex = args.rows[0];
            var row = dvProduct.getItemByIdx(selectionRowIndex);

            if (row) {
                productlist.mselectedProductID = row.ID;
                productlist.mselectedProductRow = row;
            }
        });
    }


    function datetimeFormatter(row, cell, value, columnDef, dataContext) {
        if (value != null && value != "") {
            return value.substring(0, 10);
        }
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