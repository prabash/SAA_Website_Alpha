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
    public partial class AdminLogin : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        AdminModel adminMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            adminMgr = new AdminModel();

            var logout = Request.QueryString["logout"];
            if (!String.IsNullOrEmpty(logout))
            {
                if (logout == "true")
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    Session.Remove(CommonManager.Session_GetAdminIdSessionName());
                    Session.Remove(CommonManager.Session_GetAdminUsernameSessionName());
                    Session.Remove(CommonManager.Session_GetAdminTypeSessionName());
                    Response.Redirect(baseUrl + "AdminLogin.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var encUsername = aesMgr.EncryptToString(dfnLoginUsername.Text);
                var encPassword = aesMgr.EncryptToString(dfnLoginPass.Text);
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

                int adminId;
                string adminType;

                if (adminMgr.ValidateLogin(encUsername, encPassword, out adminId, out adminType))
                {
                    regErrorBox.Visible = false;

                    Session.Add(CommonManager.Session_GetAdminIdSessionName(), adminId);
                    Session.Add(CommonManager.Session_GetAdminUsernameSessionName(), dfnLoginUsername.Text);
                    Session.Add(CommonManager.Session_GetAdminTypeSessionName(), adminType);

                    Response.Redirect(baseUrl + "AdminDashboard.aspx");
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