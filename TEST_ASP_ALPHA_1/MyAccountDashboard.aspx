<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccountDashboard.aspx.cs" Inherits="TEST_ASP_ALPHA_1.MyAccountDashboard" MasterPageFile="~/Site.Master" %>

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
                            <h2>My Dashboard</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, <%=Session[CommonManager.GetCustNameSessionName()].ToString()%>!</strong>
                                <p>From your My Account Dashboard you have the ability to view a snapshot of your recent account activity and update your account information. Select a link below to view or edit information.</p>
                            </div>
                            <div class="recent-orders">
                                <div class="title-buttons"><strong>Recent Orders</strong> <a href="MyAccountOrders.aspx">View All </a></div>
                                <div class="table-responsive">
                                    <table class="data-table" id="my-orders-table" >
                                        <thead>
                                            <tr class="first last">
                                                <th>Order #</th>
                                                <th>Date</th>
                                                <th>Item</th>
                                                <th><span class="nobr">Sub Total</span></th>
                                                <th>Status</th>
                                                <th>&nbsp;</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%var purchItems = new PurchItemModel().GetPurchItemDetails(PurchItemGetType.customerEmail, 0, new AESManager().EncryptToString(Session[CommonManager.GetCustEmailSessionName()].ToString()), 0, null, null, PurchItemOrderBy.purchDateDesc, 5);
                                              if (purchItems.Count > 0)
                                              {
                                                  foreach (var item in purchItems)
                                                  {
                                                    %>
                                                    <tr>
                                                        <td><%=item.PurchCartId %></td>
                                                        <td><%=item.PurchDate %></td>
                                                        <td><%=item.ItemTitle %></td>
                                                        <td><%=item.SubTotal %></td>
                                                        <td><%=item.Status %></td>
                                                        <tr />
                                                    <%
                                                  }
                                              }
                                              else
                                              {
                                                %>
                                                <tr>
                                                    <td colspan="6"><center>You have not ordered anything yet! Let's <a href="BrowseGamesGrid.aspx"><b>go shopping!</b></a></center></td>
                                                <tr />
                                                <%
                                              }%>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="box-account">
                                <div class="page-title">
                                    <h2>Account Information</h2>
                                </div>
                                <div class="col2-set">
                                    <div class="col-1">
                                        <h5>Contact Information</h5>
                                        <a href="MyAccountInformation.aspx">Edit</a>
                                        <p runat="server" id="txtName">Prabash Darshana </p>
                                        <a href="MyAccountChangePassword.aspx">Change Password</a>
                                        <p runat="server" id="txtEmail">prabash.darshana@gmail.com </p>
                                    </div>
                                    <div class="col-2">
                                        <h5>Primary Billing Address</h5>
                                        <a href="MyAccountInformation.aspx">Edit Address</a>
                                        <address runat="server" id="txAddress">
                                            No.36/3
                                            <br />
                                            St.Anthony's Mawatha
                                            <br />
                                            Colombo 13<br />
                                        </address>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">My Account</div>
                        <div class="block-content">
                            <ul>
                                <li class="current"><a href="#">Account Dashboard</a></li>
                                <li><a href="MyAccountInformation.aspx">Account Information</a></li>
                                <li><a href="MyAccountOrders.aspx">My Orders</a></li>
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
