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
    public partial class MyAccountChangePassword : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        CustomerManager custMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            custMgr = new CustomerManager();

            if (!IsPostBack)
            {
                if (Session[CommonManager.GetCustEmailSessionName()] == null)
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                var encEmail = aesMgr.EncryptToString(Session[CommonManager.GetCustEmailSessionName()].ToString());
                var encOldPassword = aesMgr.EncryptToString(dfOldPass.Text);
                var encNewPassword = aesMgr.EncryptToString(dfNewPass.Text);

                custMgr.ResetPassword(encEmail, encNewPassword, encOldPassword);

                regSuccessBox.Visible = true;
                regErrorBox.Visible = false;
            }
            catch (Exception ex)
            {
                regErrMsg.InnerText = ex.Message;
                regSuccessBox.Visible = false;
                regErrorBox.Visible = true;
            }
        }
    }
}