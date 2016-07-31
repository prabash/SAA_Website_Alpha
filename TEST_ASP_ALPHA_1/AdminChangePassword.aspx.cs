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
    public partial class AdminChangePassword : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        AdminModel adminMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            adminMgr = new AdminModel();

            if (!IsPostBack)
            {
                if (Session[CommonManager.Session_GetAdminUsernameSessionName()] == null)
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    Response.Redirect("AdminLogin.aspx");
                }
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                var encUsername = aesMgr.EncryptToString(Session[CommonManager.Session_GetAdminUsernameSessionName()].ToString());
                var encOldPassword = aesMgr.EncryptToString(dfAdminOldPass.Text);
                var encNewPassword = aesMgr.EncryptToString(dfAdminNewPass.Text);

                adminMgr.ResetPassword(encUsername, encNewPassword, encOldPassword);

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