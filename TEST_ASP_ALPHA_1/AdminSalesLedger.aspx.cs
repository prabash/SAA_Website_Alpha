using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1
{
    public partial class AdminSalesLedger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[CommonManager.Session_GetAdminUsernameSessionName()] == null)
                {
                    throw new HttpException(403, "Forbidden");
                }
                else
                {
                    if (Session[CommonManager.Session_GetAdminTypeSessionName()].ToString() != "Main")
                        throw new HttpException(403, "Forbidden");
                }
            }
        }
    }
}