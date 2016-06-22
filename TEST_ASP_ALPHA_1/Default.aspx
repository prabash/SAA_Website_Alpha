<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TEST_ASP_ALPHA_1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <!-- Slider -->
<div class="page" style="margin-left:-15px">
     <div class="container">
      <div class="slider-section">
        <div class="slider-intro">
            <div class="the-slideshow slideshow-wrapper">
                <ul class="slideshow" style="overflow: hidden;" runat="server" id="slideShowList"> </ul>
            <a id="home-slides-prev" class="backward browse-button" href="#">previous</a> <a id="home-slides-next" class="forward browse-button" href="#">next</a>
            <div id="home-slides-pager" class="tab-pager tab-pager-img tab-pager-ring-lgray">&nbsp;</div>
            </div>
            </div>           <div class="cat-block pro-block">
            <div class="pro1-block">
            <ul class="top-cat-box">
            <li>
            <div><img src="images/games-img.png" alt="games image" /></div>
            <h2>Games</h2>
            <a href="BrowseGamesGrid.aspx">Buy Now</a></li>
            <li>
            <div><img src="images/movies-img.png" alt="movies image" /></div>
            <h2>Movies</h2>
            <a href="BrowseMoviesGrid.aspx">Buy Now</a></li>
            <li>
            <div><img src="images/tvseries-img.png" alt="Electronics image" /></div>
            <h2>TV Series</h2>
            <a href="BrowseTvSeriesGrid.aspx">Buy Now</a></li>
            <li>
            <div><img src="images/gift-img.png" alt="gifts image" /></div>
            <h2>Gifts</h2>
            <a href="BrowseGiftsGrid.aspx">Buy Now</a></li>
            </ul>
            </div>
            </div>
        </div>
    </div>
<!-- end Slider -->
  <div class="service-section">
    <div class="container">
      <div class="row">
        <div id="store-messages">
          <div class="col-md-4 col-xs-12 col-sm-4"><i class="icon-refresh">&nbsp;</i>
            <div class="message"> <span><strong>Return &amp; Exchange</strong> in 3 working days </span>Lorem ipsum dolor sit amet, consectetur adipiscing. </div>
          </div>
          <div class="col-md-4 col-xs-12 col-sm-4"><i class="icon-truck">&nbsp;</i>
            <div class="message"><span><strong>Free Shipping</strong> order over $99</span>Lorem ipsum dolor sit amet, consectetur adipiscing. </div>
          </div>
          <div class="col-md-4 col-xs-12 col-sm-4"><i class="icon-discount">&nbsp;</i>
            <div class="message"><span><strong>50% Off</strong> on all products</span>Lorem ipsum dolor sit amet, consectetur adipiscing. </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- main container -->
  <section class="main-container col1-layout home-content-container">
    <div class="container">
      <div class="std">
        <div class="best-seller-pro wow bounceInUp animated">
          <div class="slider-items-products">
            <div class="new_title center">
              <h2>Best Sellers</h2>
            </div>
              <div id="best-seller-slider" class="product-flexslider hidden-buttons">
                  <div class="slider-items slider-width-col4" id="bestSellersList" runat="server"></div>
              </div>
          </div>
        </div>
        <!-- Featured Slider -->
        <div class="featured-pro">
          <div class="slider-items-products">
            <div class="title col-lg-6">
              <div class="new_title center">
                <h2><span>Gift Items</span></h2>
              </div>
              <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec pharetra tristique eros dignissim venenatis. Nam et dolor id arcu efficitur tincidunt quis non nisi. Duis commodo pellentesque quam, id mollis ligula dictum id. Morbi cursus consectetur magna a egestas. Sed eu sagittis leo. Mauris euismod dapibus nisl, at euismod nunc commodo vel. Nulla semper non nisi in placerat. Aenean at nisl eu odio ullamcorper varius non ac ante. Quisque dictum, felis lobortis faucibus ornare, enim mi mollis ex, ac vestibulum neque nibh at nisi. </p>
              <a title="Subscribe" class="view-all" href="#"><span>View All Products</span></a> </div>
            <div id="featured-slider" class="product-flexslider hidden-buttons col-lg-6">
              <div class="slider-items slider-width-col4" id="giftItemsList" runat="server">
              </div>
            </div>
          </div>
        </div>
        <!-- End Featured Slider -->
      </div>
    </div>
  </section>
  <!-- End main container -->
</div>
</asp:Content>
