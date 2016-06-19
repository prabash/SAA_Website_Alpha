<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccountWishlist.aspx.cs" Inherits="TEST_ASP_ALPHA_1.MyAccountWishlist" MasterPageFile="~/Site.Master" %>

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
                            <h2>My Wishlist</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, <%=Session[CommonManager.GetCustNameSessionName()].ToString()%>!</strong>
                                <p>Here you can see your wishlist!</p>
                            </div>
                            <div class="category-products">
                            <div class="toolbar">
                                <div id="sort-by">
                                    <label class="left">Sort By: </label>
                                    <ul id="sortByList" runat="server">
                                    </ul>
                                    <a class="button-asc left" href="#" title="Desc" onclick="insertParam('asc', 'false');"><span style="color: #33cc99; font-size: 11px;" class="glyphicon glyphicon-arrow-down" id="anchorDesc" runat="server"></span></a>
                                    <a class="button-asc left" href="#" title="Asc" onclick="insertParam('asc', 'true');"><span style="color: #999; font-size: 11px;" class="glyphicon glyphicon-arrow-up" runat="server" id="anchorAsc"></span></a>
                                </div>
                                <div class="pager">
                                    <div id="limiter">
                                        <label>View: </label>
                                        <ul id="viewPerPage" runat="server">
                                        </ul>
                                    </div>
                                    <div class="pages">
                                        <label>Page:</label>
                                        <ul class="pagination" runat="server" id="paginationCtrl">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <ul class="products-grid" runat="server" id="gridItemsList"></ul>
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">My Account</div>
                        <div class="block-content">
                            <ul>
                                <li><a href="MyAccountDashboard.aspx">Account Dashboard</a></li>
                                <li><a href="MyAccountInformation.aspx">Account Information</a></li>
                                <li><a href="MyAccountOrders.aspx">My Orders</a></li>
                                <li><a href="MyAccountProductReviews.aspx">My Product Reviews</a></li>
                                <li class="current"><a href="MyAccountWishlist.aspx">My Wishlist</a></li>
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
