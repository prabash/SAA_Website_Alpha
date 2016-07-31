<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="403Error.aspx.cs" Inherits="TEST_ASP_ALPHA_1._403Error" MasterPageFile="~/Site.Master" Title="403 : Forbidden" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-wrapper" style="margin-left: -15px">
        <div class="container">
            <div class="col-main">
                <div class="error_404">
                    <div class="std">
                        <div class="page-not-found wow bounceInRight animated">
                            <h2>403</h2>
                            <h3>
                                <img src="images/signal.png" alt="error image">Forbidden : Are you sure you're in the right place?!</h3>
                            <div><a href="Default.aspx" class="btn-home"><span>Back To Home</span></a></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
