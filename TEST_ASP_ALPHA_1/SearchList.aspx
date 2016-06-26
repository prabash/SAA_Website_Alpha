<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchList.aspx.cs" Inherits="TEST_ASP_ALPHA_1.Search" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- breadcrumbs -->
  <div class="breadcrumbs" style="margin-left:-15px">
    <div class="container">
      <div class="row">
        <ul>
          <li class="home"> <a href="Default.aspx" title="Go to Home Page">Home</a><span>&raquo;</span></li>
          <li class="category13"><strong>Search</strong></li>
        </ul>
      </div>
    </div>
  </div>
  <!-- End breadcrumbs --> 
  <!-- Two columns content -->
  <div class="main-container col2-left-layout" style="margin-left:-15px">
    <div class="main container">
      <div class="row">
        <section class="col-sm-9 col-sm-push-3">
        <div class="col-main">
          <div class="category-title">
            <h1>Search Results</h1>
          </div>
          <div class="category-products">
            <div class="toolbar">
              <div class="sorter">
                <div class="view-mode"> <a href="#" onclick="navigateWithParam('SearchList','SearchGrid');" title="Grid" class="button button-grid">Grid</a>&nbsp; <span title="List" class="button button-active button-list">List</span>&nbsp; </div>
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
            <ol class="products-list-alt products-list" id="productsList" runat="server" style="margin-top:-50px">
              <li class="item even" style="visibility:hidden;height:0px;margin-top:0px;" >
                <div class="product-image"> 
                  <a href="product_detail.html" title="HTC Rhyme Sense"> 
                    <img class="small-image" src="products_images/product1.jpg" alt="product-image" width="230"> 
                  </a> 
                </div>
                <div class="product-shop">
                  <h2 class="product-name">
                    <a title=" Sample Product" href="product_detail.html"> Sample Product </a>
                  </h2>
                  <div class="price-box">
                    <p class="old-price"> <span class="price-label"></span> <span id="old-price-212" class="price"> $442.99 </span> </p>
                    <p class="special-price"> <span class="price-label"></span> <span id="product-price-212" class="price"> $222.99 </span> </p>
                  </div>
                  <div class="ratings">
                    <div class="rating-box">
                      <div style="width:50%" class="rating"></div>
                    </div>
                    <p class="rating-links"> <a href="#">1 Review(s)</a> <span class="separator">|</span> <a href="#review-form">Add Your Review</a> </p>
                  </div>
                  <div class="desc std">
                    <p>Sed volutpat ac massa eget 
                      lacinia.  
                      Aenean volutpat lacus at dolor blandit </p>
                    <p>Sed sed interdum diam. Donec sit ametenim tempor, dapibus nunc eu, 
                      tincidunt mi. Vivamus dictum nec... <a class="link-learn" title="" href="#">Learn More</a> </p>
                  </div>
                  <div class="actions">
                    <button class="button btn-cart ajx-cart" title="Add to Cart" type="button"><span>Add to Cart</span></button>
                    <span class="add-to-links"> 
                      <a title="Add to Wishlist" class="button link-wishlist" href="wishlist.html"><span>Add to Wishlist</span></a> 
                      <a title="Add to Compare" class="button link-compare" href="#"><span>Add to Compare</span></a> 
                    </span> 
                  </div>
                </div>
              </li>
            </ol>
          </div></div>
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
  <!-- End Two columns content --> 

</asp:Content>