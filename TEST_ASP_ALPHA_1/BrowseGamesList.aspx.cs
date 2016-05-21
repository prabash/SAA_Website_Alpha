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
    public partial class BrowseGamesList : System.Web.UI.Page
    {
        public int count = 2;
        public string sortBy = "Name";
        public bool ascending = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSlideshowDetails();

                var _count = Request.QueryString["count"];
                if (_count != null)
                {
                    count = Convert.ToInt32(_count);
                }

                var _sortBy = Request.QueryString["sortby"];
                if (_sortBy != null)
                {
                    sortBy = _sortBy;
                }

                var _asc = Request.QueryString["asc"];
                if (_asc != null)
                {
                    ascending = Convert.ToBoolean(_asc);
                }

                if (ascending)
                {
                    anchorAsc.Attributes["style"] = "color:#33cc99;font-size:11px;";
                    anchorDesc.Attributes["style"] = "color:#999;font-size:11px;";
                }
                else
                {
                    anchorAsc.Attributes["style"] = "color:#999;font-size:11px;";
                    anchorDesc.Attributes["style"] = "color:#33cc99;font-size:11px;";
                }

                AddViewPerPageList();
                AddSortByList();
                GetListDetails();
            }
        }

        private void GetSlideshowDetails()
        {
            var slideShowDetails = new ItemsModel().GetSlideShowDetails(ItemType.Games);
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

        private void GetListDetails()
        {
            var gridDetails = new ItemsModel().GetItemDetails(ItemType.Games, EnumsManager.GetSortByOption(sortBy, ascending), count);
            foreach (var item in gridDetails)
            {
                var liControl = HTMLControlsManager.GetCustomTag("li", new[] { "item odd" });
                productsList.Controls.Add(liControl);
                {
                    var prodImageDivControl = ItemHtmlManager.Get_ProductImage_List_DivControl(item);
                    liControl.Controls.Add(prodImageDivControl);

                    var prodDetailsDivControl = ItemHtmlManager.Get_ProductDetails_List_DivControl(item);
                    liControl.Controls.Add(prodDetailsDivControl);
                }
            }
        }

        private void AddViewPerPageList()
        {
            viewPerPage.Controls.Add(CommonHtmlManager.GetItemsPerPageList(CommonManager.GetValuesPerPage(), count));
        }

        private void AddSortByList()
        {
            sortByList.Controls.Add(CommonHtmlManager.GetSortValuesList(CommonManager.GetSortByOptions(), sortBy));
        }
    }
}