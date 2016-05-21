using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using TEST_ASP_ALPHA_1.Models;
using System.Web.UI.HtmlControls;
using TEST_ASP_ALPHA_1.Common;
using System.Globalization;

namespace TEST_ASP_ALPHA_1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSlideshowDetails();
                GetBestSellersDetails();
                GetGiftItemsDeatils();
            }
        }

        private void CheckMySqlConnection()
        {
            //connectionString
            string connectionString = @"Data Source=localhost; Database=articles; User ID=root; Password=p@ssw0rd";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                Response.Write("MySQL Connection Successful!");
            }
        }

        private void GetSlideshowDetails()
        {   
            var slideShowDetails = new IndexModel().GetSlideShowDetails();
            foreach (var item in slideShowDetails)
            {
                HtmlGenericControl liControl = new HtmlGenericControl();
                liControl.Attributes["class"] = "slide";
                liControl.TagName = "li";
                slideShowList.Controls.Add(liControl);

                HtmlGenericControl paraControl = new HtmlGenericControl();
                paraControl.TagName = "p";
                liControl.Controls.Add(paraControl);
                HtmlAnchor anchorControl = new HtmlAnchor();
                anchorControl.HRef = "#";
                paraControl.Controls.Add(anchorControl);
                HtmlImage imageControl = new HtmlImage();
                imageControl.Src = item.location;
                anchorControl.Controls.Add(imageControl);

                HtmlGenericControl divControl = new HtmlGenericControl();
                divControl.TagName = "div";
                divControl.Attributes["class"] = "caption light1 top-right";
                liControl.Controls.Add(divControl);
                HtmlGenericControl innerDivControl = new HtmlGenericControl();
                innerDivControl.TagName = "div";
                innerDivControl.Attributes["class"] = "caption-inner";
                divControl.Controls.Add(innerDivControl);
                HtmlGenericControl titleParaControl = new HtmlGenericControl();
                titleParaControl.TagName = "p";
                titleParaControl.Attributes["class"] = "heading";
                titleParaControl.InnerText = item.title;
                innerDivControl.Controls.Add(titleParaControl);
            }
        }

        private void GetBestSellersDetails()
        {
            var bestSellersDetails = new IndexModel().GetBestSellersDetails();
            SetIndexSliderItems(bestSellersDetails, bestSellersList);
        }

        private void GetGiftItemsDeatils()
        {
            var giftItemDetails = new IndexModel().GetGiftItemsDetails();
            SetIndexSliderItems(giftItemDetails, giftItemsList);
        }

        private void SetIndexSliderItems(List<ItemObject> sliderItems, HtmlGenericControl sliderList)
        {
            foreach (var item in sliderItems)
            {
                var itemDivControl = HTMLControlsManager.GetDivTag(new[] { "item" });
                sliderList.Controls.Add(itemDivControl);
                {
                    var itemInnerDivControl = ItemHtmlManager.Get_ItemInner_Grid_DivControl(item);
                    itemDivControl.Controls.Add(itemInnerDivControl);
                    
                }
            }
        }
    }
}