<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TEST_ASP_ALPHA_1.Login" MasterPageFile="~/Site.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Login</strong></li>
                </ul>

            </div>
        </div>
    </div>
    <section class="main-container col1-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="account-login">
                <div class="page-title">
                    <h2>Login to your account</h2>
                </div>
                <fieldset class="col2-set">
                    <legend>Login to your Account</legend>
                    <div class="col-1 registered-users">
                        <strong>Registered Customers</strong>
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
                            <p class="required">* Required Fields</p>
                            <div class="buttons-set">
                                <asp:Button CssClass="button login" ID="btnLogin" Text="LOGIN" runat="server" OnClick="btnLogin_Click"/>
                                <a class="forgot-word" href="ResetPassword.aspx">Forgot Your Password?</a>
                            </div>
                        </form>
                    </div>
                    <div class="col-2 new-users">
                        <strong>New Customers</strong>
                        <form class="content" ID="registerForm" method="post">
                            <p>Are you new to SAA Videos? If so, please register with us.</p>
                            
                            <div class="buttons-set">
                                <button class="button create-account" type="button" ID="btnRegCreateAccount" onclick="redirect('Login','Register');"> <span>Create an Account</span></button>
                            </div>
                        </form>
                    </div>
                </fieldset>
            </div>
        </div>
    </section>
</asp:Content>


