<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSiteAdministrators.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminSiteAdministrators"MasterPageFile="~/Admin.Master" Title="Administrators" %>


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
                    <li class="Users"><a>Users</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Administrators</strong></li>
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
                            <h2>Administrators</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to view information of site administrators</p>
                                <br />
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
                                <li><a href="AdminUsers.aspx">User Details</a></li>
                                <li><a href="AdminUserQueries.aspx">User Queries</a></li>
                                <li class="current"><a href="#">Administrators</a></li>
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
            url: 'Handlers/AdminSiteAdminstratorsHandler.ashx',
            datatype: "json",
            height: 222,
            colNames: ['Id', 'Username', 'Type'],
            colModel: [
                        { name: 'Id', index: 'Id', width: 150, stype: 'text', search: true, sorttype: "int", key: true, editable: false },
   		                { name: 'Username', index: 'Username', width: 400, search: true, stype: 'text', sortable: true, editable: true },
                        { name: 'Type', index: 'Type', width: 400, search: true, stype: 'text', sortable: true, editable: true, edittype: 'select', editoptions: { value: getAdminTypes() } }

            ],
            rowNum: 10,
            mtype: 'GET',
            loadonce: true,
            rowList: [10, 20, 30],
            gridview: true,
            autowidth: true,
            ignoreCase: true,
            pager: '#jQGridDemoPager',
            sortname: 'Id',
            viewrecords: true,
            sortorder: 'asc',
            caption: "Administrators",
            editurl: 'Handlers/AdminSiteAdminstratorsHandler.ashx'
        });

        $("#jQGridDemo").jqGrid('filterToolbar', { searchOperators: false, searchOnEnter: false, autosearch: true, defaultSearch: 'cn' });
        $('#jQGridDemo').jqGrid('navGrid', '#jQGridDemoPager',
                    {
                        edit: true,
                        add: true,
                        del: true,
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
                            ItemId: function () {
                                var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selrow');
                                var value = $('#jQGridDemo').jqGrid('getCell', sel_id, 'Id');
                                return value;
                            }
                        }
                    },
                    {
                        closeAfterAdd: true,//Closes the add window after add
                        afterSubmit: function (response, postdata) {
                            if (response.responseText == "") {

                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                                return [true, '']
                            }
                            else {
                                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid')//Reloads the grid after Add
                                return [false, response.responseText]
                            }
                        }
                    },
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
                            AdminId: function () {
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

        function getAdminTypes()
        {
            var itemTypes = { 'Main': 'Main', 'Sub': 'Sub' };
            return itemTypes;
        }
    </script>
</asp:Content>