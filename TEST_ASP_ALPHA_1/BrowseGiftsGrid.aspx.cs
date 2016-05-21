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
    public partial class BrowseGiftsGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSlideshowDetails();
                GetGridDetails();
            }
        }



        private void GetSlideshowDetails()
        {
            var slideShowDetails = new ItemsModel().GetSlideShowDetails(ItemType.Gifts);
            foreach (var item in slideShowDetails)
            {
                var itemDivControl = HTMLControlsManager.GetDivTag(new[] { "item" });
                itemSlideShow.Controls.Add(itemDivControl);
                {
                    var itemAnchor = HTMLControlsManager.GetAnchorTag("#");
                    itemDivControl.Controls.Add(itemAnchor);
                    {
                        var altImage = HTMLControlsManager.GetImageTag(item.location, null, "Games_banner");
                        itemAnchor.Controls.Add(altImage);
                    }

                    var catDivControl = HTMLControlsManager.GetDivTag(new[] { "cat-img-title", "cat-bg", "cat-box" });
                    itemDivControl.Controls.Add(catDivControl);
                    {
                        var headingControl = HTMLControlsManager.GetCustomTag("h2", new[] { "cat-heading" }, item.title);
                        catDivControl.Controls.Add(headingControl);
                    }
                }
            }
        }

        private void GetGridDetails()
        {
            var gridDetails = new ItemsModel().GetItemDetails(ItemType.Gifts, ItemSortBy.NameAsc, 18);
            foreach (var item in gridDetails)
            {
                var liControl = HTMLControlsManager.GetCustomTag("li", new[] { "item col-lg-4", "col-md-3", "col-sm-4", "col-xs-12" });
                gridItemsList.Controls.Add(liControl);
                {
                    var itemInnerDivControl = ItemHtmlManager.Get_ItemInner_Grid_DivControl(item);
                    liControl.Controls.Add(itemInnerDivControl);

                }
            }
        }
    }
}