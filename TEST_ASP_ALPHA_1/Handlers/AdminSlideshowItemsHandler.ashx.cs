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
    /// Summary description for AdminSlideshowItemsHandler
    /// </summary>
    public class AdminSlideshowItemsHandler : IHttpHandler
    {
        #region Handler

        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");
            var collectionEmployee = new IndexModel().GetSlideShowDetails();
            string strResponse = string.Empty;

            if (strOperation == null)
            {
                //oper = null which means its first load.
                var jsonSerializer = new JavaScriptSerializer();
                context.Response.Write(jsonSerializer.Serialize(collectionEmployee.AsQueryable<SlideShowObj>().ToList<SlideShowObj>()));
            }
            else if (strOperation == "del")
            {
                var id = Convert.ToInt32(forms.Get("EmpId").ToString());
                new IndexModel().DeleteSlideshowItem(new SlideShowObj { id = id });
                strResponse = "Record removed successfully!";
                context.Response.Write(strResponse);
            }
            else
            {
                string strOut = string.Empty;
                AddEdit(forms, out strOut);
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

        private void AddEdit(NameValueCollection forms, out string strResponse)
        {
            string strOperation = forms.Get("oper");
            string strEmpId = string.Empty;

            string location = forms.Get("location").ToString();
            string title = forms.Get("title").ToString();

            SlideShowObj objItem = new SlideShowObj();
            objItem.location = location;
            objItem.title = title;

            if (strOperation == "add")
            {
                new IndexModel().AddSlideshowItem(objItem);
            }
            else if (strOperation == "edit")
            {
                objItem.id = Convert.ToInt32(forms.Get("EmpId").ToString());
                new IndexModel().UpdateSlideshowItem(objItem);
            }

            strResponse = "Record updated successfully";
        }

        #endregion
    }
}