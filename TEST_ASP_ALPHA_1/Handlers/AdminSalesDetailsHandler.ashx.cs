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
    /// Summary description for AdminSalesDetailsHandler
    /// </summary>
    public class AdminSalesDetailsHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");
            string strResponse = string.Empty;

            var _cartId = context.Request.QueryString["cartId"];
            if (_cartId != null)
            {
                var cartId = Convert.ToInt32(_cartId);
                var collection = new PurchItemModel().GetPurchItemDetails(PurchItemGetType.purchCartId, 0, null, cartId);

                var jsonSerializer = new JavaScriptSerializer();

                if (strOperation == null)
                {
                    //oper = null which means its first load.
                    context.Response.Write(jsonSerializer.Serialize(collection.AsQueryable<PurchaseItemObject>().ToList<PurchaseItemObject>()));
                }
            }

            if (strOperation == "edit")
            {
                ItemStatus itemStatus;

                string status = forms.Get("Status").ToString();
                Enum.TryParse<ItemStatus>(status, out itemStatus);

                var purchId = Convert.ToInt32(forms.Get("PurchId").ToString());
                new PurchItemModel().UpdateItemStatus(purchId, itemStatus);

                strResponse = "Record updated successfully";
                context.Response.Write(strResponse);
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