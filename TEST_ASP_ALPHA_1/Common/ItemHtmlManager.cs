using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace TEST_ASP_ALPHA_1.Common
{
    public class ItemHtmlManager
    {
        public static HtmlGenericControl Get_ItemInner_Grid_DivControl(ItemObject item)
        {
            HtmlGenericControl itemInnerDivControl = HTMLControlsManager.GetDivTag(new[] { "item-inner" });
            {
                var productBlockDivControl = HTMLControlsManager.GetDivTag(new[] { "product-block" });
                itemInnerDivControl.Controls.Add(productBlockDivControl);
                {
                    var productImageDivControl = HTMLControlsManager.GetDivTag(new[] { "product-image" });
                    productBlockDivControl.Controls.Add(productImageDivControl);
                    {
                        var productAnchorControl = HTMLControlsManager.GetAnchorTag("ProductDetails.aspx?itemId=" + item.Id);
                        productImageDivControl.Controls.Add(productAnchorControl);
                        {
                            var productDisplayFigureControl = HTMLControlsManager.GetCustomTag("figure", new[] { "product-display" });
                            productAnchorControl.Controls.Add(productDisplayFigureControl);
                            {
                                if (item.onSale)
                                {
                                    var saleLabelDivControl = HTMLControlsManager.GetDivTag(new[] { "sale-label", "sale-top-left" }, "Sale");
                                    productDisplayFigureControl.Controls.Add(saleLabelDivControl);
                                }
                                else
                                {
                                    var newLabelDivControl = HTMLControlsManager.GetDivTag(new[] { "new-label", "new-top-left" }, "New");
                                    productDisplayFigureControl.Controls.Add(newLabelDivControl);
                                }

                                var mainImageControl = HTMLControlsManager.GetImageTag(item.defaultLocation, new[] { "lazyOwl", "nproduct-mainpic" },
                                    "product-image", "display: block;");
                                productDisplayFigureControl.Controls.Add(mainImageControl);

                                var altImageControl = HTMLControlsManager.GetImageTag(item.altLocation, new[] { "product-secondpic" },
                                    "product-image");
                                productDisplayFigureControl.Controls.Add(altImageControl);
                            }
                        }
                    }

                    var productMetaDivControl = HTMLControlsManager.GetDivTag(new[] { "product-meta" });
                    productBlockDivControl.Controls.Add(productMetaDivControl);
                    {
                        var productActionDivControl = HTMLControlsManager.GetDivTag(new[] { "product-action" });
                        productMetaDivControl.Controls.Add(productActionDivControl);
                        {
                            Dictionary<string, string> addCartAtt = new Dictionary<string, string>();
                            addCartAtt.Add("onclick", "AddToCart(" + item.Id + ",'" + item.title + "');");
                            var addCartAnchorControl = HTMLControlsManager.GetAnchorTag("javascript:void(0)", new[] { "addcart" }, "Add to cart ", null, null, addCartAtt);
                            {
                                var italicControl = HTMLControlsManager.GetCustomTag("i", new[] { "icon-shopping-cart" }, " ");
                                addCartAnchorControl.Controls.Add(italicControl);
                            }
                            productActionDivControl.Controls.Add(addCartAnchorControl);

                            Dictionary<string, string> addWishListAtt = new Dictionary<string, string>();
                            addWishListAtt.Add("onclick", "AddRemovetoWishList(" + item.Id + ",'" + item.title + "');");
                            var wishlistAnchorControl = HTMLControlsManager.GetAnchorTag("javascript:void(0)", new[] { "wishlist" }, null, null, null, addWishListAtt);
                            productActionDivControl.Controls.Add(wishlistAnchorControl);
                            {
                                var italicControl = HTMLControlsManager.GetCustomTag("i", new[] { "icon-heart" });
                                wishlistAnchorControl.Controls.Add(italicControl);
                            }

                            var quickViewAnchorControl = HTMLControlsManager.GetAnchorTag("quick_view.html", new[] { "quickview" });
                            productActionDivControl.Controls.Add(quickViewAnchorControl);
                            {
                                var italicControl = HTMLControlsManager.GetCustomTag("i", new[] { "icon-zoom" });
                                quickViewAnchorControl.Controls.Add(italicControl);
                            }
                        }
                    }
                }

                var itemInfoDivControl = HTMLControlsManager.GetDivTag(new[] { "item-info" });
                itemInnerDivControl.Controls.Add(itemInfoDivControl);
                {
                    var infoInnerDivControl = HTMLControlsManager.GetDivTag(new[] { "info-inner" });
                    itemInfoDivControl.Controls.Add(infoInnerDivControl);
                    {
                        var itemTitleDivControl = HTMLControlsManager.GetDivTag(new[] { "item-title" });
                        infoInnerDivControl.Controls.Add(itemTitleDivControl);
                        {
                            var productDetailAnchorTag = HTMLControlsManager.GetAnchorTag("ProductDetails.aspx?itemId=" + item.Id, null, item.title, item.title);
                            itemTitleDivControl.Controls.Add(productDetailAnchorTag);
                        }

                        var itemContentDivControl = HTMLControlsManager.GetDivTag(new[] { "item-content" });
                        infoInnerDivControl.Controls.Add(itemContentDivControl);
                        {
                            var itemPriceDivControl = HTMLControlsManager.GetDivTag(new[] { "item-price" });
                            itemContentDivControl.Controls.Add(itemPriceDivControl);
                            {
                                var priceBoxDivControl = HTMLControlsManager.GetDivTag(new[] { "price-box" });
                                itemPriceDivControl.Controls.Add(priceBoxDivControl);
                                {
                                    if (item.onSale)
                                    {
                                        var oldPriceParaControl = HTMLControlsManager.GetCustomTag("p", new[] { "old-price" });
                                        priceBoxDivControl.Controls.Add(oldPriceParaControl);
                                        {
                                            var priceLabelSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price-label" }, "Regular Price:");
                                            oldPriceParaControl.Controls.Add(priceLabelSpanControl);

                                            var priceSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, item.regularPrice.FormatCurrency("LKR").ToString());
                                            oldPriceParaControl.Controls.Add(priceSpanControl);
                                        }

                                        var newPriceParaControl = HTMLControlsManager.GetCustomTag("p", new[] { "special-price" });
                                        priceBoxDivControl.Controls.Add(newPriceParaControl);
                                        {
                                            var priceLabelSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price-label" }, "Regular Price:");
                                            newPriceParaControl.Controls.Add(priceLabelSpanControl);

                                            var priceSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, item.currentPrice.FormatCurrency("LKR").ToString());
                                            newPriceParaControl.Controls.Add(priceSpanControl);
                                        }
                                    }
                                    else
                                    {
                                        var regularPriceSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "regular-price" });
                                        priceBoxDivControl.Controls.Add(regularPriceSpanControl);
                                        {
                                            var priceSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, item.currentPrice.FormatCurrency("LKR").ToString());
                                            regularPriceSpanControl.Controls.Add(priceSpanControl);
                                        }
                                    }
                                }
                            }

                            var ratingDivControl = HTMLControlsManager.GetDivTag(new[] { "rating" });
                            itemContentDivControl.Controls.Add(ratingDivControl);
                            {
                                var ratingsDivControl = HTMLControlsManager.GetDivTag(new[] { "ratings" });
                                ratingDivControl.Controls.Add(ratingsDivControl);
                                {
                                    var ratingBoxDivControl = HTMLControlsManager.GetDivTag(new[] { "rating-box" });
                                    ratingsDivControl.Controls.Add(ratingBoxDivControl);
                                    {
                                        var currentRatingDivControl = HTMLControlsManager.GetDivTag(new[] { "rating" }, "", "width:" + item.rating + "%");
                                        ratingBoxDivControl.Controls.Add(currentRatingDivControl);
                                    }

                                    var ratingLinksParaControl = HTMLControlsManager.GetCustomTag("p", new[] { "rating-links" });
                                    ratingsDivControl.Controls.Add(ratingLinksParaControl);
                                    {
                                        var reviewAnchorControl = HTMLControlsManager.GetAnchorTag("#", null, "1 Review(s)");
                                        ratingLinksParaControl.Controls.Add(reviewAnchorControl);

                                        var seperatorSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "separator" }, "|");
                                        ratingLinksParaControl.Controls.Add(seperatorSpanControl);

                                        var addReviewAnchorControl = HTMLControlsManager.GetAnchorTag("#", null, "Add Review");
                                        ratingLinksParaControl.Controls.Add(addReviewAnchorControl);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return itemInnerDivControl;
        }

        public static HtmlGenericControl Get_ProductImage_List_DivControl(ItemObject item)
        {
            HtmlGenericControl productImageDivControl = HTMLControlsManager.GetDivTag(new[] { "product-image" });
            {
                var productAnchorControl = HTMLControlsManager.GetAnchorTag("ProductDetails.aspx?itemId=" + item.Id, null, null, item.title);
                productImageDivControl.Controls.Add(productAnchorControl);
                {
                    var mainImageControl = HTMLControlsManager.GetImageTag(item.defaultLocation, new[] { "small-image" },
                                    "product-image");
                    mainImageControl.Width = 230;
                    productAnchorControl.Controls.Add(mainImageControl);
                }
            }

            return productImageDivControl;
        }

        public static HtmlGenericControl Get_ProductDetails_List_DivControl(ItemObject item)
        {
            HtmlGenericControl productDetailDivControl = HTMLControlsManager.GetDivTag(new[] { "product-shop" });
            {
                var productNameH2Control = HTMLControlsManager.GetCustomTag("h2", new[] { "product-name" });
                productDetailDivControl.Controls.Add(productNameH2Control);
                {
                    var mainImageControl = HTMLControlsManager.GetAnchorTag("ProductDetails.aspx?itemId=" + item.Id, null, item.title);
                    productNameH2Control.Controls.Add(mainImageControl);
                }

                var priceBoxDivControl = HTMLControlsManager.GetDivTag(new[] {"price-box"});
                productDetailDivControl.Controls.Add(priceBoxDivControl);
                {
                    if (item.onSale)
                    {
                        var oldPriceParaControl = HTMLControlsManager.GetCustomTag("p", new[] { "old-price" });
                        priceBoxDivControl.Controls.Add(oldPriceParaControl);
                        {
                            var priceLabelSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price-label" }, "Regular Price:");
                            oldPriceParaControl.Controls.Add(priceLabelSpanControl);

                            var priceSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, item.regularPrice.FormatCurrency("LKR").ToString());
                            oldPriceParaControl.Controls.Add(priceSpanControl);
                        }

                        var newPriceParaControl = HTMLControlsManager.GetCustomTag("p", new[] { "special-price" });
                        priceBoxDivControl.Controls.Add(newPriceParaControl);
                        {
                            var priceLabelSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price-label" }, "Regular Price:");
                            newPriceParaControl.Controls.Add(priceLabelSpanControl);

                            var priceSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, item.currentPrice.FormatCurrency("LKR").ToString());
                            newPriceParaControl.Controls.Add(priceSpanControl);
                        }
                    }
                    else
                    {
                        var regularPriceSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "regular-price" });
                        priceBoxDivControl.Controls.Add(regularPriceSpanControl);
                        {
                            var priceSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, item.currentPrice.FormatCurrency("LKR").ToString());
                            regularPriceSpanControl.Controls.Add(priceSpanControl);
                        }
                    }
                }

                var ratingsDivControl = HTMLControlsManager.GetDivTag(new[] { "ratings" });
                productDetailDivControl.Controls.Add(ratingsDivControl);
                {
                    var ratingBoxDivControl = HTMLControlsManager.GetDivTag(new[] { "rating-box" });
                    ratingsDivControl.Controls.Add(ratingBoxDivControl);
                    {
                        var currentRatingDivControl = HTMLControlsManager.GetDivTag(new[] { "rating" }, "", "width:" + item.rating + "%");
                        ratingBoxDivControl.Controls.Add(currentRatingDivControl);
                    }

                    var ratingLinksParaControl = HTMLControlsManager.GetCustomTag("p", new[] { "rating-links" });
                    ratingsDivControl.Controls.Add(ratingLinksParaControl);
                    {
                        var reviewAnchorControl = HTMLControlsManager.GetAnchorTag("#", null, "1 Review(s)");
                        ratingLinksParaControl.Controls.Add(reviewAnchorControl);

                        var seperatorSpanControl = HTMLControlsManager.GetCustomTag("span", new[] { "separator" }, "|");
                        ratingLinksParaControl.Controls.Add(seperatorSpanControl);

                        var addReviewAnchorControl = HTMLControlsManager.GetAnchorTag("#", null, "Add Review");
                        ratingLinksParaControl.Controls.Add(addReviewAnchorControl);
                    }
                }

                var type = EnumsManager.GetItemType(item.type);
                
                var descriptionDivControl = HTMLControlsManager.GetDivTag(new[] { "desc std" });
                productDetailDivControl.Controls.Add(descriptionDivControl);
                {
                    var paraDescControl = HTMLControlsManager.GetCustomTag("p", null);
                    if (type == ItemType.Games || type == ItemType.Movies || type == ItemType.TvSeries)
                    {
                        paraDescControl.InnerHtml = "<b>Year</b> : " + item.year + "<br/>" + "<b>Genre</b> : " + item.genre + "<br/>" + item.description;
                    }
                    else
                    {
                        paraDescControl.InnerHtml = item.description;
                    }
                    descriptionDivControl.Controls.Add(paraDescControl);
                }
                

                var actionsDivControl = HTMLControlsManager.GetDivTag(new[] { "actions" });
                productDetailDivControl.Controls.Add(actionsDivControl);
                {
                    var buttonAddtoCart = HTMLControlsManager.GetCustomTag("button", new[] { "button", "btn-cart", "ajx-cart" }, null, null, null, "Add to Cart", "button");
                    actionsDivControl.Controls.Add(buttonAddtoCart);
                    {
                        Dictionary<string, string> addCartAtt = new Dictionary<string, string>();
                        addCartAtt.Add("onclick", "AddToCart(" + item.Id + ",'" + item.title + "');");
                        var spanAddtoCart = HTMLControlsManager.GetCustomTag("span", null, "Add to Cart", null, null, null, null, addCartAtt);
                        buttonAddtoCart.Controls.Add(spanAddtoCart);
                    }

                    var spanAddtoWishlistLinks = HTMLControlsManager.GetCustomTag("span", new[] { "add-to-links" });
                    actionsDivControl.Controls.Add(spanAddtoWishlistLinks);
                    {
                        Dictionary<string, string> addWishlistAtt = new Dictionary<string, string>();
                        addWishlistAtt.Add("onclick", "AddRemovetoWishList(" + item.Id + ",'" + item.title + "');");
                        var anchorAddToWish = HTMLControlsManager.GetAnchorTag("javascript:void(0)", new[] { "button", "link-wishlist" }, "", "Add to Wishlist", null, addWishlistAtt);
                        spanAddtoWishlistLinks.Controls.Add(anchorAddToWish);
                        {
                            var spanAddtoWishlist = HTMLControlsManager.GetCustomTag("span", null, "Add to Wishlist");
                            anchorAddToWish.Controls.Add(spanAddtoWishlist);
                        }
                    }
                }
            }

            return productDetailDivControl;
        }
    }
}