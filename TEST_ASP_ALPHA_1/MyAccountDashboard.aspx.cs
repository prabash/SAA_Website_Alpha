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
    public partial class MyAccountDashboard : System.Web.UI.Page
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
                else
                {
                    LoadCurrentDetails();
                }
            }
        }

        private void LoadCurrentDetails()
        {
            var email = Session[CommonManager.GetCustEmailSessionName()].ToString();
            var custDetails = custMgr.GetCustomerDetails(CustomerGetType.email, 0, aesMgr.EncryptToString(email));
            if (custDetails != null)
            {
                var customer = custDetails.First();
                txtName.InnerText = customer.name;
                txtEmail.InnerText = email;
                txAddress.InnerHtml = customer.addressLine1 + ",<br/>"+ customer.addressLine2 + ",<br/>" + customer.city;
            }
        }
    }
}