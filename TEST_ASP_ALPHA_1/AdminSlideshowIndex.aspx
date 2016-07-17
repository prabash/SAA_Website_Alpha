<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSlideshowIndex.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminSlideshowIndex" MasterPageFile="~/Admin.Master" Title="Index Slideshow"%>

<%@ Import Namespace="TEST_ASP_ALPHA_1.Common" %>
<%@ Import Namespace="TEST_ASP_ALPHA_1.Models" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="AdminDashboard.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Index Slideshow</strong></li>
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
                            <h2>Index Slideshow</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to add or edit the items that will be shown in the slideshow of the homepage.</p>
                                <br />
                                <p><b>INSTRUCTIONS :</b></p>
                                <ul style="margin-left: 15px; list-style-type: square">
                                    <li style="line-height: 0.5em;">
                                        <p>Please make sure the banner dimension is = <b>1170px * 470px</b></p>
                                    </li>
                                    <li style="line-height: 0.5em;">
                                        <p>Always use the same folder for "location" and <b>only change the file name</b></p>
                                    </li>
                                </ul>
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
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">Slideshow</div>
                        <div class="block-content">
                            <ul>
                                <li class="current"><a href="#">Index Slideshow</a></li>
                                <li><a href="AdminSlideshowItems.aspx">Items Slideshow</a></li>
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
            url: 'Handlers/AdminSlideshowIndexHandler.ashx',
            datatype: "json",
            height: 222,
            colNames: ['Id', 'Title', 'Location'],
            colModel: [
                        { name: 'id', index: 'id', width: 50, stype: 'text', search: true },
   		                { name: 'title', index: 'title', width: 370, search: true, stype: 'text', sortable: true, editable: true },
   		                { name: 'location', index: 'location', width: 370, search: true, editable: true }
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
            caption: "Index Slideshow Details",
            editurl: 'Handlers/AdminSlideshowIndexHandler.ashx'
        });

        $("#jQGridDemo").jqGrid('filterToolbar', { searchOperators: false, searchOnEnter: false, autosearch: true, defaultSearch: 'cn' });
        $('#jQGridDemo').jqGrid('navGrid', '#jQGridDemoPager',
                    {
                        edit: true,
                        add: true,
                        del: true,
                        search: false,
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
                            EmpId: function () {
                                var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selrow');
                                var value = $('#jQGridDemo').jqGrid('getCell', sel_id, 'id');
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
                            EmpId: function () {
                                var sel_id = $('#jQGridDemo').jqGrid('getGridParam', 'selrow');
                                var value = $('#jQGridDemo').jqGrid('getCell', sel_id, 'id');
                                return value;
                            }
                        }
                    }
             );

        $(window).on("resize", function () {
            var newWidth = $("#jQGridDemo").closest(".ui-jqgrid").parent().width();
            $('#jQGridDemo').jqGrid("setGridWidth", newWidth, true);
        });
    </script>
</asp:Content>
