using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class AdminSalesDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[CommonManager.Session_GetAdminUsernameSessionName()] == null)
                {
                    throw new HttpException(403, "Forbidden");
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetCustDetails(string custId)
        {
            try
            {
                var custData = new CustomerModel().GetCustomerDetails(Common.CustomerGetType.id, Convert.ToInt32(custId)).First();
                custData.emailAddress = new AESManager().DecryptString(custData.emailAddress);

                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(custData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}