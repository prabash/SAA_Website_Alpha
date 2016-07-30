using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1.Handlers
{
    /// <summary>
    /// Summary description for AdminSalesLedgerHandler
    /// </summary>
    public class AdminSalesLedgerHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");

            DateTime? fromDate = null, toDate = null;
            var _fromDate = context.Request.QueryString["fromDate"];
            if (!String.IsNullOrEmpty(_fromDate))
            {
                fromDate = Convert.ToDateTime(_fromDate);
            }
            else
                fromDate = DateTime.Today;

            var _toDate = context.Request.QueryString["toDate"];
            if (!String.IsNullOrEmpty(_toDate))
            {
                toDate = Convert.ToDateTime(_toDate);
            }
            else
                toDate = DateTime.Today;

            var collection = new PurchCartModel().GetCartDetails(CartGetType.purchDate, 0, null, 0, 0, 0, fromDate, toDate);
            collection.ForEach(rec => rec.CustEmailAddress = new AESManager().DecryptString(rec.CustEmailAddress));

            string strResponse = string.Empty;
            var jsonSerializer = new JavaScriptSerializer();

            if (strOperation == null)
            {
                //oper = null which means its first load.
                context.Response.Write(jsonSerializer.Serialize(collection.AsQueryable<CartObject>().ToList<CartObject>()));
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}