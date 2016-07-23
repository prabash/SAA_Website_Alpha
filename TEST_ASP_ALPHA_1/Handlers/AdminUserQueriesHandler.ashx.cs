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
    /// Summary description for AdminUserQueriesHandler
    /// </summary>
    public class AdminUserQueriesHandler : IHttpHandler
    {
        #region Handler

        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");
            var collectionEmployee = new CustCommentsModel().GetCustomerComments();

            string strResponse = string.Empty;
            var jsonSerializer = new JavaScriptSerializer();

            if (strOperation == null)
            {
                //oper = null which means its first load.
                context.Response.Write(jsonSerializer.Serialize(collectionEmployee.AsQueryable<CustomerCommentObject>().ToList<CustomerCommentObject>()));
            }
            else if (strOperation == "del")
            {
                var Ids = (List<int>)jsonSerializer.Deserialize(forms.Get("CustId").ToString(), typeof(List<int>));
                new CustCommentsModel().DeleteCustComments(Ids);
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