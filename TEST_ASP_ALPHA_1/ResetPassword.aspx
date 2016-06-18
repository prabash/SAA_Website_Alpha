<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="TEST_ASP_ALPHA_1.ResetPassword" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Reset Password</strong></li>
                </ul>

            </div>
        </div>
    </div>
    <!-- main-container -->
    <section class="main-container col1-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="account-login">
                <div class="page-title">
                    <h2>Reset your password</h2>
                </div>
                <fieldset class="col2-set">
                    <legend>Reset your password</legend>
                    <div class="col-1 new-users">
                        <strong>Registered Customers</strong>
                        <form class="content" runat="server" ID="registerForm" method="post">
                            <p>Forgot your password? Please enter your email address and we'll send you a link to reset your password</p>
                            <ul class="form-list">
                                <li runat="server" ID="liEmailAddress" visible="false">
                                    <label>Email Address <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox type="email" CssClass="input-text required-entry" value="" runat="server" ID="dfnResetEmail" required/>
                                </li>
                                <li runat="server" ID="liPassword" visible="false">
                                    <label for="pass">New Password <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfnResetPass" required/>
                                    <br />
                                    <asp:RegularExpressionValidator ID="Regex1" runat="server" 
                                        ControlToValidate="dfnResetPass"
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$" 
                                        ErrorMessage="Password must contain: Minimum 6 characters, at least 1 Alphabet and 1 Number" 
                                        ForeColor="Red" Display="Dynamic" Font-Bold="True"/>
                                </li>
                                <li runat="server" ID="liConfPassword" visible="false">
                                    <label for="pass">Confirm Password <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfnResetConfPass" required/>
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                         ControlToValidate="dfnResetConfPass"
                                         CssClass="ValidationError"
                                         ControlToCompare="dfnResetPass"
                                         ErrorMessage="Passwords do not match!" 
                                         ToolTip="Password must be the same" Display="Dynamic" Font-Bold="True" ForeColor="Red" />
                                </li>
                            </ul>
                            <div class="error" runat="server" ID="regErrorBox" visible="false" style="height:30px;margin-top:10px;padding:8px;">
                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                    <p runat="server" ID="regErrMsg"></p> 
                            </div>
                            <div class="success" runat="server" ID="regSuccessBoxEmail" visible="false" style="height:30px;margin-top:10px;padding:8px;">
                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                    <p runat="server" ID="regSuccessMsgEmail">Success! We have emailed you a link to reset your password.</p>
                            </div>
                             <div class="success" runat="server" ID="regSuccessBoxPassword" visible="false" style="height:30px;margin-top:10px;padding:8px;">
                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                    <p runat="server" ID="regSuccessMsgPassword">Success!. You can login to your account now.</p>
                            </div>
                            <p class="required">* Required Fields</p>
                            <div class="buttons-set">
                                <asp:Button class="button create-account" type="button" runat="server" ID="btnResetPasswordEmail" OnClick="btnResetPasswordEmail_Click" Text="RESET PASSWORD" Visible="false"></asp:Button>
                                <asp:Button class="button create-account" type="button" runat="server" ID="btnResetPassword" OnClick="btnResetPassword_Click" Text="RESET PASSWORD" Visible="false"></asp:Button>
                            </div>
                        </form>
                    </div>
                    <div class="col-2 registered-users">
                        <strong>Registered Customers</strong>
                        <form class="content" ID="form2">
                            <p>Did you just remember your password? If so, please log in.</p>
                            <div class="buttons-set">
                                <button type="button" class="button login" onclick="redirect('ResetPassword', 'Login');" id="btnLogin"><span>Login</span></button>
                            </div>
                        </form>
                    </div>
                </fieldset>
            </div>
        </div>
    </section>
    <!--End main-container -->
</asp:Content>

