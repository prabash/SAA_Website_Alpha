using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = Request.QueryString["itemId"];
                if (!String.IsNullOrEmpty(id))
                {
                    var itemDetails = new ItemsModel().GetItemDetailById(Convert.ToInt32(id));
                    ItemType currentItemType = EnumsManager.GetItemType(itemDetails.type);
                    productTypeBreadcrumb.InnerText = itemDetails.type;
                    switch (currentItemType)
                    {
                        case ItemType.Games:
                            productTypeBreadcrumb.HRef = "BrowseGamesGrid.aspx"; break;
                        case ItemType.TvSeries:
                            productTypeBreadcrumb.HRef = "BrowseTvSeriesGrid.aspx";break;
                        case ItemType.Movies:
                            productTypeBreadcrumb.HRef = "BrowseMoviesGrid.aspx";break;
                        case ItemType.Gifts:
                            productTypeBreadcrumb.HRef = "BrowseGiftsGrid.aspx";break;
                        case ItemType.Electronics:
                            productTypeBreadcrumb.HRef = "BrowseElectronicsGrid.aspx";break;
                        default:
                            break;
                    }

                    productTitle.InnerText = productTitleBreadcrumb.InnerText = itemDetails.title;
                    addToWishlist.Attributes.Add("onclick", "AddRemovetoWishList(" + itemDetails.Id + ",'" + itemDetails.title + "');");

                    largeImage.Src = zoom1.HRef = itemDetails.defaultLocation;
                    ratingBox.Attributes["style"] = "width:" + itemDetails.rating + "%";
                    overviewPara.InnerHtml = "<b>Year</b> : " + itemDetails.year + "<br/>" + "<b>Genre</b> : " + itemDetails.genre + "<br/>" + itemDetails.overview;

                    if (itemDetails.onSale)
                    {
                        var regularPriceSpan = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, itemDetails.regularPrice.FormatCurrency("LKR").ToString(), null, "old-price-48");
                        regularPriceBox.Controls.Add(regularPriceSpan);

                        var specialPriceSpan = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, itemDetails.currentPrice.FormatCurrency("LKR").ToString(), null, "product-price-48");
                        specialPriceBox.Controls.Add(specialPriceSpan);
                    }
                    else
                    {
                        regularPriceBox.Visible = false;

                        specialPriceLabel.InnerText = "Price:";
                        var specialPriceSpan = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, itemDetails.currentPrice.FormatCurrency("LKR").ToString(), null, "product-price-48");
                        specialPriceBox.Controls.Add(specialPriceSpan);
                    }

                    productDescription.InnerText = itemDetails.description;
                    btnAddToCart.Attributes["onclick"] = "AddToCart(" + itemDetails.Id + ",'" + itemDetails.title + "');";

                    if (itemDetails.onSale)
                    {
                        divItemStatus.Attributes["class"] = "sale-label sale-top-left";
                        divItemStatus.InnerText = "Sale";
                    }
                    else
                    {
                        divItemStatus.Attributes["class"] = "new-label new-top-left";
                        divItemStatus.InnerText = "New";
                    }

                    GetRelatedItemsGridDetails(Convert.ToInt32(id), currentItemType);
                }

            }
        }

        private void GetRelatedItemsGridDetails(int itemId, ItemType currentItemType)
        {
            var gridDetails = new ItemsModel().GetRelatedItems(currentItemType, new List<int> { itemId });
            foreach (var item in gridDetails)
            {
                var itemDivControl = HTMLControlsManager.GetDivTag(new[] { "item" });
                relatedItemsGridList.Controls.Add(itemDivControl);
                {
                    var itemInnerDivControl = ItemHtmlManager.Get_ItemInner_Grid_DivControl(item);
                    itemDivControl.Controls.Add(itemInnerDivControl);
                }
            }
        }

    }
}