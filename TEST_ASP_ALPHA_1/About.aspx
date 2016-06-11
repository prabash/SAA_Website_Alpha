﻿<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="TEST_ASP_ALPHA_1.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>About Us</strong></li>
                </ul>
            </div>
        </div>
    </div>
    <!-- main-container -->
    <div class="main-container col2-right-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="row">
                <section class="col-main col-sm-9">
                    <div class="static-contain">
                        <div class="page-title new_page_title">
                            <h2>About Us</h2>
                        </div>

                        <img src="images/about_us.jpg" style="padding-bottom:10px;height:100%;width:100%;"/>
                        <br/>
                        <p>We are the market leaders selling latest games, movies and tv series.</p>
                        <br/>
                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum</p>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-company">
                        <div class="block-title">Company </div>
                        <div class="block-content">
                            <ol id="recently-viewed-items">
                                <li class="item odd"><strong>About Us</strong></li>
                                <li class="item even"><a href="Contact.aspx">Contact Us</a></li>
                                <li class="item last"><a href="BrowseGamesGrid.aspx">Browse...</a></li>
                            </ol>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <!--End main-container -->
</asp:Content>
