<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminLogin"  MasterPageFile="~/Admin.Master" Title="Admin Login"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Admin Login</strong></li>
                </ul>

            </div>
        </div>
    </div>
    <section class="main-container col1-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="account-login">
                <div class="page-title">
                    <h2>Admin login</h2>
                </div>
                <fieldset class="col2-set">
                    <legend>Login</legend>
                    <div class="col-1 registered-users">
                        <strong>Admin</strong>
                        <form class="content" ID="form2" runat="server">
                            <p>Hello Admin! Please login here.</p>
                            <ul class="form-list">
                                <li>
                                    <label>Username <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox type="text" title="Email Address" class="input-text required-entry" value="" ID="dfnLoginUsername" runat="server" required/>
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
                            </div>
                        </form>
                    </div>
                </fieldset>
            </div>
        </div>
    </section>
</asp:Content>