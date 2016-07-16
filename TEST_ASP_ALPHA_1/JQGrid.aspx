<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JQGrid.aspx.cs" Inherits="TEST_ASP_ALPHA_1.JQGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="JQGridReq/jquery-1.9.0.min.js" type="text/javascript"></script>
    <link href="JQGridReq/jquery-ui-1.9.2.custom.css" rel="stylesheet" type="text/css" />
    <script src="JQGridReq/jquery.jqGrid.js" type="text/javascript"></script>
    <link href="JQGridReq/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="JQGridReq/grid.locale-en.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Welcome To Kodoths JQGrid Demo
        </h2>
        <table id="jQGridDemo">
        </table>
        <div id="jQGridDemoPager">
        </div>
        <script type="text/javascript">
            jQuery("#jQGridDemo").jqGrid({
                url: 'http://localhost:24010/JQGridHandler.ashx',
                datatype: "json",
                colNames: ['Id', 'Title', 'Location'],
                colModel: [
                            { name: 'id', index: 'id', width: 50, stype: 'text', search: true },
                            { name: 'title', index: 'title', width: 375, search: true, stype: 'text', sortable: true, editable: true },
                            { name: 'location', index: 'location', width: 375, search: true, editable: true }
                ],
                rowNum: 10,
                mtype: 'GET',
                loadonce: true,
                rowList: [10, 20, 30],
                pager: '#jQGridDemoPager',
                sortname: 'id',
                viewrecords: true,
                sortorder: 'desc',
                caption: "List Employee Details",
                editurl: 'http://localhost:24010/JQGridHandler.ashx'
            });
            $("#jQGridDemo").jqGrid('filterToolbar', { searchOperators: true, sopt: ['cn', 'bw', 'eq'], searchOnEnter: false, autosearch: true });

            $('#jQGridDemo').jqGrid('navGrid', '#jQGridDemoPager',
                        {
                            edit: true,
                            add: true,
                            del: true,
                        },
                        {   //EDIT
                            //                       height: 300,
                            //                       width: 400,
                            //                       top: 50,
                            //                       left: 100,
                            //                       dataheight: 280,
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
                        },
                        {//SEARCH
                            closeOnEscape: true,
                            searchoptions: {
                                dataInit: function (elem) {
                                    //$(elem).css({height:"30px", width:"40px"});
                                    $(elem).height(30).width(100);
                                }
                            }

                        }
                 );

            $(window).on("resize", function () {
                var newWidth = $("#jQGridDemo").closest(".ui-jqgrid").parent().width();
                $('#jQGridDemo').jqGrid("setGridWidth", newWidth, true);
            });

        </script>
    </form>
</body>
</html>
