<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminDashboard" MasterPageFile="~/Admin.Master" Title="Dashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Slider -->
    <div class="page" style="margin-left: -15px">
        <div class="container">
            <div class="slider-section">
                <div class="slider-intro">
                    <div class="the-slideshow slideshow-wrapper">
                        <ul class="slideshow" style="overflow: hidden;" runat="server" id="slideShowList"></ul>
                        <a id="home-slides-prev" class="backward browse-button" href="#">previous</a> <a id="home-slides-next" class="forward browse-button" href="#">next</a>
                        <div id="home-slides-pager" class="tab-pager tab-pager-img tab-pager-ring-lgray">&nbsp;</div>
                    </div>
                </div>
            </div>
        </div>
        <!-- end Slider -->
        <!-- main container -->
        <!-- End main container -->
    </div>
</asp:Content>
