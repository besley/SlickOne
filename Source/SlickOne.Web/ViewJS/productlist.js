var productlist = (function () {
    productlist.mselectedProductID = 0;
    productlist.mselectedProductRow = null;

    function productlist() {
    }

    productlist.load = function () {
        jshelper.ajaxGet("/soneweb/api/product/GetProductList", null, function (result) {
            if (result.Status == 1) {
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

                var dsProduct = result.Entity;
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
            };
        });

        function datetimeFormatter(row, cell, value, columnDef, dataContext) {
            if (value != null && value != "") {
                return value.substring(0, 10);
            }
        }
    }

    productlist.sure = function () {
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
                    //jshelper.ajaxGet("/soneweb/api/product/Delete/" + productlist.mselectedProductID,
                    //    null, function (result) {
                    //        if (result.Status == 1) {
                    //            $.msgBox({
                    //                title: "Biz / Product",
                    //                content: "产品记录已经删除！",
                    //                type: "info"
                    //            });
                    //            productlist.mselectedProductID = 0;

                    //            //refresh
                    //            productlist.load();
                    //        }
                    //    });
                }
            }
        });
    }

    return productlist;
})();