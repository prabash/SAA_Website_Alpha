using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1.Handlers
{
    /// <summary>
    /// Summary description for AdminUserDetailsHandler
    /// </summary>
    public class AdminUserDetailsHandler : IHttpHandler
    {

        #region Handler

        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");
            var collectionEmployee = new CustomerModel().GetCustomerDetails(CustomerGetType.all);
            collectionEmployee.ForEach(rec => rec.emailAddress = new AESManager().DecryptString(rec.emailAddress));

            string strResponse = string.Empty;

            if (strOperation == null)
            {
                //oper = null which means its first load.
                var jsonSerializer = new JavaScriptSerializer();
                context.Response.Write(jsonSerializer.Serialize(collectionEmployee.AsQueryable<CustomerObject>().ToList<CustomerObject>()));
            }
            else if (strOperation == "del")
            {
                var id = Convert.ToInt32(forms.Get("CustId").ToString());
                new CustomerModel().DeleteCustomer(id);
                strResponse = "Record removed successfully!";
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

        #endregion
    }
}