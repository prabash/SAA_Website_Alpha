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
    public partial class ResetPassword : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        CustomerModel loginMgr;
        string resetEmail = "";
        DateTime reqDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            loginMgr = new CustomerModel();

            var resetReq = Request.QueryString["RE"];
            if (!String.IsNullOrEmpty(resetReq))
            {
                resetEmail = emailMgr.GetCustEmailFromEmailingCode(resetReq, out reqDate);
                viewPasswordReset();
            }
            else
            {
                viewEmailReset();
            }
        }

        protected void btnResetPasswordEmail_Click(object sender, EventArgs e)
        {
            try
            {
                var encEmail = aesMgr.EncryptToString(dfnResetEmail.Text);
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                string custUsername;
                if (loginMgr.ValidateEmail(encEmail, out custUsername))
                {
                    emailMgr.SendResetPasswordEmail(emailMgr.GetEmailingCode(encEmail), custUsername, baseUrl);
                }

                regSuccessBoxEmail.Visible = true;
                regSuccessBoxPassword.Visible = false;
                regErrorBox.Visible = false;
            }
            catch (Exception ex)
            {
                regErrMsg.InnerText = ex.Message;
                regSuccessBoxEmail.Visible = false;
                regSuccessBoxPassword.Visible = false;
                regErrorBox.Visible = true;
            }


        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                var timeDif = DateTime.Now - reqDate;
                if (timeDif.Minutes < CommonManager.GetRequestTimeoutMins())
                {
                    var encrPassword = aesMgr.EncryptToString(dfnResetPass.Text);
                    loginMgr.ResetPassword(resetEmail, encrPassword);

                    regSuccessBoxEmail.Visible = false;
                    regSuccessBoxPassword.Visible = true;
                    regErrorBox.Visible = false;
                }
                else
                    throw new Exception("your password reset request has timed out!");
            }
            catch (Exception ex)
            {
                regErrMsg.InnerText = ex.Message;
                regSuccessBoxEmail.Visible = false;
                regSuccessBoxPassword.Visible = false;
                regErrorBox.Visible = true;
            }
        }

        private void viewEmailReset()
        {
            btnResetPasswordEmail.Visible = true;
            btnResetPassword.Visible = false;

            liEmailAddress.Visible = true;
            liPassword.Visible = false;
            liConfPassword.Visible = false;
        }

        private void viewPasswordReset()
        {
            btnResetPasswordEmail.Visible = false;
            btnResetPassword.Visible = true;

            liEmailAddress.Visible = false;
            liPassword.Visible = true;
            liConfPassword.Visible = true;
        }
    }
}