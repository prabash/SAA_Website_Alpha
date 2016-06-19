<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccountInformation.aspx.cs" Inherits="TEST_ASP_ALPHA_1.MyAccountInformation" MasterPageFile="~/Site.Master" %>
<%@ Import Namespace="TEST_ASP_ALPHA_1.Common" %>

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
                    <div class="checkout-page">
                        <div class="page-title new_page_title">
                            <h1>Account Information</h1>
                        </div>
                        <div class="welcome-msg">
                            <strong>Hello there, <%=Session[CommonManager.GetCustNameSessionName()].ToString()%>!</strong>
                            <p>You can edit your account information here.</p>
                        </div>
                        <div id="checkout-step-billing" class="step a-item" style="">
                            <form runat="server" ID="frmAccountInfo" method="post">
                                <fieldset class="group-select">
                                    <ul>
                                        <li id="billing-new-address-form">
                                            <fieldset>
                                                <legend>New Address</legend>
                                                <input type="hidden" name="billing[address_id]" value="" id="billing:address_id" />
                                                <ul>
                                                    <li>
                                                        <div class="customer-name">
                                                            <div class="input-box name-firstname">
                                                                <label for="billing:firstname">Full Name<span class="required">*</span></label>
                                                                <br />
                                                                <input type="text" class="input-text required-entry" title="Full Name" runat="server" ID="dfName" required />
                                                            </div>
                                                            <div class="input-box name-lastname">
                                                                <label for="billing:lastname">NIC<span class="required">*</span> </label>
                                                                <br />
                                                                <input type="text" id="dfNic" title="NIC No." class="input-text required-entry" runat="server" required />
                                                            </div>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <label>Address  <span class="required">*</span></label>
                                                        <br />
                                                        <input type="text" title="Street Address" id="dfAddLine1" value="" class="input-text required-entry" placeholder="Address Line 1" runat="server" required />
                                                    </li>
                                                    <li>
                                                        <input type="text" title="Street Address 2" id="dfAddLine2" value="" class="input-text" placeholder="Address Line 2" runat="server" required />
                                                    </li>
                                                    <li>
                                                        <div class="input-box">
                                                            <label for="shipping:city">City <span class="required">*</span></label>
                                                            <br />
                                                            <select id="cmbCity" title="City" class="validate-select" style="" runat="server" required>
                                                                <option value="">Please select the city</option>
                                                                <option value="Colombo 1">Colombo 1</option>
                                                                <option value="Colombo 2">Colombo 2</option>
                                                                <option value="Colombo 3">Colombo 3</option>
                                                                <option value="Colombo 4">Colombo 4</option>
                                                                <option value="Colombo 5">Colombo 5</option>
                                                                <option value="Colombo 6">Colombo 6</option>
                                                                <option value="Colombo 7">Colombo 7</option>
                                                                <option value="Colombo 8">Colombo 8</option>
                                                                <option value="Colombo 9">Colombo 9</option>
                                                                <option value="Colombo 10">Colombo 10</option>
                                                                <option value="Colombo 11">Colombo 11</option>
                                                                <option value="Colombo 12">Colombo 12</option>
                                                                <option value="Colombo 13">Colombo 13</option>
                                                                <option value="Colombo 14">Colombo 14</option>
                                                                <option value="Colombo 15">Colombo 15</option>
                                                                <option value="Dehiwala">Dehiwala</option>
                                                                <option value="Katubedda">Katubedda</option>
                                                                <option value="Kottawa">Kottawa</option>
                                                                <option value="Moratuwa">Moratuwa</option>
                                                                <option value="Mt. Lavinia">Mt. Lavinia</option>
                                                                <option value="Panadura">Panadura</option>
                                                                <option value="Piliyandala">Piliyandala</option>
                                                                <option value="Ratmalana">Ratmalana</option>
                                                            </select>
                                                        </div>
                                                        <div class="input-box">
                                                            <label for="billing:telephone">Telephone <span class="required">*</span></label>
                                                            <br />
                                                            <input type="text" value="" title="Telephone" class="input-text required-entry" runat="server" id="dfTelephone" required />
                                                        </div>
                                                    </li>
                                                </ul>
                                            </fieldset>
                                        </li>
                                    </ul>
                                    <div class="error" runat="server" id="regErrorBox" visible="false" style="height: 30px; margin-top: 10px; padding: 8px;">
                                        <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>
                                        <p runat="server" id="regErrMsg"></p>
                                    </div>
                                    <div class="success" runat="server" id="regSuccessBox" visible="false" style="height: 30px; margin-top: 10px; padding: 8px;">
                                        <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>
                                        <p runat="server" id="regSuccessMsg">Successfully updated details!</p>
                                    </div>
                                    <p class="require"><em class="required">* </em>Required Fields</p>
                                    <asp:Button CssClass="button continue" runat="server" ID="btnUpdate" Text="UPDATE DETAILS" OnClick="btnUpdate_Click" />
                                </fieldset>
                            </form>
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">My Account</div>
                        <div class="block-content">
                            <ul>
                                <li><a href="MyAccountDashboard.aspx">Account Dashboard</a></li>
                                <li class="current"><a href="#">Account Information</a></li>
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
