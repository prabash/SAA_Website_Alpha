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
    public partial class CheckoutMethod : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        CustomerModel loginMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            loginMgr = new CustomerModel();
            if (Session[CommonManager.GetCustEmailSessionName()] != null)
            {
                if (Session[CommonManager.GetCheckoutEligibleSessionName()] != null)
                {
                    var checkoutEligible = (bool)Session[CommonManager.GetCheckoutEligibleSessionName()];
                    if(!checkoutEligible)
                        Response.Redirect("CheckoutBillingInfo.aspx");
                    else
                        Response.Redirect("CheckoutPaymentInfo.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var encEmail = aesMgr.EncryptToString(dfnLoginEmail.Text);
                var encPassword = aesMgr.EncryptToString(dfnLoginPass.Text);
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

                int custId;
                string custUsername;
                bool checkoutEligible;

                if (loginMgr.ValidateLogin(encEmail, encPassword, out custId, out custUsername, out checkoutEligible))
                {
                    regErrorBox.Visible = false;

                    Session.Add(CommonManager.GetCustIdSessionName(), custId);
                    Session.Add(CommonManager.GetCustNameSessionName(), custUsername);
                    Session.Add(CommonManager.GetCustEmailSessionName(), dfnLoginEmail.Text);
                    Session.Add(CommonManager.GetCheckoutEligibleSessionName(), checkoutEligible);

                    if (!checkoutEligible)
                        Response.Redirect("CheckoutBillingInfoUpdate.aspx");
                    else
                        Response.Redirect(baseUrl + "CheckoutPaymentInfo.aspx");
                }
            }
            catch (Exception ex)
            {
                regErrMsg.InnerText = ex.Message;
                regErrorBox.Visible = true;
            }
        }
    }
}