<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccountOrders.aspx.cs" Inherits="TEST_ASP_ALPHA_1.MyAccountOrders" MasterPageFile="~/Site.Master" %>

<%@ Import Namespace="TEST_ASP_ALPHA_1.Common" %>
<%@ Import Namespace="TEST_ASP_ALPHA_1.Models" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>My Account</strong></li>
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
                            <h2>My Orders</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, <%=Session[CommonManager.GetCustNameSessionName()].ToString()%>!</strong>
                                <p>Here you can see the list off all your orders.</p>
                            </div>
                            <div class="recent-orders">
                                <div class="title-buttons"><strong>Order History</strong></div>
                                <div class="toolbar">
                                    <div class="pager">
                                        <div class="pages" style="float:right">
                                            <label>Page:</label>
                                            <ul class="pagination" runat="server" id="paginationCtrl">
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <table class="data-table" id="my-orders-table">
                                        <thead>
                                            <tr class="first last">
                                                <th>Order #</th>
                                                <th>Date</th>
                                                <th>Item</th>
                                                <th><span class="nobr">Sub Total</span></th>
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%var purchItems = new PurchItemModel().GetPurchItemDetails(PurchItemGetType.customerEmail, 0, new AESManager().EncryptToString(Session[CommonManager.GetCustEmailSessionName()].ToString()), 0, null, null, PurchItemOrderBy.OrderIdDesc, currentViewPerPage, (currentPage - 1) * currentViewPerPage);
                                              if (purchItems.Count > 0)
                                              {
                                                  var colorYellow = "#ffed81";
                                                  var colorRed = "#ff6c6c";
                                                  var colorBlue = "#5dbbff";
                                                  var selectColor = "";
                                                  foreach (var item in purchItems)
                                                  {
                                                      if (item.Status == CommonManager.Status_GetNewItemName())
                                                          selectColor = colorYellow;
                                                      else if (item.Status == CommonManager.Status_GetDeliveredItemName())
                                                          selectColor = colorBlue;
                                                      else if (item.Status == CommonManager.Status_GetCancelledItemName())
                                                          selectColor = colorRed;
                                            %>
                                            <tr style="background-color:<%=selectColor%>">
                                                <td><%=item.PurchId %></td>
                                                <td><%=item.PurchDate %></td>
                                                <td><%=item.ItemTitle %></td>
                                                <td><%=item.SubTotal %></td>
                                                <td><%=item.Status %></td>
                                                <td><a href="javascript:void(0);" onclick="CancelOrder('<%=item.PurchId%>');"><%=item.Status == CommonManager.Status_GetNewItemName()? "cancel" : ""%></a></td>
                                             <tr />
                                            <%
                                                  }
                                              }
                                              else
                                              {
                                            %>
                                            <tr>
                                                <td colspan="6">
                                                    <center>You have not ordered anything yet! Let's <a href="BrowseGamesGrid.aspx"><b>go shopping!</b></a></center>
                                                </td>
                                                <tr />
                                            <%
                                              }%>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">My Account</div>
                        <div class="block-content">
                            <ul>
                                <li><a href="MyAccountDashboard">Account Dashboard</a></li>
                                <li><a href="MyAccountInformation.aspx">Account Information</a></li>
                                <li class="current"><a href="MyAccountOrders.aspx">My Orders</a></li>
                                <li><a href="MyAccountProductReviews.aspx">My Product Reviews</a></li>
                                <li><a href="MyAccountWishlist.aspx">My Wishlist</a></li>
                                <li class="last"><a href="MyAccountChangePassword.aspx">Change Password</a></li>
                            </ul>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <!--End main-container -->
</asp:Content>

