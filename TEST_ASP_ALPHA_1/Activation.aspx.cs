using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class Activation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var activationCode = Request.QueryString["ActivationCode"];
                if (!String.IsNullOrEmpty(activationCode))
                {
                    new CustomerModel().ActivateUser(activationCode);

                    successActMsgBox.Visible = true;
                    errorActMsgBox.Visible = false;
                }

                var checkout = Request.QueryString["Checkout"];
                if (!String.IsNullOrEmpty(checkout))
                {
                    var customerList = new CustomerModel().GetCustomerDetails(CustomerGetType.email, 0, activationCode);
                    if (customerList.Count > 0)
                    {
                        var customer = customerList.First();
                        Session.Add(CommonManager.GetCustIdSessionName(), customer.Id);
                        Session.Add(CommonManager.GetCustNameSessionName(), customer.username);
                        Session.Add(CommonManager.GetCustEmailSessionName(), new AESManager().DecryptString(activationCode));
                        Session.Add(CommonManager.GetCheckoutEligibleSessionName(), false);

                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                        Response.Redirect(baseUrl + "CheckoutBillingInfoUpdate.aspx");
                    }
                }
            }
            catch (Exception)
            {
                successActMsgBox.Visible = false;
                errorActMsgBox.Visible = true;
            }
        }

        #region Subscription

        protected void btnSubscribe_Click(object sender, EventArgs e)
        {
            SubscribeNewsletter();
        }

        private void SubscribeNewsletter()
        {
            try
            {
                var email = dfnEmailAdd.Value;
                new CustCommentsModel().SaveSubscription(email);
                successBox.Visible = true;
            }
            catch (Exception)
            {
                errorBox.Visible = true;
            }
        }

        #endregion
    }
}