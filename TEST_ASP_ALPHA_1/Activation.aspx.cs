using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                    new CustomerManager().ActivateUser(activationCode);

                    successActMsgBox.Visible = true;
                    errorActMsgBox.Visible = false;
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