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
    /// Summary description for AdminSalesMasterHandler
    /// </summary>
    public class AdminSalesMasterHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");

            ItemStatus itemStatus = ItemStatus.New;

            var _itemStatus = context.Request.QueryString["itemStatus"];
            if (!String.IsNullOrEmpty(_itemStatus))
            {
                Enum.TryParse<ItemStatus>(_itemStatus, out itemStatus);
            }

            DateTime? fromDate = null, toDate = null;
            var _fromDate = context.Request.QueryString["fromDate"];
            if (!String.IsNullOrEmpty(_fromDate))
            {
                fromDate = Convert.ToDateTime(_fromDate);
            }

            var _toDate = context.Request.QueryString["toDate"];
            if (!String.IsNullOrEmpty(_toDate))
            {
                toDate = Convert.ToDateTime(_toDate);
            }

            var collectionEmployee = new PurchCartModel().GetSalesCartDetails(itemStatus, fromDate, toDate);

            string strResponse = string.Empty;
            var jsonSerializer = new JavaScriptSerializer();

            if (strOperation == null)
            {
                //oper = null which means its first load.
                context.Response.Write(jsonSerializer.Serialize(collectionEmployee.AsQueryable<SalesCartObject>().ToList<SalesCartObject>()));
            }
            if (strOperation == "edit")
            {
                ItemStatus cartStatus;

                string status = forms.Get("CartStatus").ToString();
                Enum.TryParse<ItemStatus>(status, out cartStatus);

                var cartId = Convert.ToInt32(forms.Get("CartId").ToString());
                new PurchCartModel().UpdateCartStatus(cartId, cartStatus);

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