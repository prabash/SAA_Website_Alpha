﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseGamesGrid.aspx.cs" Inherits="TEST_ASP_ALPHA_1.BrowseItemGrid" MasterPageFile="~/Site.Master"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="breadcrumbs" style="margin-left:-15px">
    <div class="container">
      <div class="row">
        <ul>
          <li class="home"> <a href="Default.aspx" title="Go to Home Page">Home</a><span>&raquo;</span></li>
          <li class=""> <a href="#" title="Go to Home Page">Browse</a><span>&raquo;</span></li>
          <li class="category13"><strong>Games</strong></li>
        </ul>
      </div>
    </div>
  </div>
  <div class="main-container col2-left-layout" style="margin-left:-15px">
    <div class="main container">
      <div class="row">
        <section class="col-sm-9 col-sm-push-3">
        <div class="col-main">
          <div class="category-title">
            <h1>Games</h1>
          </div>
          <div class="category-description std">
            <div class="slider-items-products">
              <div id="category-desc-slider" class="product-flexslider hidden-buttons">
                <div class="slider-items slider-width-col1" runat="server" id="itemSlideShow"> 
                </div>
              </div>
            </div>
          </div>
          <div class="category-products">
            <div class="toolbar">
              <div class="sorter">
                <div class="view-mode"> <span title="Grid" class="button button-active button-grid">Grid</span><a href="#" onclick="navigateWithParam('BrowseGamesGrid', 'BrowseGamesList');" title="List" class="button button-list">List</a> </div>
              </div>
              <div id="sort-by">
                <label class="left">Sort By: </label>
                <ul id="sortByList" runat="server">
                  
                </ul>
                <a class="button-asc left" href="#" title="Desc" onclick="insertParam('asc', 'false');"><span style="color:#33cc99;font-size:11px;" class="glyphicon glyphicon-arrow-down" id="anchorDesc" runat="server"></span></a>
                <a class="button-asc left" href="#" title="Asc" onclick="insertParam('asc', 'true');"><span style="color:#999;font-size:11px;" class="glyphicon glyphicon-arrow-up" runat="server" id="anchorAsc"></span></a>
              </div>
              <div class="pager">
                <div id="limiter">
                  <label>View: </label>
                  <ul id="viewPerPage" runat="server">
                  </ul>
                </div>
                <div class="pages">
                  <label>Page:</label>
                  <ul class="pagination" runat="server" id="paginationCtrl">
                  </ul>
                </div>
              </div>
            </div>
            <ul class="products-grid" runat="server" id="gridItemsList"></ul>
          </div>
		  </div>
        </section>
        <aside class="col-left sidebar col-sm-3 col-xs-12 col-sm-pull-9 wow bounceInUp animated">
          <div class="side-nav-categories">
            <div class="block-title"> Categories </div>
            <div class="box-content box-category">
              <ul id="magicat">
                <li class="level0- level0"> <span class="magicat-cat"><a href="BrowseGamesGrid.aspx"><span>Games</span></a></span> </li>
                <li class="level0- level0"> <span class="magicat-cat"><a href="BrowseTvSeriesGrid.aspx"><span>TV Series</span></a></span> </li>
                <li class="level0- level0"> <span class="magicat-cat"><a href="BrowseMoviesGrid.aspx"><span>Movies</span></a></span> </li>
                <li class="level0- level0"> <span class="magicat-cat"><a href="BrowseGiftsGrid.aspx"><span>Gifts</span></a></span> </li>
                <li class="level0- level0 last"> <span class="magicat-cat"><a href="BrowseElectronicsGrid.aspx"><span>Electronics</span></a></span> </li>
              </ul>
            </div>
          </div>
          <div class="block block-layered-nav">
            <div class="block-title">Shop By</div>
            <div class="block-content">
              <p class="block-subtitle">Shopping Options</p>
              <dl id="narrow-by-list">
                <dt class="odd">Price</dt>
                <dd class="odd">
                  <ol runat="server" id="priceRangeSearch"> </ol>
                </dd>
                <dt class="even">Year</dt>
                <dd class="even">
                  <ol runat="server" id="yearSearch"> </ol>
                </dd>
                <dt class="odd">Genre</dt>
                <dd class="odd">
                  <ol runat="server" id="genreSearch"> </ol>
                </dd>
                <dt class="even">Sales</dt>
                  <dd class="even">
                  <ol runat="server" id="salesSearch"> </ol>
                </dd>
              </dl>
            </div>
          </div>
          <div class="block block-subscribe">
                <div class="block-title">Newsletter</div>
                <form Id="newsLetterForm" method="post" runat="server">
                    <div class="block-content">
                        <div class="form-subscribe-header">Sign up for our newsletter:</div>
                        <input type="email" placeholder="Enter your email address" class="input-text required-entry validate-email" title="" Id="dfnEmailAdd" name="email" runat="server" style="z-index:10;" required>
                        <div class="success" runat="server" ID="successBox" style="width:210px;height:30px;margin-top:10px;padding:8px;" visible="false">
                            <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                Thank you!
                        </div>
                        <div class="error" runat="server" ID="errorBox" style="width:210px;height:30px;margin-top:10px;padding:8px;" visible="false">
                            <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                Error Occurred!
                        </div>
                        <div class="actions">
                            <asp:Button CssClass="button button-subscribe" Text="SUBSCRIBE" runat="server" ID="btnSubscribe" OnClick="btnSubscribe_Click"></asp:Button>
                        </div>
                    </div>
                </form>
            </div>
        </aside>
      </div>
    </div>
  </div>
</asp:Content>