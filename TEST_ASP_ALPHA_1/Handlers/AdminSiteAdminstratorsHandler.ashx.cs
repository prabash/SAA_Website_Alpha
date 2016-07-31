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
    /// Summary description for AdminSiteAdminstratorsHandler
    /// </summary>
    public class AdminSiteAdminstratorsHandler : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");
            var collection = new AdminModel().GetAdminDetails();
            collection.ForEach(rec => rec.Username = new AESManager().DecryptString(rec.Username));

            string strResponse = string.Empty;

            if (strOperation == null)
            {
                //oper = null which means its first load.
                var jsonSerializer = new JavaScriptSerializer();
                context.Response.Write(jsonSerializer.Serialize(collection.AsQueryable<AdminUserObject>().ToList<AdminUserObject>()));
            }
            else if (strOperation == "del")
            {
                var id = Convert.ToInt32(forms.Get("AdminId").ToString());
                new AdminModel().DeleteAdminUser(id);
                strResponse = "Record removed successfully!";
                context.Response.Write(strResponse);
            }
            else
            {
                string strOut = string.Empty;
                Save(forms, out strOut);
                context.Response.Write(strOut);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void Save(NameValueCollection forms, out string strResponse)
        {
            string strOperation = forms.Get("oper");
            string strEmpId = string.Empty;

            var username = new AESManager().EncryptToString(forms.Get("Username").ToString());
            var type = forms.Get("Type").ToString();

            if (strOperation == "add")
            {
                var password = new AESManager().EncryptToString("Password123");
                new AdminModel().RegisterAdmin(username, password, type);
            }
            else if (strOperation == "edit")
            {
                var id = Convert.ToInt32(forms.Get("Id").ToString());
                new AdminModel().UpdateAdminData(id, username, type);
            }

            strResponse = "Record updated successfully";
        }
    }
}