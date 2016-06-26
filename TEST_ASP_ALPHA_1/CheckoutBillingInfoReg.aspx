<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckoutBillingInfoReg.aspx.cs" Inherits="TEST_ASP_ALPHA_1.CheckoutBillingInfo" MasterPageFile="~/Site.Master" %>

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
                            <li id="opc-login" class="section">
                                <div class="step-title">
                                    <span class="number">1</span>
                                    <h3 class="one_page_heading">Checkout Method</h3>
                                </div>
                            </li>
                            <li id="opc-billing" class="section allow active">
                                <div class="step-title">
                                    <span class="number">2</span>
                                    <h3 class="one_page_heading">Billing Information : Register</h3>
                                    <!--<a href="#">Edit</a>-->
                                </div>
                                <div id="checkout-step-billing" class="step a-item" style="">
                                    <form class="content" runat="server" id="registerForm" method="post">
                                        <fieldset class="group-select">
                                            <ul>
                                                <li id="shipping-new-address-form">
                                                    <fieldset>
                                                        <input type="hidden" name="shipping[address_id]" value="9681" id="shipping:address_id" />
                                                        <ul>
                                                            <li>
                                                                <div class="customer-name">
                                                                    <div class="input-box name-firstname">
                                                                        <label>Email Address <span class="required">*</span></label>
                                                                        <br>
                                                                        <asp:TextBox type="email" CssClass="input-text required-entry" value="" runat="server" ID="dfnRegEmail" required />
                                                                    </div>
                                                                    <div class="input-box name-lastname">
                                                                        <label for="pass">Username <span class="required">*</span></label>
                                                                        <br>
                                                                        <asp:TextBox CssClass="input-text required-entry" runat="server" ID="dfnRegUsername" required />
                                                                    </div>
                                                                </div>
                                                            </li>
                                                            <li>
                                                                <div class="input-box">
                                                                    <label for="pass">Password <span class="required">*</span></label>
                                                                    <br>
                                                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfnRegPass" required />
                                                                    <br />
                                                                    <asp:RegularExpressionValidator ID="Regex1" runat="server"
                                                                        ControlToValidate="dfnRegPass"
                                                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"
                                                                        ErrorMessage="Password must contain: Minimum 6 characters, at least 1 Alphabet and 1 Number"
                                                                        ForeColor="Red" Display="Dynamic" Font-Bold="True" />
                                                                </div>
                                                                <div class="input-box">
                                                                    <label for="pass">Confirm Password <span class="required">*</span></label>
                                                                    <br>
                                                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfnRegConfPass" required />
                                                                    <br />
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                                        ControlToValidate="dfnRegConfPass"
                                                                        CssClass="ValidationError"
                                                                        ControlToCompare="dfnRegPass"
                                                                        ErrorMessage="Passwords do not match!"
                                                                        ToolTip="Password must be the same" Display="Dynamic" Font-Bold="True" ForeColor="Red" />
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
                                                <p runat="server" id="regSuccessMsg">Thank you! Please check your email for the activation link to activate & continue your checkout</p>
                                            </div>
                                            <p class="require"><em class="required">* </em>Required Fields</p>
                                            <div class="buttons-set1" id="shipping-buttons-container">
                                                <asp:Button class="button create-account" type="button" runat="server" ID="btnRegCreateAccount" OnClick="btnRegCreateAccount_Click" Text="REGISTER & CONTINUE"></asp:Button>
                                            </div>
                                        </fieldset>
                                    </form>
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
