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
    public partial class CheckoutMethodUpdate : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        CustomerModel custMgr;

        protected void Page_Load(object sender, EventArgs e)
        {
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            custMgr = new CustomerModel();

            if (!IsPostBack)
            {
                if (Session[CommonManager.GetCustEmailSessionName()] == null)
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    Response.Redirect("CheckoutMethod.aspx");
                }
                else
                {
                    LoadCurrentDetails();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(Session[CommonManager.GetCustIdSessionName()]);
                var email = Session[CommonManager.GetCustEmailSessionName()].ToString();

                custMgr.UpdateUserData(id, aesMgr.EncryptToString(email), dfName.Value, dfTelephone.Value, dfAddLine1.Value + "|" + dfAddLine2.Value + "|" + cmbCity.Value);

                regErrorBox.Visible = false;
            }
            catch (Exception ex)
            {
                regErrMsg.InnerText = ex.Message;
                regErrorBox.Visible = true;
            }
        }


        void LoadCurrentDetails()
        {
            try
            {
                var email = Session[CommonManager.GetCustEmailSessionName()].ToString();
                var custDetails = custMgr.GetCustomerDetails(CustomerGetType.email, 0, aesMgr.EncryptToString(email));
                if (custDetails != null)
                {
                    var customer = custDetails.First();
                    dfName.Value = customer.name;
                    dfAddLine1.Value = customer.addressLine1;
                    dfAddLine2.Value = customer.addressLine2;
                    cmbCity.Value = customer.city;
                    dfTelephone.Value = customer.telephoneNo;
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