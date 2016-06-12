using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class Login : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        LoginManager loginMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            loginMgr = new LoginManager();
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var encEmail = aesMgr.EncryptToString(dfnLoginEmail.Text);
            var encPassword = aesMgr.EncryptToString(dfnLoginPass.Text);
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            if(loginMgr.ValidateLogin(encEmail, encPassword))
            {
                Response.Redirect(baseUrl + "Default.aspx");
            }
        }
    }
}