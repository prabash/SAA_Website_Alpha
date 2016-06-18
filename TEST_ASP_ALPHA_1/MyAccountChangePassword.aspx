<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccountChangePassword.aspx.cs" Inherits="TEST_ASP_ALPHA_1.MyAccountChangePassword" MasterPageFile="~/Site.Master" %>

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
                            <h1>Change Password</h1>
                        </div>
                        <div class="welcome-msg">
                            <p>Hey, <strong><%=Session[CommonManager.GetCustNameSessionName()].ToString()%></strong>, change your current password here.</p>
                        </div>
                        <div id="checkout-step-billing" class="step a-item" style="">
                            <form runat="server" id="frmAccountInfo" method="post">
                                <fieldset class="group-select">
                                    <ul>
                                        <li id="billing-new-address-form">
                                            <fieldset>
                                                <legend>New Address</legend>
                                                <input type="hidden" name="billing[address_id]" value="" id="billing:address_id" />
                                                <ul>
                                                    <li>
                                                        <label for="pass">Old Password <span class="required">*</span></label>
                                                        <br>
                                                        <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfOldPass" required />
                                                    </li>
                                                    <li>
                                                        <label for="pass">New Password <span class="required">*</span></label>
                                                        <br>
                                                        <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfNewPass" required />
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="Regex1" runat="server"
                                                            ControlToValidate="dfNewPass"
                                                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"
                                                            ErrorMessage="Password must contain: Minimum 6 characters, at least 1 Alphabet and 1 Number"
                                                            ForeColor="Red" Display="Dynamic" Font-Bold="True" />
                                                    </li>
                                                    <li>
                                                        <label for="pass">Confirm Password <span class="required">*</span></label>
                                                        <br>
                                                        <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfConNewfPass" required />
                                                        <br />
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                            ControlToValidate="dfConNewfPass"
                                                            CssClass="ValidationError"
                                                            ControlToCompare="dfNewPass"
                                                            ErrorMessage="Passwords do not match!"
                                                            ToolTip="Password must be the same" Display="Dynamic" Font-Bold="True" ForeColor="Red" />
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
                                        <p runat="server" id="regSuccessMsg">Successfully updated the password!</p>
                                    </div>
                                    <p class="require"><em class="required">* </em>Required Fields</p>
                                    <asp:Button CssClass="button continue" runat="server" ID="btnChangePassword" Text="CHANGE PASSWORD" OnClick="btnChangePassword_Click" />
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
                                <li><a href="MyAccountInformation.aspx">Account Information</a></li>
                                <li><a href="MyAccountOrders.aspx">My Orders</a></li>
                                <li><a href="MyAccountProductReviews.aspx">My Product Reviews</a></li>
                                <li><a href="MyAccountWishlist.aspx">My Wishlist</a></li>
                                <li class="current last"><a href="#">Change Password</a></li>
                            </ul>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <!--End main-container -->
</asp:Content>
