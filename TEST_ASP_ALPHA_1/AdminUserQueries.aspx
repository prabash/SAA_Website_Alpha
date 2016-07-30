<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUserQueries.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminUserQueries" MasterPageFile="~/Admin.Master" Title="Users Queries" %>

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
                    <li class="category13"><strong>User Queries</strong></li>
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
                            <h2>User Queries</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to view user queries</p>
                                <br />
                            </div>
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
                            <br />
                            <form id="contactForm" class="group-select">
                                <fieldset>
                                    <ul>
                                        <li>
                                            <div class="input-box">
                                                <label>Subject</label>
                                                <input type="text" id="txtCommSubject" readonly="readonly" class="input-text" />
                                            </div>
                                        </li>
                                        <li>
                                            <div>
                                                <label>Comment</label>
                                                <br>
                                                <textarea id="txtComment" cols="7" rows="4" class="input-text" readonly="readonly"></textarea>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="buttons-set">
                                                <input type="hidden" id="txtCustEmail" />
                                                <input type="button" class="button submit" value="REPLY" onclick='mailToCustomer();'>
                                            </div>
                                        </li>
                                    </ul>
                                </fieldset>
                            </form>
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">Users</div>
                        <div class="block-content">
                            <ul>
                                <li><a href="AdminUsers.aspx">User Details</a></li>
                                <li class="current"><a href="#">User Queries</a></li>
                            </ul>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery("#jQGridDemo").jqGrid({
            url: 'Handlers/AdminUserQueriesHandler.ashx',
            datatype: "json",
            height: 222,
            colNames: ['Id', 'Name', 'Email', 'Telephone No', 'Subject', 'Comment', 'Date'],
            colModel: [
                        { name: 'Id', index: 'Id', width: 150, stype: 'text', search: true, sorttype: "int", key: true },
   		                { name: 'Name', index: 'Name', width: 400, search: true, stype: 'text', sortable: true },
                        { name: 'EmailAddress', index: 'EmailAddress', width: 500, search: true, stype: 'text', sortable: true },
                        { name: 'TelephoneNo', index: 'TelephoneNo', width: 300, search: true, stype: 'text', sortable: true },
                        { name: 'Subject', index: 'Subject', width: 500, search: true, stype: 'text', sortable: true },
                        { name: 'Comment', index: 'Comment', hidden: true, search: true, stype: 'text', sortable: true },
                        { name: 'Date', index: 'Date', width: 400, search: true, stype: 'text', sortable: true, formatter: "date", formatoptions: { newformat: "d/m/Y H:i:s" }, sorttype:'date' }

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
            sortname: 'Date',
            viewrecords: true,
            caption: "User Details",
            multiselect: true,
            multiboxonly: true,
            editurl: 'Handlers/AdminUserQueriesHandler.ashx',
            onSelectRow: function (id) {
                var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selrow');
                var subject = $('#jQGridDemo').jqGrid('getCell', sel_id, 'Subject');
                var comment = $('#jQGridDemo').jqGrid('getCell', sel_id, 'Comment');
                var email = $('#jQGridDemo').jqGrid('getCell', sel_id, 'EmailAddress');
                $('#txtCommSubject').val(subject);
                $('#txtComment').val(comment);
                $('#txtCustEmail').val(email);
            }
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
                                var selectedrows = $('#jQGridDemo').jqGrid("getGridParam", "selarrrow");
                                var i = 0;
                                var selectedData = [];
                                if (selectedrows.length) {
                                    for (i = 0; i < selectedrows.length; i++) {
                                        // fill array selectedData with the data from selected rows
                                        var sel_id = selectedrows[i]
                                        selectedData.push($('#jQGridDemo').jqGrid("getCell", sel_id, 'Id'));
                                    }
                                }
                                return JSON.stringify(selectedData);
                            }
                        }
                    }
             );

        $(window).on("resize", function () {
            var newWidth = $("#jQGridDemo").closest(".ui-jqgrid").parent().width();
            $('#jQGridDemo').jqGrid("setGridWidth", newWidth, true);
        });

    </script>
    <script>
        function mailToCustomer() {
            var email = $('#txtCustEmail').val();
            if (email != '') {
                window.location.href = 'mailto:' + email;
            }
        }
    </script>
</asp:Content>

