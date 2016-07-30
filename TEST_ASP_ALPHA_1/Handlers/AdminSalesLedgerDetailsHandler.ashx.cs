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
    /// Summary description for AdminSalesLedgerDetailsHandler
    /// </summary>
    public class AdminSalesLedgerDetailsHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");

            int cartId = 0;
            var _id = context.Request.QueryString["id"];
            if (!String.IsNullOrEmpty(_id))
            {
                cartId = Convert.ToInt32(_id);
            }

            var collection = new PurchItemModel().GetPurchItemDetails(Common.PurchItemGetType.purchCartId, 0, null, cartId);
            var jsonSerializer = new JavaScriptSerializer();

            if (strOperation == null)
            {
                //oper = null which means its first load.
                context.Response.Write(jsonSerializer.Serialize(collection.AsQueryable<PurchaseItemObject>().ToList<PurchaseItemObject>()));
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