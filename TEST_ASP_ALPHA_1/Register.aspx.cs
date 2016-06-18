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
    public partial class Register : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        CustomerModel loginMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            loginMgr = new CustomerModel();
        }

        protected void btnRegCreateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                var encrEmail = aesMgr.EncryptToString(dfnRegEmail.Text);
                var encrPassword = aesMgr.EncryptToString(dfnRegPass.Text);
                var username = dfnRegUsername.Text;

                loginMgr.RegisterUser(encrEmail, username, encrPassword);
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                emailMgr.SendActivationEmail(encrEmail, username, baseUrl);

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