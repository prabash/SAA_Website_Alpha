﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseGiftsGrid.aspx.cs" Inherits="TEST_ASP_ALPHA_1.BrowseGiftsGrid" MasterPageFile="~/Site.Master"%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="breadcrumbs" style="margin-left:-15px">
    <div class="container">
      <div class="row">
        <ul>
          <li class="home"> <a href="Default.aspx" title="Go to Home Page">Home</a><span>&raquo;</span></li>
          <li class=""> <a href="#" title="Go to Home Page">Browse</a><span>&raquo;</span></li>
          <li class="category13"><strong>Gifts</strong></li>
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
            <h1>Gifts</h1>
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
                <div class="view-mode"> <span title="Grid" class="button button-active button-grid">Grid</span><a href="#" onclick="navigateWithParam('BrowseGiftsGrid', 'BrowseGiftsList');" title="List" class="button button-list">List</a> </div>
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
                <dt class="even">Sales</dt>
                  <dd class="even">
                  <ol runat="server" id="salesSearch"> </ol>
                </dd>
              </dl>
            </div>
          </div>
          <div class="block block-cart">
            <div class="block-title ">My Cart</div>
            <div class="block-content">
              <div class="summary">
                <p class="amount">There are <a href="#">2 items</a> in your cart.</p>
                <p class="subtotal"> <span class="label">Cart Subtotal:</span> <span class="price">$27.99</span> </p>
              </div>
              <div class="ajax-checkout">
                <button type="submit" title="Submit" class="button button-checkout"><span>Checkout</span></button>
              </div>
              <p class="block-subtitle">Recently added item(s) </p>
              <ul>
                <li class="item"> <a class="product-image" title="Fisher-Price Bubble Mower" href="product_detail.html"><img width="80" alt="product-image" src="products_images/product1.jpg"></a>
                  <div class="product-details">
                    <div class="access"> <a class="btn-remove1" title="Remove This Item" href="#"> <span class="icon"></span> Remove </a> </div>
                    <p class="product-name"> <a href="product_detail.html">Sample Product</a> </p>
                  <strong>1</strong> x <span class="price">$19.99</span> </div>
                </li>
                <li class="item last"> <a class="product-image" title="Prince Lionheart Jumbo Toy Hammock" href="product_detail.html"><img width="80" alt="product-image" src="products_images/product1.jpg"></a>
                  <div class="product-details">
                    <div class="access"> <a class="btn-remove1" title="Remove This Item" href="#"> <span class="icon"></span> Remove </a> </div>
                    <p class="product-name"> <a href="product_detail.html"> Sample Product</a> </p>
                    <strong>1</strong> x <span class="price">$8.00</span> 
                    <!--access clearfix--> 
                  </div>
                </li>
              </ul>
            </div>
          </div>
          <div class="block block-subscribe">
            <div class="block-title">Newsletter</div>
            <form id="newsletter-validate-detail" method="post" action="http://www.magikcommerce.com//newsletter/subscriber/new/">
              <div class="block-content">
                <div class="form-subscribe-header"> Sign up for our newsletter:</div>
                <input type="text" placeholder="Enter your email address" class="input-text required-entry validate-email" title="" id="newsletter" name="email">
                <div class="actions">
                  <button class="button button-subscribe" title="Submit" type="submit"><span>Subscribe</span></button>
                </div>
              </div>
            </form>
          </div>
        </aside>
      </div>
    </div>
  </div>
</asp:Content>

