<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminItems.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminItems" MasterPageFile="~/Admin.Master" Title="Items" %>

<%@ Import Namespace="TEST_ASP_ALPHA_1.Common" %>
<%@ Import Namespace="TEST_ASP_ALPHA_1.Models" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Items</strong></li>
                </ul>
            </div>
        </div>
    </div>
    <!-- main-container -->
    <div class="main-container col1-right-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="row">
                <section class="col-main col-sm-12 wow bounceInUp animated">
                    <div class="my-account">
                        <div class="page-title new_page_title">
                            <h2>Sales Items</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to add or edit the items.</p>
                                <br />
                            </div>
                            <div>
                                <table id="jQGridDemo" class="" style="width: 600px">
                                </table>
                                <div id="jQGridDemoPager">
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>
    <!--End main-container -->

    <script type="text/javascript">
        jQuery("#jQGridDemo").jqGrid({
            url: 'Handlers/AdminItemsHandler.ashx',
            datatype: "json",
            height: 450,
            colNames: ['Id', 'Title', 'Def. Location', 'Alt. Location', 'Reg. Price', 'Current Price', 'Description', 'Overview', 'Year', 'Type', 'Genre', 'Rating', 'On Sale', 'Best Seller'],
            colModel: [
                        { name: 'Id', index: 'Id', width: 50, stype: 'text', search: true },
   		                { name: 'title', index: 'title', width: 150, search: true, stype: 'text', sortable: true, editable: true },
   		                { name: 'defaultLocation', index: 'defaultLocation', width: 150, search: true, editable: true },
                        { name: 'altLocation', index: 'altLocation', width: 150, search: true, editable: true },
                        { name: 'regularPrice', index: 'regularPrice', width: 100, search: true, editable: true },
                        { name: 'currentPrice', index: 'currentPrice', width: 100, search: true, editable: true },
                        { name: 'description', index: 'description', width: 110, search: true, editable: true },
                        { name: 'overview', index: 'overview', width: 110, search: true, editable: true },
                        { name: 'year', index: 'year', width: 110, search: true, editable: true },
                        { name: 'type', index: 'type', width: 110, editable: true, edittype: 'select', editoptions: { value: getAllItemTypes() }, stype: 'select', searchoptions: { sopt: ['eq'], value: getAllItemTypes() } },
                        { name: 'genre', index: 'genre', width: 110, search: true, editable: true },
                        { name: 'rating', index: 'rating', width: 110, search: true, editable: true },
                        { name: 'onSale', index: 'onSale', width: 110, search: true, editable: true, edittype: 'select', editoptions: { value: getBinaryItems() }, stype: 'select', searchoptions: { sopt: ['eq'], value: getBinaryItems() } },
                        { name: 'bestSeller', index: 'bestSeller', width: 110, search: true, editable: true, edittype: 'select', editoptions: { value: getBinaryItems() }, stype: 'select', searchoptions: { sopt: ['eq'], value: getBinaryItems() } }
            ],
            rownumbers: true,
            rowNum: 20,
            mtype: 'GET',
            loadonce: true,
            rowList: [20, 50, 100],
            gridview: true,
            autowidth: true,
            ignoreCase: true,
            pager: '#jQGridDemoPager',
            sortname: 'title',
            viewrecords: true,
            sortorder: 'desc',
            caption: "Item Details",
            editurl: 'Handlers/AdminItemsHandler.ashx'
        });
        $("#jQGridDemo").jqGrid('filterToolbar', { searchOnEnter: false, autosearch: true, defaultSearch: 'cn' });
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
                            ItemId: function () {
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
        function getAllItemTypes() {
            var itemTypes = { '': '', 'Games': 'Games', 'Movies': 'Movies', 'TvSeries': 'TvSeries', 'TvSeries': 'TvSeries', 'Gifts': 'Gifts', 'Electronics': 'Electronics' };
            return itemTypes;
        }
        function getBinaryItems() {
            var itemTypes = { '':'', 'true': 'true', 'false': 'false' };
            return itemTypes;
        }
    </script>
</asp:Content>
