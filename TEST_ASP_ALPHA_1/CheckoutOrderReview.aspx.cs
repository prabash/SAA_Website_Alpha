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
    public partial class CheckoutOrderReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[CommonManager.GetCustEmailSessionName()] == null)
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    Response.Redirect("Cart.aspx");
                }
            }
        }

        protected void btnCompleteCheckout_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCart();
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

                Session.Remove(CommonManager.GetCartItemsSessionName());
                Session.Remove(CommonManager.GetCartItemsWithQtySessionName());

                Response.Redirect("CheckoutComplete.aspx?CheckoutRes=Success");
            }
            catch (Exception ex)
            {
                Response.Redirect("CheckoutComplete.aspx?CheckoutRes=" + ex.Message);
            }
        }

        private void SaveCart()
        {
            var custId = Session[CommonManager.GetCustIdSessionName()];
            var custEmail = Session[CommonManager.GetCustEmailSessionName()];
            var cartSession = Session[CommonManager.GetCartItemsWithQtySessionName()];
            var cartOb = new CartObject();
            cartOb.Items = new List<PurchaseItemObject>();

            if (cartSession != null)
            {
                var cart = (CartObject)cartSession;
                if (cart != null)
                {
                    var cartIds = new List<int>();
                    foreach (var item in cart.Items)
                    {
                        cartIds.Add(item.ItemId);
                    }

                    var searchCriteria = new Dictionary<string, string>();
                    searchCriteria.Add(CommonManager.GetIdCriterionName(), CommonManager.GetSearchByIdCriterion(cartIds));
                    var itemDetails = new TEST_ASP_ALPHA_1.Models.ItemsModel().GetItemDetails(ItemType.All, ItemSortBy.NameAsc, 0, 0, searchCriteria);
                    foreach (var item in itemDetails)
                    {
                        var purchItem = new PurchaseItemObject();
                        purchItem.ItemId = item.Id;
                        purchItem.ItemQuantity = cart.Items.First(rec => rec.ItemId == item.Id).ItemQuantity;
                        purchItem.CostPerItem = item.currentPrice;
                        purchItem.ItemTitle = item.title;
                        purchItem.PurchCartId = 0;
                        purchItem.PurchDate = DateTime.Now;
                        purchItem.Status = CommonManager.Status_GetNewItemName();
                        purchItem.SubTotal = item.currentPrice * purchItem.ItemQuantity;
                        cartOb.Items.Add(purchItem);
                    }
                }
            }
            cartOb.CartDelivery = 0;
            cartOb.CartDiscount = new ItemsModel().GetItemDiscount();
            cartOb.CartTotal = cartOb.Items.Sum(rec => rec.SubTotal);
            cartOb.CustEmailAddress = new AESManager().EncryptToString(custEmail.ToString());
            cartOb.CustId = Convert.ToInt32(custId);
            cartOb.CartStatus = CommonManager.Status_GetNewItemName();
            cartOb.PurchaseDate = DateTime.Now;

            new PurchCartModel().SaveCart(cartOb);
        }
    }
}