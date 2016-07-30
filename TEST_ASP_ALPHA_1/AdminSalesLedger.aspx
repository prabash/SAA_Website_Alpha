<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSalesLedger.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminSalesLedger" MasterPageFile="~/Admin.Master" Title="Sales Ledger" %>

<%@ Import Namespace="TEST_ASP_ALPHA_1.Common" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="scripts/Chart.js" type="text/javascript"></script>
    <link href="css/flapper.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery.flapper.js"></script>
    <script src="Scripts/flapdemo.js"></script>

    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="AdminDashboard.aspx">Home</a><span>&raquo;</span></li>
                    <li class="Users"><a>Sales</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Sales Ledger</strong></li>
                </ul>
            </div>
        </div>
    </div>
    <!-- main-container -->
    <div class="main-container col2-right-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="row">
                <section class="col-main col-sm-9 wow bounceInUp animated">
                    <div class="my-account">
                        <div class="page-title new_page_title">
                            <h2>Sales Ledger</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to view information regarding sales</p>
                                <br />
                            </div>

                            <div class="recent-orders">
                                <form runat="server">
                                    <div class="table-responsive">
                                        <fieldset class="group-select">
                                            <ul>
                                                <li>
                                                    <div class="input-box">
                                                        <label for="shipping:city">From Date</label>
                                                        <br />
                                                        <input type="date" id="dtStartDate_PurchCart" class="input-text" />
                                                    </div>
                                                    <div class="input-box">
                                                        <label for="shipping:city">To Date</label>
                                                        <br />
                                                        <input type="date" id="dtEndDate_PurchCart" class="input-text" />
                                                    </div>
                                                </li>
                                                <li>
                                                    <div class="input-box">
                                                        <input type="button" id="btnGenerateChart" class="button submit" value="REFRESH DATA" onclick="RefreshDataSalesLedger()" />
                                                    </div>
                                                </li>
                                            </ul>
                                        </fieldset>
                                        <br />
                                        <hr />
                                        <div>
                                            <table id="jQGridDemo" class="data-table" style="width: 600px">
                                            </table>
                                            <div id="jQGridDemoPager">
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </form>
                            </div>
                            <br />
                            <br />
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">Sales</div>
                        <div class="block-content">
                            <ul>
                                <li><a href="AdminSalesDashboard.aspx">Sales Dashboard</a></li>
                                <li><a href="AdminSalesDetails.aspx">Sales Details</a></li>
                                <li class="current"><a href="#">Sales Ledger</a></li>
                            </ul>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <script>
        Date.prototype.toDateInputValue = (function() {
            var local = new Date(this);
            local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
            return local.toJSON().slice(0,10);
        });

        document.getElementById('dtStartDate_PurchCart').value = new Date().toDateInputValue();
        document.getElementById('dtEndDate_PurchCart').value = new Date().toDateInputValue();
    </script>

    <!--End main-container -->
    <script type="text/javascript">
        var cartsLocalJSON;
        jQuery("#jQGridDemo").jqGrid({
            url: 'Handlers/AdminSalesLedgerHandler.ashx',
            datatype: "json",
            height: '300',
            colNames: ['Cart Id', 'Cust Email', 'Cart Status', 'Purchased Date', 'Cart Discount', 'Cart Delivery', 'Cart Total'],
            colModel: [
                        { name: 'CartId', index: 'CartId', width: 150, stype: 'text', search: true, sorttype: "int", key: true, editable: false },
                        { name: 'CustEmailAddress', index: 'CustEmailAddress', width: 300, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'CartStatus', index: 'CartStatus', width: 300, search: true, stype: 'text', sortable: true, editable: true, formatter: fontFormatter, edittype: 'select', editoptions: { value: getPurchCartStatuses() } },
                        { name: 'PurchaseDate', index: 'PurchaseDate', width: 400, search: true, stype: 'text', sortable: true, formatter: "date", formatoptions: { newformat: "d/m/Y H:i:s" }, sorttype: 'date' },
                        { name: 'CartDiscount', index: 'CartDiscount', width: 200, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'CartDelivery', index: 'CartDelivery', width: 200, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'CartTotal', index: 'CartTotal', width: 300, search: true, stype: 'text', sortable: true, editable: false }

            ],
            rowNum: 10,
            mtype: 'GET',
            rowList: [10, 20, 30],
            gridview: true,
            autowidth: true,
            ignoreCase: true,
            pager: '#jQGridDemoPager',
            sortorder: 'desc',
            sortname: 'PurchasedDate',
            viewrecords: true,
            caption: "Sales Ledger",
            subGrid: true,
            footerrow: true,
            gridComplete: function() {
                var $grid = $('#jQGridDemo');
                var colSum = $grid.jqGrid('getCol', 'CartTotal', false, 'sum');
                $grid.jqGrid('footerData', 'set', { 'CartTotal': colSum });
            },
            subGridRowExpanded: function(subgrid_id, row_id) {
                //start of subgrid row expanded
                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");

                $("#" + subgrid_table_id).jqGrid({
                    url: "Handlers/AdminSalesLedgerDetailsHandler.ashx?id=" + row_id,
                    datatype: 'json',
                    colNames: ['Item Title', 'Item Qty', 'Item Cost', 'Discount', 'Sub Total'],
                    colModel: [
                                { name: 'ItemTitle', width: 170 },
                                { name: 'ItemQuantity', width: 110 },
                                { name: 'CostPerItem', width: 110 },
                                { name: 'Discount', width: 110 },
                                { name: 'SubTotal', width: 60 }
                    ],
                    sortname: 'ItemTitle',
                    pager: pager_id,
                    sortorder: "asc"
                });
                $("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: false, del: false })
            }
        });

        $("#jQGridDemo").jqGrid('filterToolbar', { searchOperators: false, searchOnEnter: false, autosearch: true, defaultSearch: 'cn' });
        $('#jQGridDemo').jqGrid('navGrid', '#jQGridDemoPager',
                    {
                        edit: false,
                        add: false,
                        del: false,
                        search: true,
                        refresh: true
                    },
                    {},
                    {},
                    {}
             );

        $(window).on("resize", function () {
            var newWidth = $("#jQGridDemo").closest(".ui-jqgrid").parent().width();
            $('#jQGridDemo').jqGrid("setGridWidth", newWidth, true);
        });


        function fontFormatter(cellValue, opts, rowObject) {
            switch (cellValue) {
                case "<%=CommonManager.Status_GetNewItemName()%>":
                    return '<span style="color:<%=CommonManager.Colors_GetItemStatusColorNew()%>">' + cellValue + '</span>';
                    break;
                case "<%=CommonManager.Status_GetDeliveredItemName()%>":
                    return '<span style="color:<%=CommonManager.Colors_GetItemStatusColorDelivered()%>">' + cellValue + '</span>';
                    break;
                case "<%=CommonManager.Status_GetClosedItemName()%>":
                    return '<span style="color:<%=CommonManager.Colors_GetItemStatusColorClosed()%>">' + cellValue + '</span>';
                    break;
                case "<%=CommonManager.Status_GetCancelledItemName()%>":
                    return '<span style="color:<%=CommonManager.Colors_GetItemStatusColorCancelled()%>">' + cellValue + '</span>';
                    break;
            }
        };

        function getPurchCartStatuses() {
            var statuses = {
                '<%=CommonManager.Status_GetClosedItemName()%>': '<%=CommonManager.Status_GetClosedItemName()%>'
            };
            return statuses;
        };


    </script>
    <script>
        function RefreshDataSalesLedger() {

            var fromDate = document.getElementById('dtStartDate_PurchCart').value;
            if (!fromDate) {
                fromDate = "";
            }

            var toDate = document.getElementById('dtEndDate_PurchCart').value;
            if (!toDate) {
                toDate = "";
            }

            var newUrl = "Handlers/AdminSalesLedgerHandler.ashx?itemStatus=" + status + "&fromDate=" + fromDate + "&toDate=" + toDate;
            jQuery("#jQGridDemo").jqGrid('setGridParam', { url: newUrl, page: 1, datatype: 'json' }).trigger('reloadGrid');
        };
    </script>
</asp:Content>
