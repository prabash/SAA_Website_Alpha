<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JQGrid.aspx.cs" Inherits="TEST_ASP_ALPHA_1.JQGrid" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="scripts/Chart.js" type="text/javascript"></script>
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

        <div>
            <!--style="width:400px;height:400px"-->
            <canvas id="myChart"></canvas>
        </div>
        <div class="wrapper">
            <div class="cell">
                <div id="holder">
                    <div class="digits"></div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function GetData() {
            $.ajax({
                type: "POST",
                url: "MyAccountAjaxPage.aspx/getLineChartData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            var data = response.d;
            var _labelSet = data.split('|')[0];
            var _splittedLabels = _labelSet.split(',');
            var arrayLength = _splittedLabels.length;
            var labelsArr = [];

            for (var i = 0; i < arrayLength; i++) {
                labelsArr.push(_splittedLabels[i]);
            }

            var _dataSet = data.split('|')[1];
            var _splittedData = _dataSet.split(',');
            arrayLength = _splittedData.length;
            var dataArr = [];

            for (var i = 0; i < arrayLength; i++) {
                dataArr.push(_splittedData[i]);
            }


            var ctx = document.getElementById("myChart");
            var myChart = new Chart(ctx, {
                type: 'pie',
                responsive: 'true',
                data: {
                    labels: labelsArr,
                    datasets: [{
                        label: 'Date',
                        data: dataArr,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },

            });
        }
        GetData();
    </script>
</body>

</html>
