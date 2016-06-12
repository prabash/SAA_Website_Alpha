<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TEST_ASP_ALPHA_1.Register" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <section class="main-container col1-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="account-login">
                <div class="page-title">
                    <h2>Create an Account</h2>
                </div>
                <fieldset class="col2-set">
                    <legend>Create an Account</legend>
                    <div class="col-1 new-users">
                        <strong>New Customers</strong>
                        <form class="content" runat="server" ID="registerForm" method="post">
                            <p>Creating an account will allow you to move through the checkout process faster, and to keep in touch with us.</p>
                            <ul class="form-list">
                                <li>
                                    <label>Email Address <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox type="email" CssClass="input-text required-entry" value="" runat="server" ID="dfnRegEmail" required/>
                                </li>
                                <li>
                                    <label for="pass">Username <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox CssClass="input-text required-entry" runat="server" ID="dfnRegUsername" required/>
                                </li>
                                <li>
                                    <label for="pass">Password <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfnRegPass" required/>
                                    <br />
                                    <asp:RegularExpressionValidator ID="Regex1" runat="server" 
                                        ControlToValidate="dfnRegPass"
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$" 
                                        ErrorMessage="Password must contain: Minimum 6 characters, at least 1 Alphabet and 1 Number" 
                                        ForeColor="Red" Display="Dynamic" Font-Bold="True"/>
                                </li>
                                <li>
                                    <label for="pass">Confirm Password <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfnRegConfPass" required/>
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                         ControlToValidate="dfnRegConfPass"
                                         CssClass="ValidationError"
                                         ControlToCompare="dfnRegPass"
                                         ErrorMessage="Passwords do not match!" 
                                         ToolTip="Password must be the same" Display="Dynamic" Font-Bold="True" ForeColor="Red" />
                                </li>
                            </ul>
                            <div class="error" runat="server" ID="regErrorBox" visible="false" style="height:30px;margin-top:10px;padding:8px;">
                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                    <p runat="server" ID="regErrMsg"></p> 
                            </div>
                            <div class="success" runat="server" ID="regSuccessBox" visible="false" style="height:30px;margin-top:10px;padding:8px;">
                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                    <p runat="server" ID="regSuccessMsg">Thank you! Please check your email for the activation link :)</p>
                            </div>
                            <p class="required">* Required Fields</p>
                            <div class="buttons-set">
                                <asp:Button class="button create-account" type="button" runat="server" ID="btnRegCreateAccount" OnClick="btnRegCreateAccount_Click" Text="CREATE AN ACCOUNT"></asp:Button>
                            </div>
                        </form>
                    </div>
                    <div class="col-2 registered-users">
                        <strong>Registered Customers</strong>
                        <form class="content" ID="form2">
                            <p>Do you already have an account with us? If so, please log in.</p>
                            <div class="buttons-set">
                                <button type="button" class="button login" onclick="redirect();" id="btnLogin"><span>Login</span></button>
                            </div>
                        </form>
                    </div>
                </fieldset>
            </div>
        </div>
    </section>

    <script type="text/javascript">
        function redirect() {
            var uri = window.location.href;
            window.location.replace(uri.replace('Register', 'Login'));
        }
    </script>
</asp:Content>