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
    public partial class MyAccountOrders : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        CustomerModel custMgr;

        public int currentViewPerPage = CommonManager.GetDefaultValuePerTable();
        public int currentPage = 1;

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
                    Response.Redirect("Login.aspx");
                }
            }

            var _page = Request.QueryString["page"];
            if (_page != null)
            {
                currentPage = Convert.ToInt32(_page);
            }

            CommonHtmlManager.AddPurchItemPagination(currentPage, currentViewPerPage, ref this.paginationCtrl, PurchItemGetType.customerEmail, 0, 
                new AESManager().EncryptToString(Session[CommonManager.GetCustEmailSessionName()].ToString()));
        }
    }
}