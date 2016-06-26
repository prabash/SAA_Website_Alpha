<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckoutMethod.aspx.cs" Inherits="TEST_ASP_ALPHA_1.CheckoutMethod" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- main-container -->
    <div class="main-container col2-right-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="row">
                <section class="col-main col-sm-12">
                    <div class="checkout-page">
                        <div class="page-title new_page_title">
                            <h1>Checkout</h1>
                        </div>
                        <ol class="one-page-checkout" id="checkoutSteps">
                            <li id="opc-login" class="section allow active">
                                <div class="step-title">
                                    <span class="number">1</span>
                                    <h3 class="one_page_heading">Checkout Method</h3>
                                    <!--<a href="#">Edit</a>-->
                                </div>
                                <div id="checkout-step-login" class="step a-item" style="">
                                    <div class="col2-set">
                                        <div class="col-1">
                                            <h2>Register & Checkout</h2>
                                            <ul class="ul">
                                                <li>Fast and easy check out</li>
                                                <li>Easy access to your order history and status</li>
                                            </ul>
                                            <div class="buttons-set">
                                                <p class="required">&nbsp;</p>
                                                <button onclick="window.location.href='CheckoutBillingInfoReg.aspx';" class="button continue" type="button" id="onepage-guest-register-button"><span><span>Continue</span></span></button>
                                            </div>
                                        </div>
                                        <div class="col-2">
                                            <h2>Login</h2>
                                            <form class="content" ID="form2" runat="server">
                                                <p>If you have an account with us, please log in.</p>
                                                <ul class="form-list">
                                                    <li>
                                                        <label>Email Address <span class="required">*</span></label>
                                                        <br>
                                                        <asp:TextBox type="email" title="Email Address" class="input-text required-entry" value="" id="dfnLoginEmail" runat="server" required/>
                                                    </li>
                                                    <li>
                                                        <label for="pass">Password <span class="required">*</span></label>
                                                        <br>
                                                        <asp:TextBox TextMode="Password" title="Password" class="input-text required-entry validate-password" ID="dfnLoginPass" runat="server" required/>
                                                    </li>
                                                </ul>
                                                <div class="error" runat="server" ID="regErrorBox" visible="false" style="height:30px;margin-top:10px;padding:8px;">
                                                    <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                                        <p runat="server" ID="regErrMsg"></p> 
                                                </div>
                                                <p class="required">* Required Fields</p>
                                                <div class="buttons-set">
                                                    <asp:Button CssClass="button login" ID="btnLogin" Text="LOGIN" runat="server" OnClick="btnLogin_Click"/>
                                                    <a class="f-right" href="ResetPassword.aspx">Forgot Your Password?</a>
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <li id="opc-billing" class="section">
                                <div class="step-title">
                                    <span class="number">2</span>
                                    <h3 class="one_page_heading">Billing Information</h3>
                                </div>
                            </li>
                            <li id="opc-payment" class="section">
                                <div class="step-title">
                                    <span class="number">3</span>
                                    <h3 class="one_page_heading">Payment Information</h3>
                                </div>
                            </li>
                            <li id="opc-review" class="section">
                                <div class="step-title">
                                    <span class="number">4</span>
                                    <h3 class="one_page_heading">Order Review</h3>
                                </div>
                            </li>
                        </ol>
                    </div>
                </section>
            </div>
        </div>
    </div>
    <!--End main-container -->
</asp:Content>
