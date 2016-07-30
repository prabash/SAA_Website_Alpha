<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSalesDetails.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminSalesDetails" MasterPageFile="~/Admin.Master" Title="Sales Details" %>

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
                    <li class="category13"><strong>Sales Details</strong></li>
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
                            <h2>Sales Details</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to view information regarding sales</p>
                                <br />
                            </div>

                            <div class="recent-orders">
                                <form runat="server">
                                    <fieldset class="group-select">
                                        <ul>
                                            <li>
                                                <div class="input-box">
                                                    <label for="shipping:city">From Date</label>
                                                    <br />
                                                    <input type="date" id="dtStartDate_PurchItemsByType" class="input-text" />
                                                </div>
                                                <div class="input-box">
                                                    <label for="shipping:city">To Date</label>
                                                    <br />
                                                    <input type="date" id="dtEndDate_PurchItemsByType" class="input-text" />
                                                </div>
                                            </li>
                                            <li>
                                                <div class="input-box">
                                                    <label for="shipping:city">Cart Status</label>
                                                    <br />
                                                    <select id="cmbCartStatuses" class="input-text">
                                                        <option value="All">All</option>
                                                        <option value="<%=CommonManager.Status_GetNewItemName()%>"><%=CommonManager.Status_GetNewItemName()%></option>
                                                        <option value="<%=CommonManager.Status_GetDeliveredItemName()%>"><%=CommonManager.Status_GetDeliveredItemName()%></option>
                                                        <option value="<%=CommonManager.Status_GetCancelledItemName()%>"><%=CommonManager.Status_GetCancelledItemName()%></option>
                                                        <option value="<%=CommonManager.Status_GetClosedItemName()%>"><%=CommonManager.Status_GetClosedItemName()%></option>
                                                    </select>
                                                </div>

                                            </li>
                                            <li>
                                                <div class="input-box">
                                                    <input type="button" id="btnGenerateChart" class="button submit" value="REFRESH DATA" onclick="RefreshData()" />
                                                </div>
                                            </li>
                                        </ul>
                                    </fieldset>
                                    <br />
                                    <hr />
                                    <div class="table-responsive">
                                        <h3>Purchase Cart Details</h3>
                                        <div>
                                            <table id="jQGridDemo" class="data-table" style="width: 600px">
                                            </table>
                                            <div id="jQGridDemoPager">
                                            </div>
                                        </div>
                                        <br />
                                        <hr />
                                        <h3>Purchase Item Details</h3>
                                        <fieldset class="group-select">
                                            <ul>
                                                <li>
                                                    <div class="input-box">
                                                        <label for="shipping:city">Customer Name</label>
                                                        <br />
                                                        <input type="text" id="txtCustName" class="input-text" />
                                                    </div>
                                                    <div class="input-box">
                                                        <label for="shipping:city">Email</label>
                                                        <br />
                                                        <input type="text" id="txtCustEmail" class="input-text" />
                                                    </div>
                                                </li>
                                                <li>
                                                    <label for="shipping:city">Address</label>
                                                    <br />
                                                    <input type="text" id="txtCustAddress" class="input-text" />

                                                </li>
                                                <li>
                                                    <div class="input-box">
                                                        <label for="shipping:city">Telephone</label>
                                                        <br />
                                                        <input type="text" id="txtCustTelephone" class="input-text" />
                                                    </div>
                                                </li>
                                            </ul>
                                        </fieldset>
                                        <br />
                                        <div>
                                            <table id="jQGridSubDemo" class="data-table" style="width: 600px">
                                            </table>
                                            <div id="jQGridSubDemoPager">
                                            </div>
                                        </div>
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
                                <li class="current"><a href="#">Sales Details</a></li>
                                <li><a href="AdminSalesLedger.aspx">Sales Ledger</a></li>
                            </ul>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <!--End main-container -->
    <script type="text/javascript">
        jQuery("#jQGridDemo").jqGrid({
            url: 'Handlers/AdminSalesMasterHandler.ashx',
            datatype: "json",
            height: 222,
            colNames: ['Cart Id', 'Cust Id', 'Cust Name', 'No. of Items', 'Cart Status', 'Purchased Date'],
            colModel: [
                        { name: 'CartId', index: 'CartId', width: 150, stype: 'text', search: true, sorttype: "int", key: true, editable: false },
                        { name: 'CustId', index: 'CustId', hidden: true, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'CustName', index: 'CustName', width: 500, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'PurchasedItems', index: 'PurchasedItems', width: 300, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'CartStatus', index: 'CartStatus', width: 300, search: true, stype: 'text', sortable: true, editable: true, formatter: fontFormatter, edittype: 'select', editoptions: { value: getPurchCartStatuses() } },
                        { name: 'PurchasedDate', index: 'PurchasedDate', width: 400, search: true, stype: 'text', sortable: true, formatter: "date", formatoptions: { newformat: "d/m/Y H:i:s" }, sorttype: 'date' }

            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            rowList: [10, 20, 30],
            gridview: true,
            autowidth: true,
            ignoreCase: true,
            pager: '#jQGridDemoPager',
            sortorder: 'desc',
            sortname: 'PurchasedDate',
            viewrecords: true,
            caption: "New Sales Details",
            editurl: 'Handlers/AdminSalesMasterHandler.ashx',
            onSelectRow: function (id) {
                var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selrow');
                var cartID = $('#jQGridDemo').jqGrid('getCell', sel_id, 'CartId');
                var custID = $('#jQGridDemo').jqGrid('getCell', sel_id, 'CustId');

                GetCustomerData(custID);

                var newUrl = "Handlers/AdminSalesDetailsHandler.ashx?cartId=" + cartID;
                jQuery("#jQGridSubDemo").jqGrid('setGridParam', { url: newUrl, page: 1, datatype: 'json' })
                jQuery("#jQGridSubDemo").jqGrid('setCaption', "Item Details for Cart Id: " + cartID)
                .trigger('reloadGrid');
            }
        });

        $("#jQGridDemo").jqGrid('filterToolbar', { searchOperators: false, searchOnEnter: false, autosearch: true, defaultSearch: 'cn' });
        $('#jQGridDemo').jqGrid('navGrid', '#jQGridDemoPager',
                    {
                        edit: true,
                        add: false,
                        del: false,
                        search: true,
                        refresh: true
                    },
                    {
                        closeOnEscape: true,//Closes the popup on pressing escape key
                        reloadAfterSubmit: true,
                        drag: true,
                        afterSubmit: function (response, postdata) {
                            if (response.responseText == "") {

                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');//Reloads the grid after edit
                                return [true, '']
                            }
                            else {
                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                                return [false, response.responseText]//Captures and displays the response text on th Edit window
                            }
                        },
                        editData: {
                            CartId: function () {
                                var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selrow');
                                var value = $('#jQGridDemo').jqGrid('getCell', sel_id, 'CartId');
                                return value;
                            }
                        }
                    },
                    {},
                    {}
             );

        $(window).on("resize", function () {
            var newWidth = $("#jQGridDemo").closest(".ui-jqgrid").parent().width();
            $('#jQGridDemo').jqGrid("setGridWidth", newWidth, true);
        });

        // SUB JQGRID ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        jQuery("#jQGridSubDemo").jqGrid({
            url: 'Handlers/AdminSalesDetailsHandler.ashx?cartId=0',
            datatype: "json",
            height: 222,
            colNames: ['Purch Id', 'Item Name', 'Item Qty', 'Sub Total', 'Item Status'],
            colModel: [
                        { name: 'PurchId', index: 'PurchId', stype: 'text', width: 150, sorttype: "int", key: true, editable: false },
                        { name: 'ItemTitle', index: 'ItemTitle', width: 600, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'ItemQuantity', index: 'ItemQuantity', width: 300, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'SubTotal', index: 'SubTotal', width: 300, search: true, stype: 'text', sortable: true, editable: false },
                        { name: 'Status', index: 'Status', width: 300, search: true, stype: 'text', sortable: true, editable: true, formatter: fontFormatter, edittype: 'select', editoptions: { value: getPurchItemStatuses() } }

            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            rowList: [10, 20, 30],
            gridview: true,
            autowidth: true,
            ignoreCase: true,
            pager: '#jQGridSubDemoPager',
            sortorder: 'desc',
            sortname: 'ItemId',
            viewrecords: true,
            caption: "Sales Item Details",
            editurl: 'Handlers/AdminSalesDetailsHandler.ashx'
        });

        $("#jQGridSubDemo").jqGrid('filterToolbar', { searchOperators: false, searchOnEnter: false, autosearch: true, defaultSearch: 'cn' });
        $('#jQGridSubDemo').jqGrid('navGrid', '#jQGridSubDemoPager',
                    {
                        edit: true,
                        add: false,
                        del: false,
                        search: true,
                        refresh: true
                    },
                    {
                        closeOnEscape: true,//Closes the popup on pressing escape key
                        reloadAfterSubmit: true,
                        drag: true,
                        afterSubmit: function (response, postdata) {
                            if (response.responseText == "") {

                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');//Reloads the grid after edit
                                return [true, '']
                            }
                            else {
                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid'); //Reloads the grid after edit
                                return [false, response.responseText]//Captures and displays the response text on th Edit window
                            }
                        },
                        editData: {
                            PurchId: function () {
                                var sel_id = $('#jQGridSubDemo').jqGrid('getGridParam', 'selrow');
                                var value = $('#jQGridSubDemo').jqGrid('getCell', sel_id, 'PurchId');
                                return value;
                            }
                        }
                    },
                    {},
                    {}
             );

        $(window).on("resize", function () {
            var newWidth = $("#jQGridSubDemo").closest(".ui-jqgrid").parent().width();
            $('#jQGridSubDemo').jqGrid("setGridWidth", newWidth, true);
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

        function getPurchItemStatuses() {
            var statuses = {
                '<%=CommonManager.Status_GetDeliveredItemName()%>': '<%=CommonManager.Status_GetDeliveredItemName()%>'
            };
            return statuses;
        };

    </script>
    <script>
        function RefreshData() {
            var status = document.getElementById('cmbCartStatuses').value;

            var fromDate = document.getElementById('dtStartDate_PurchItemsByType').value;
            if (!fromDate) {
                fromDate = "";
            }

            var toDate = document.getElementById('dtEndDate_PurchItemsByType').value;
            if (!toDate) {
                toDate = "";
            }

            var newUrl = "Handlers/AdminSalesMasterHandler.ashx?itemStatus=" + status + "&fromDate=" + fromDate + "&toDate=" + toDate;
            jQuery("#jQGridDemo").jqGrid('setGridParam', { url: newUrl, page: 1, datatype: 'json' }).trigger('reloadGrid');
        };
    </script>
    <script>
        function GetCustomerData(custId) {
            $.ajax({
                type: "POST",
                url: "AdminSalesDetails.aspx/GetCustDetails",
                data: '{custId: "' + custId + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessGetCustomerData,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccessGetCustomerData(response) {
            var customer = jQuery.parseJSON(response.d);
            document.getElementById('txtCustName').value = customer["name"];
            document.getElementById('txtCustEmail').value = customer["emailAddress"];
            document.getElementById('txtCustAddress').value = customer["addressLine1"] + ", " + customer["addressLine2"] + ", " + customer["city"];
            document.getElementById('txtCustTelephone').value = customer["telephoneNo"];
        };
    </script>
</asp:Content>
