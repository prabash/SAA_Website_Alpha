<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminUsers" MasterPageFile="~/Admin.Master" Title="Users" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="css/flapper.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery.flapper.js"></script>
    <script src="Scripts/flapdemo.js"></script>

    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="AdminDashboard.aspx">Home</a><span>&raquo;</span></li>
                    <li class="Users">Users</a><span>&raquo;</span></li>
                    <li class="category13"><strong>User Details</strong></li>
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
                            <h2>User Details</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to view information of users</p>
                                <br />
                            </div>
                            <div class="displays">
                                <div class="activity"></div>
                                <table style="width:100%">
                                    <tr>
                                        <td><h3>TOTAL REGISTERED USERS&nbsp;</h3></td>
                                        <td style="float:right;"><input class="disTotalActiveUsers M" /></td>
                                    </tr>
                                    <tr>
                                        <td><h3>LOGGED IN TODAY&nbsp;</h3></td>
                                        <td style="float:right;"><input class="disLoggedInToday M" /></td>
                                    </tr>
                                </table>
                                
                            </div>
                            <div class="inputarea" style="height:0px;visibility:hidden">
                                <div class="inline">
                                    <textarea id="txtTotalActiveUsers" placeholder="Type Something Here..." rows="1" cols="5"></textarea></div>
                                <div class="inline">
                                    <button id="btnTotalActiveUsers">And Click Here</button></div>
                            </div>
                            <div class="inputarea" style="height:0px;visibility:hidden">
                                <div class="inline">
                                    <textarea id="txtLoggedInToday" placeholder="Type Something Here..." rows="1" cols="5"></textarea></div>
                                <div class="inline">
                                    <button id="btnLoggedInToday">And Click Here</button></div>
                            </div>
                            <br />
                            <div class="recent-orders">
                                <form runat="server">
                                    <div class="table-responsive">
                                        <div>
                                            <table id="jQGridDemo" class="data-table" style="width: 600px">
                                            </table>
                                            <div id="jQGridDemoPager">
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <br />
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">Users</div>
                        <div class="block-content">
                            <ul>
                                <li class="current"><a href="#">User Details</a></li>
                                <li><a href="AdminResetUserPassword.aspx">Reset User Passwords</a></li>
                                <li><a href="AdminUserQueries.aspx">User Queries</a></li>
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
            url: 'Handlers/AdminUserDetailsHandler.ashx',
            datatype: "json",
            height: 222,
            colNames: ['Id', 'Name', 'Email', 'Telephone No', 'Address', 'Username', 'Registered Date', 'Last Logged', 'Active'],
            colModel: [
                        { name: 'Id', index: 'Id', width: 150, stype: 'text', search: true, sorttype: "int", key: true },
   		                { name: 'name', index: 'name', width: 400, search: true, stype: 'text', sortable: true },
                        { name: 'emailAddress', index: 'emailAddress', width: 600, search: true, stype: 'text', sortable: true },
                        { name: 'telephoneNo', index: 'telephoneNo', width: 400, search: true, stype: 'text', sortable: true },
                        { name: 'addressLine1', index: 'addressLine1', width: 800, search: true, stype: 'text', sortable: true, formatter: addressFormatter },
                        { name: 'username', index: 'username', width: 300, search: true, stype: 'text', sortable: true },
                        { name: 'registeredDate', index: 'registeredDate', width: 250, search: true, stype: 'text', sortable: true, formatter: "date", formatoptions: { newformat: "d/m/Y H:i:s" } },
                        { name: 'lastLoggedDate', index: 'lastLoggedDate', width: 250, search: true, stype: 'text', sortable: true, formatter: "date", formatoptions: { newformat: "d/m/Y H:i:s" } },
   		                { name: 'active', index: 'active', width: 200, search: true, editable: true }

            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            rowList: [10, 20, 30],
            gridview: true,
            autowidth: true,
            ignoreCase: true,
            pager: '#jQGridDemoPager',
            sortname: 'id',
            viewrecords: true,
            sortorder: 'desc',
            caption: "User Details",
            editurl: 'Handlers/AdminUserDetailsHandler.ashx'
        });

        $("#jQGridDemo").jqGrid('filterToolbar', { searchOperators: false, searchOnEnter: false, autosearch: true, defaultSearch: 'cn' });
        $('#jQGridDemo').jqGrid('navGrid', '#jQGridDemoPager',
                    {
                        edit: false,
                        add: false,
                        del: true,
                        search: true,
                        refresh: true
                    },
                    {},
                    {},
                    {   //DELETE
                        closeOnEscape: true,
                        closeAfterDelete: true,
                        reloadAfterSubmit: true,
                        closeOnEscape: true,
                        drag: true,
                        afterSubmit: function (response, postdata) {
                            if (response.responseText == "") {

                                $("#jQGridDemo").trigger("reloadGrid", [{ current: true }]);
                                return [false, response.responseText]
                            }
                            else {
                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')
                                return [true, response.responseText]
                            }
                        },
                        delData: {
                            CustId: function () {
                                var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selrow');
                                var value = $('#jQGridDemo').jqGrid('getCell', sel_id, 'Id');
                                return value;
                            }
                        }
                    }
             );

        $(window).on("resize", function () {
            var newWidth = $("#jQGridDemo").closest(".ui-jqgrid").parent().width();
            $('#jQGridDemo').jqGrid("setGridWidth", newWidth, true);
        });

        function addressFormatter(cellvalue, options, rowObject) {
            return cellvalue + ', ' + rowObject.addressLine2 + ', ' + rowObject.city;
        }
    </script>
    <script type="text/javascript">
        setInterval(GetTotalActiveUsers, '<%=TEST_ASP_ALPHA_1.Common.CommonManager.Counter_GetRefreshTimeOut()%>');
        function GetTotalActiveUsers() {
            $.ajax({
                type: "POST",
                url: "AdminUsers.aspx/GetTotalActiveUsers",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessGetActiveUsers,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccessGetActiveUsers(response) {
            $('#txtTotalActiveUsers').val(response.d);
            $('#btnTotalActiveUsers').click();
        };
        
    </script>
    <script type="text/javascript">
        setInterval(GetLoggedInToday, '<%=TEST_ASP_ALPHA_1.Common.CommonManager.Counter_GetRefreshTimeOut()%>');
        function GetLoggedInToday() {
            $.ajax({
                type: "POST",
                url: "AdminUsers.aspx/GetLoggedInToday",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessGetLoggedInToday,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccessGetLoggedInToday(response) {
            $('#txtLoggedInToday').val(response.d);
            $('#btnLoggedInToday').click();
        };
        
    </script>
</asp:Content>
