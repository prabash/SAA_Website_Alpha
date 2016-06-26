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
    public partial class MyAccountAjaxPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string AddRemoveToMyWishlist(int itemId, string itemTitle)
        {
            try
            {
                var custId = HttpContext.Current.Session[CommonManager.GetCustIdSessionName()];
                if (custId == null)
                {
                    return "Login";
                }
                else
                {
                    bool added;
                    new CustWishlistModel().AddOrRemoveToWishList(Convert.ToInt32(custId), itemId, out added);
                    if (added)
                        return "Successfully added " + itemTitle + " to your wishlist!";
                    else
                        return "Removed " + itemTitle + " from your wishlist!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string AddToCartSession(int itemId, string itemTitle)
        {
            try
            {
                var cartSession = HttpContext.Current.Session[CommonManager.GetCartItemsSessionName()];
                if (cartSession == null)
                {
                    HttpContext.Current.Session.Add(CommonManager.GetCartItemsSessionName(), new List<int>());
                    cartSession = HttpContext.Current.Session[CommonManager.GetCartItemsSessionName()];
                }

                var cart = (List<int>)cartSession;
                if (!cart.Contains(itemId))
                {
                    cart.Add(itemId);
                    HttpContext.Current.Session[CommonManager.GetCartItemsSessionName()] = cart;
                    return "Successfully added " + itemTitle + " to your cart!";
                }
                else
                    return "The item already exists in your cart!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string RemoveFromCartSession(int itemId)
        {
            try
            {
                var cartSession = HttpContext.Current.Session[CommonManager.GetCartItemsSessionName()];
                if (cartSession != null)
                {
                    var cart = (List<int>)cartSession;
                    if (cart.Contains(itemId))
                    {
                        cart.Remove(itemId);
                        HttpContext.Current.Session[CommonManager.GetCartItemsSessionName()] = cart;
                        return "Successfully removed item from your cart!";
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void ClearCart()
        {
            try
            {
                var cartSession = HttpContext.Current.Session[CommonManager.GetCartItemsSessionName()];
                if (cartSession != null)
                {
                    var cart = (List<int>)cartSession;
                    cart = new List<int>();
                    HttpContext.Current.Session[CommonManager.GetCartItemsSessionName()] = cart;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void CancelOrder(string purchId)
        {
            try
            {
                new PurchItemModel().CancelOrder(Convert.ToInt32(purchId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}