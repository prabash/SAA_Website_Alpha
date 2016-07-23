<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="TEST_ASP_ALPHA_1.ProductDetails" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- start breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a href="index.html" title="Go to Home Page">Home</a><span>&raquo;</span></li>
                    <li class=""><a href="grid.html" title="Go to Home Page" runat="server" id="productTypeBreadcrumb"></a><span>&raquo;</span></li>
                    <li class="category13"><strong runat="server" id="productTitleBreadcrumb"></strong></li>
                </ul>
            </div>
        </div>
    </div>
    <!-- end breadcrumbs -->
    <!-- main-container -->
    <section class="main-container col1-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="col-main">
                <div class="row">
                    <div class="product-view">
                        <div class="product-essential">
                            <form action="#" method="post" id="product_addtocart_form">
                                <input name="form_key" value="6UbXroakyQlbfQzK" type="hidden">
                                <div class="product-img-box col-sm-4 col-xs-12">
                                    <div runat="server" id="divItemStatus">New </div>
                                    <div class="product-image">
                                        <div class="large-image"><a href="products_images/product1.jpg" class="cloud-zoom" id="zoom1" rel="useWrapper: false, adjustY:0, adjustX:20" runat="server">
                                            <img alt="product-image" src="products_images/product1.jpg" runat="server" id="largeImage">
                                        </a></div>
                                        <div class="flexslider flexslider-thumb" style="visibility: collapse; height: 0px;">
                                            <ul class="previews-list slides">
                                                <li><a href='products_images/product1.jpg' class='cloud-zoom-gallery' rel="useZoom: 'zoom1', smallImage: 'products_images/product1.jpg' ">
                                                    <img src="products_images/product1.jpg" alt="Thumbnail 1" /></a></li>
                                                <li><a href='products_images/product1.jpg' class='cloud-zoom-gallery' rel="useZoom: 'zoom1', smallImage: 'products_images/product1.jpg' ">
                                                    <img src="products_images/product1.jpg" alt="Thumbnail 2" /></a></li>
                                                <li><a href='products_images/product1.jpg' class='cloud-zoom-gallery' rel="useZoom: 'zoom1', smallImage: 'products_images/product1.jpg' ">
                                                    <img src="products_images/product1.jpg" alt="Thumbnail 1" /></a></li>
                                                <li><a href='products_images/product1.jpg' class='cloud-zoom-gallery' rel="useZoom: 'zoom1', smallImage: 'products_images/product1.jpg' ">
                                                    <img src="products_images/product1.jpg" alt="Thumbnail 2" /></a></li>
                                                <li><a href='products_images/product1.jpg' class='cloud-zoom-gallery' rel="useZoom: 'zoom1', smallImage: 'products_images/product1.jpg' ">
                                                    <img src="products_images/product1.jpg" alt="Thumbnail 2" /></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <!-- end: more-images -->
                                    <div class="clear"></div>
                                </div>
                                <div class="product-shop col-sm-8 col-xs-12">
                                    <div class="product-next-prev"><a href="#" class="product-next"><span></span></a><a href="#" class="product-prev"><span></span></a></div>
                                    <div class="product-name">
                                        <h1 runat="server" id="productTitle">Sample Product</h1>
                                    </div>
                                    <div class="ratings">
                                        <div class="rating-box">
                                            <div style="width: 60%" class="rating" id="ratingBox" runat="server"></div>
                                        </div>
                                        <p class="rating-links"><a href="#">1 Review(s)</a> <span class="separator">|</span> <a href="#">Add Your Review</a> </p>
                                    </div>
                                    <div class="short-description">
                                        <h2>Quick Overview</h2>
                                        <p runat="server" id="overviewPara"></p>
                                    </div>
                                    <p class="availability in-stock">Availability: <span>In Stock</span></p>
                                    <div class="price-block">
                                        <div class="price-box">
                                            <p class="old-price" runat="server" id="regularPriceBox">
                                                <span class="price-label" runat="server" id="regularPriceLabel">Regular Price:</span>
                                            </p>
                                            <p class="special-price" runat="server" id="specialPriceBox">
                                                <span class="price-label" runat="server" id="specialPriceLabel">Special Price</span>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="add-to-box">
                                        <div class="add-to-cart">
                                            <label for="qty">Quantity:</label>
                                            <div class="pull-left">
                                                <div class="custom pull-left">
                                                    <button onclick="var result = document.getElementById('qty'); var qty = result.value; if( !isNaN( qty ) &amp;&amp; qty &gt; 0 ) result.value--;return false;" class="reduced items-count" type="button"><i class="icon-minus">&nbsp;</i></button>
                                                    <input type="text" class="input-text qty" title="Qty" value="1" maxlength="12" id="qty" name="qty">
                                                    <button onclick="var result = document.getElementById('qty'); var qty = result.value; if( !isNaN( qty )) result.value++;return false;" class="increase items-count" type="button"><i class="icon-plus">&nbsp;</i></button>
                                                </div>
                                            </div>
                                            <button runat="server" id="btnAddToCart" class="button btn-cart" title="Add to Cart" type="button"><span><i class="icon-basket"></i>Add to Cart</span></button>
                                        </div>
                                        <div class="email-addto-box">
                                            <ul class="add-to-links">
                                                <li><a class="link-wishlist" href="javascript:void(0);" runat="server" id="addToWishlist"><span>Add/Remove from Wishlist</span></a></li>
                                                <li style="visibility: collapse; width: 0px;"><span class="separator">|</span> <a class="link-compare" href="compare.html"><span></span></a></li>
                                            </ul>
                                            <p style="margin-left: -30px;" class="email-friend"><a href="#" class=""><span>Email to a Friend</span></a></p>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="product-collateral">
                            <ul id="product-detail-tab" class="nav nav-tabs product-tabs">
                                <li class="active"><a href="#product_tabs_description" data-toggle="tab">Product Description </a></li>
                                <li style="visibility: hidden"><a href="#product_tabs_tags" data-toggle="tab">Tags</a></li>
                                <li style="visibility: hidden"><a href="#reviews_tabs" data-toggle="tab">Reviews</a> </li>
                            </ul>
                            <div id="productTabContent" class="tab-content">
                                <div class="tab-pane fade in active" id="product_tabs_description">
                                    <div class="std">
                                        <p runat="server" id="productDescription"></p>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="product_tabs_tags" style="visibility: hidden">
                                    <div class="box-collateral box-tags">
                                        <div class="tags">
                                            <form id="addTagForm" action="#" method="get">
                                                <div class="form-add-tags">
                                                    <label for="productTagName">Add Tags:</label>
                                                    <div class="input-box">
                                                        <input class="input-text required-entry" name="productTagName" id="productTagName" type="text">
                                                        <button type="button" title="Add Tags" class=" button btn-add" onclick="submitTagForm()"><span>Add Tags</span> </button>
                                                    </div>
                                                    <!--input-box-->
                                                </div>
                                            </form>
                                        </div>
                                        <!--tags-->
                                        <p class="note">Use spaces to separate tags. Use single quotes (') for phrases.</p>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="reviews_tabs" style="visibility: hidden">
                                    <div class="box-collateral box-reviews" id="customer-reviews">
                                        <div class="box-reviews1">
                                            <div class="form-add">
                                                <form id="review-form" method="post" action="#">
                                                    <h3>Write Your Own Review</h3>
                                                    <fieldset>
                                                        <h4>How do you rate this product? <em class="required">*</em></h4>
                                                        <span id="input-message-box"></span>
                                                        <table id="product-review-table" class="data-table">
                                                            <thead>
                                                                <tr class="first last">
                                                                    <th>&nbsp;</th>
                                                                    <th><span class="nobr">1 *</span></th>
                                                                    <th><span class="nobr">2 *</span></th>
                                                                    <th><span class="nobr">3 *</span></th>
                                                                    <th><span class="nobr">4 *</span></th>
                                                                    <th><span class="nobr">5 *</span></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr class="first odd">
                                                                    <th>Price</th>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="11" id="Price_1" name="ratings[3]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="12" id="Price_2" name="ratings[3]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="13" id="Price_3" name="ratings[3]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="14" id="Price_4" name="ratings[3]"></td>
                                                                    <td class="value last">
                                                                        <input type="radio" class="radio" value="15" id="Price_5" name="ratings[3]"></td>
                                                                </tr>
                                                                <tr class="even">
                                                                    <th>Value</th>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="6" id="Value_1" name="ratings[2]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="7" id="Value_2" name="ratings[2]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="8" id="Value_3" name="ratings[2]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="9" id="Value_4" name="ratings[2]"></td>
                                                                    <td class="value last">
                                                                        <input type="radio" class="radio" value="10" id="Value_5" name="ratings[2]"></td>
                                                                </tr>
                                                                <tr class="last odd">
                                                                    <th>Quality</th>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="1" id="Quality_1" name="ratings[1]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="2" id="Quality_2" name="ratings[1]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="3" id="Quality_3" name="ratings[1]"></td>
                                                                    <td class="value">
                                                                        <input type="radio" class="radio" value="4" id="Quality_4" name="ratings[1]"></td>
                                                                    <td class="value last">
                                                                        <input type="radio" class="radio" value="5" id="Quality_5" name="ratings[1]"></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <input type="hidden" value="" class="validate-rating" name="validate_rating">
                                                        <div class="review1">
                                                            <ul class="form-list">
                                                                <li>
                                                                    <label class="required" for="nickname_field">Nickname<em>*</em></label>
                                                                    <div class="input-box">
                                                                        <input type="text" class="input-text required-entry" id="nickname_field" name="nickname">
                                                                    </div>
                                                                </li>
                                                                <li>
                                                                    <label class="required" for="summary_field">Summary<em>*</em></label>
                                                                    <div class="input-box">
                                                                        <input type="text" class="input-text required-entry" id="summary_field" name="title">
                                                                    </div>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="review2">
                                                            <ul>
                                                                <li>
                                                                    <label class="label-wide" for="review_field">Review<em>*</em></label>
                                                                    <div class="input-box">
                                                                        <textarea class="required-entry" rows="3" cols="5" id="review_field" name="detail"></textarea>
                                                                    </div>
                                                                </li>
                                                            </ul>
                                                            <div class="buttons-set">
                                                                <button class="button submit" title="Submit Review" type="submit"><span>Submit Review</span></button>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </form>
                                            </div>
                                        </div>
                                        <div class="box-reviews2">
                                            <h3>Customer Reviews</h3>
                                            <div class="box visible">
                                                <ul>
                                                    <li>
                                                        <table class="ratings-table">
                                                            <tbody>
                                                                <tr>
                                                                    <th>Value</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 100%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Quality</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 100%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Price</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 100%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <div class="review">
                                                            <h6><a href="#/catalog/product/view/id/61/">Excellent</a></h6>
                                                            <small>Review by <span>Leslie Prichard </span>on 1/3/2014 </small>
                                                            <div class="review-txt">I have purchased shirts from Minimalism a few times and am never disappointed. The quality is excellent and the shipping is amazing. It seems like it's at your front door the minute you get off your pc. I have received my purchases within two days - amazing.</div>
                                                        </div>
                                                    </li>
                                                    <li class="even">
                                                        <table class="ratings-table">
                                                            <tbody>
                                                                <tr>
                                                                    <th>Value</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 100%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Quality</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 100%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Price</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 80%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <div class="review">
                                                            <h6><a href="#/catalog/product/view/id/60/">Amazing</a></h6>
                                                            <small>Review by <span>Sandra Parker</span>on 1/3/2014 </small>
                                                            <div class="review-txt">Minimalism is the online ! </div>
                                                        </div>
                                                    </li>
                                                    <li>
                                                        <table class="ratings-table">
                                                            <tbody>
                                                                <tr>
                                                                    <th>Value</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 100%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Quality</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 100%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Price</th>
                                                                    <td>
                                                                        <div class="rating-box">
                                                                            <div class="rating" style="width: 80%;"></div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <div class="review">
                                                            <h6><a href="#/catalog/product/view/id/59/">Nicely</a></h6>
                                                            <small>Review by <span>Anthony  Lewis</span>on 1/3/2014 </small>
                                                            <div class="review-txt">Unbeatable service and selection. This store has the best business model I have seen on the net. They are true to their word, and go the extra mile for their customers. I felt like a purchasing partner more than a customer. You have a lifetime client in me. </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="actions"><a class="button view-all" id="revies-button"><span><span>View all</span></span></a> </div>
                                        </div>
                                        <div class="clear"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="box-additional">
                                <div class="related-pro wow bounceInUp animated">
                                    <div class="slider-items-products">
                                        <div class="new_title center">
                                            <h2>Related Products</h2>
                                        </div>
                                        <div id="related-products-slider" class="product-flexslider hidden-buttons">
                                            <div class="slider-items slider-width-col4" id="relatedItemsGridList" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--End main-container -->
</asp:Content>
