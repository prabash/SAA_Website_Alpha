<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminChangePassword.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminChangePassword" MasterPageFile="~/Admin.Master" Title="Admin Change Password" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Change Password</strong></li>
                </ul>

            </div>
        </div>
    </div>
    <section class="main-container col1-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="account-login">
                <div class="page-title">
                    <h2>Change Password</h2>
                </div>
                <fieldset class="col1-set">
                    <legend>Login</legend>
                    <div class="col-1 registered-users">
                        <strong>Admin</strong>
                        <form class="content" id="AdminChangePasswordForm" runat="server">
                            <p>Hello Admin! Please login here.</p>
                            <ul class="form-list">
                                <li>
                                    <label for="pass">Old Password <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfAdminOldPass" />
                                </li>
                                <li>
                                    <label for="pass">New Password <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfAdminNewPass" />
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="dfAdminNewPass"
                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"
                                        ErrorMessage="Password must contain: Minimum 6 characters, at least 1 Alphabet and 1 Number"
                                        ForeColor="Red" Display="Dynamic" Font-Bold="True" />
                                </li>
                                <li>
                                    <label for="pass">Confirm Password <span class="required">*</span></label>
                                    <br>
                                    <asp:TextBox TextMode="Password" CssClass="input-text required-entry validate-password" runat="server" ID="dfAdminConNewfPass" />
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator2" runat="server"
                                        ControlToValidate="dfAdminConNewfPass"
                                        CssClass="ValidationError"
                                        ControlToCompare="dfAdminNewPass"
                                        ErrorMessage="Passwords do not match!"
                                        ToolTip="Password must be the same" Display="Dynamic" Font-Bold="True" ForeColor="Red" />
                                </li>
                            </ul>
                            <div class="error" runat="server" id="regErrorBox" visible="false" style="height: 30px; margin-top: 10px; padding: 8px;">
                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>
                                <p runat="server" id="regErrMsg"></p>
                            </div>
                            <div class="success" runat="server" id="regSuccessBox" visible="false" style="height: 30px; margin-top: 10px; padding: 8px;">
                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>
                                <p runat="server" id="P3">Successfully updated the password!</p>
                            </div>
                            <div class="success" runat="server" id="Div4" style="height: 30px; margin-top: 10px; padding: 8px;">
                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>
                                <p runat="server" id="P4">If you are a new Admin and want to change the default password, the default password is : Password123</p>
                            </div>
                            <p class="require"><em class="required">* </em>Required Fields</p>
                            <div class="buttons-set">
                                <asp:Button CssClass="button continue" runat="server" ID="Button1" Text="CHANGE PASSWORD" OnClick="btnChangePassword_Click" />
                            </div>
                        </form>
                    </div>
                </fieldset>
            </div>
        </div>
    </section>
</asp:Content>
