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
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetRelatedItemsGridDetails(new List<int> { 1, 2, 5 }, ItemType.All);
        }

        private void GetRelatedItemsGridDetails(List<int> itemIds, ItemType currentItemType)
        {
            var gridDetails = new ItemsModel().GetRelatedItems(currentItemType, itemIds);
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

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string CalcItemSubQty(int itemQty, double itemPrice)
        {
            try
            {
                return FormatCurrency(itemQty * itemPrice);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string FormatCurrency(double price)
        {
            try
            {
                return price.FormatCurrency("LKR").ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}