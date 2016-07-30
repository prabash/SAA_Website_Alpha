<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSalesDashboard.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminSalesDashboard" MasterPageFile="~/Admin.Master" Title="Sales" %>

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
                    <li class="category13"><strong>Sales Dashboard</strong></li>
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
                            <h2>Sales Dashboard</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to view information regarding sales</p>
                                <br />
                            </div>
                            <div class="displays">
                                <div class="activity"></div>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <h3>PURCHASED ITEMS TODAY&nbsp;</h3>
                                        </td>
                                        <td style="float: right;">
                                            <input class="disPurchasedItemsToday M" /></td>
                                    </tr>
                                </table>

                            </div>
                            <div class="inputarea" style="height: 0px; visibility: hidden">
                                <div class="inline">
                                    <textarea id="txtPurchasedItemsToday" placeholder="Type Something Here..." rows="1" cols="5"></textarea>
                                </div>
                                <div class="inline">
                                    <button id="btnPurchasedItemsToday">And Click Here</button>
                                </div>
                            </div>
                            <hr />
                            <br />
                            <h3>PURCHASED ITEMS BY TYPE&nbsp;</h3>
                            <fieldset>
                                <table style="width: 100%">
                                    <tr>
                                        <td>From Date</td>
                                        <td>
                                            <input type="date" id="dtStartDate_PurchItemsByType" class="input-sm" /></td>
                                        <td>To Date</td>
                                        <td>
                                            <input type="date" id="dtEndDate_PurchItemsByType" class="input-sm" /></td>
                                        <td>
                                            <input type="button" id="btnGenerateChart" class="button submit" value="REFRESH CHART" onclick="GetData()" /></td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <div id="chartDiv_purchItemsByType">
                                <canvas id="chart_purchItemsByType"></canvas>
                            </div>
                            <br />
                            <h4 id="noData_purchItemsByType" style="display:none;text-align:center">NO DATA TO BE DISPLAYED</h4>
                            <br />
                            <hr />
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">Sales</div>
                        <div class="block-content">
                            <ul>
                                <li class="current"><a href="#">Sales Dashboard</a></li>
                                <li><a href="AdminSalesDetails.aspx">Sales Details</a></li>
                                <li><a href="AdminSalesDetails.aspx">Sales Ledger</a></li>
                            </ul>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <!--End main-container -->
    <script type="text/javascript">
        function GetData() {
            var fromDate = document.getElementById('dtStartDate_PurchItemsByType').value;
            if (!fromDate) {
                fromDate = new Date('01-01-1900 00:00:00').toDateString();
            }

            var toDate = document.getElementById('dtEndDate_PurchItemsByType').value;
            if (!toDate) {
                toDate = new Date('01-01-1900 00:00:00').toDateString();
            }

            $.ajax({
                type: "POST",
                url: "AdminSalesDashboard.aspx/getPieChart_PurchasedItemsByType",
                data: '{fromDate: "' + fromDate + '", toDate: "' + toDate + '"}',
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
            var labelsArr = [];
            if (_labelSet) {
                var _splittedLabels = _labelSet.split(',');
                var arrayLength = _splittedLabels.length;

                for (var i = 0; i < arrayLength; i++) {
                    labelsArr.push(_splittedLabels[i]);
                }
            }

            var _dataSet = data.split('|')[1];
            var dataArr = [];
            if (_dataSet) {
                var _splittedData = _dataSet.split(',');
                arrayLength = _splittedData.length;


                for (var i = 0; i < arrayLength; i++) {
                    dataArr.push(_splittedData[i]);
                }
            }

            if (dataArr.length > 0) {
                var div = document.getElementById("chartDiv_purchItemsByType");
                div.style.display = 'block';
                var txt = document.getElementById("noData_purchItemsByType");
                txt.style.display = 'none';

                var ctx = document.getElementById("chart_purchItemsByType");
                var myChart = new Chart(ctx, {
                    type: 'doughnut',
                    responsive: 'true',
                    data: {
                        labels: labelsArr,
                        datasets: [{
                            label: 'Date',
                            data: dataArr,
                            backgroundColor: [
                                '#FF6384',
                                '#36A2EB',
                                '#FFCE56',
                                '#32ffbe',
                                '#ff9639'
                            ],
                            borderColor: '#000000',
                            borderWidth: 1
                        }]
                    },

                });
            }
            else {
                var div = document.getElementById("chartDiv_purchItemsByType");
                div.style.display = 'none';
                var txt = document.getElementById("noData_purchItemsByType");
                txt.style.display = 'block';
            }
        }
        GetData();
    </script>
    <script type="text/javascript">
        setInterval(GetTotalActiveUsers, '<%=TEST_ASP_ALPHA_1.Common.CommonManager.Counter_GetRefreshTimeOut()%>');
        function GetTotalActiveUsers() {
            $.ajax({
                type: "POST",
                url: "AdminSalesDashboard.aspx/GetPurchasedItemsToday",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccessGetPurchasedItemsToday,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccessGetPurchasedItemsToday(response) {
            $('#txtPurchasedItemsToday').val(response.d);
            $('#btnPurchasedItemsToday').click();
        };

    </script>
</asp:Content>
