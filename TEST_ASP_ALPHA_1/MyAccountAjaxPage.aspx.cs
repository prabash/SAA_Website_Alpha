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
    }
}