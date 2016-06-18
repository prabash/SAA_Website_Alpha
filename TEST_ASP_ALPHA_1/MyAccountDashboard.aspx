<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccountDashboard.aspx.cs" Inherits="TEST_ASP_ALPHA_1.MyAccountDashboard" MasterPageFile="~/Site.Master" %>

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
              <div class="welcome-msg"> <strong>Hello, pranali deshmukh!</strong>
                <p>From your My Account Dashboard you have the ability to view a snapshot of your recent account activity and update your account information. Select a link below to view or edit information.</p>
              </div>
              <div class="recent-orders">
                <div class="title-buttons"><strong>Recent Orders</strong> <a href="#">View All </a> </div>
                <div class="table-responsive">
                  <table class="data-table" id="my-orders-table">
                    
                    <thead>
                      <tr class="first last">
                        <th>Order #</th>
                        <th>Date</th>
                        <th>Ship to</th>
                        <th><span class="nobr">Order Total</span></th>
                        <th>Status</th>
                        <th>&nbsp;</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr class="first odd">
                        <td>500000002</td>
                        <td>9/9/10 </td>
                        <td>pranali d</td>
                        <td><span class="price">$5.00</span></td>
                        <td><em>Pending</em></td>
                        <td class="a-center last"><span class="nobr"> <a href="#">View Order</a> <span class="separator">|</span> <a href="#">Reorder</a> </span></td>
                      </tr>
                      <tr class="last even">
                        <td>500000001</td>
                        <td>9/9/10 </td>
                        <td>pranali d</td>
                        <td><span class="price">$1,397.99</span></td>
                        <td><em>Pending</em></td>
                        <td class="a-center last"><span class="nobr"> <a href="#">View Order</a> <span class="separator">|</span> <a href="#">Reorder</a> </span></td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>
              <div class="box-account">
                <div class="page-title">
                  <h2>Account Information</h2>
                </div>
                <div class="col2-set">
                  <div class="col-1">
                    <h5>Contact Information</h5>
                    <a href="#">Edit</a>
                    <p> pranali deshmukh<br>
                      pranalid15@gmail.com<br>
                      <a href="#">Change Password</a> </p>
                  </div>
                  <div class="col-2">
                    <h5>Newsletters</h5>
                    <a href="#">Edit</a>
                    <p> You are currently not subscribed to any newsletter. </p>
                  </div>
                </div>
                <div class="col2-set">
                  <h4>Address Book</h4>
                  <div class="manage_add"><a href="#">Manage Addresses</a> </div>
                  <div class="col-1">
                    <h5>Primary Billing Address</h5>
                    <address>
                    pranali d<br>
                    aundh<br>
                    tyyrt,  Alabama, 46532<br>
                    United States<br>
                    T: 454541 <br>
                    <a href="#">Edit Address</a>
                    </address>
                  </div>
                  <div class="col-2">
                    <h5>Primary Shipping Address</h5>
                    <address>
                    pranali d<br>
                    aundh<br>
                    tyyrt,  Alabama, 46532<br>
                    United States<br>
                    T: 454541 <br>
                    <a href="#">Edit Address</a>
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
