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
    /// Summary description for AdminItemsHandler
    /// </summary>
    public class AdminItemsHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            System.Collections.Specialized.NameValueCollection forms = context.Request.Form;
            string strOperation = forms.Get("oper");
            var collectionEmployee = new ItemsModel().GetItemDetails(Common.ItemType.All, Common.ItemSortBy.NameAsc);
            string strResponse = string.Empty;

            if (strOperation == null)
            {
                //oper = null which means its first load.
                var jsonSerializer = new JavaScriptSerializer();
                context.Response.Write(jsonSerializer.Serialize(collectionEmployee.AsQueryable<ItemObject>().ToList<ItemObject>()));
            }
            else if (strOperation == "del")
            {
                var id = Convert.ToInt32(forms.Get("ItemId").ToString());
                new ItemsModel().DeleteSalesItem(id);
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

            var title = forms.Get("title").ToString();
            var defaultLocation = forms.Get("defaultLocation").ToString();
            var altLocation = forms.Get("altLocation").ToString();
            var regularPrice = forms.Get("regularPrice").ToString();
            var currentPrice = forms.Get("currentPrice").ToString();
            var description = forms.Get("description").ToString();
            var overview = forms.Get("overview").ToString();
            var year = forms.Get("year").ToString();
            var type = forms.Get("type").ToString();
            var genre = forms.Get("genre").ToString();
            var rating = forms.Get("rating").ToString();
            var onSale = forms.Get("onSale").ToString();
            var bestSeller = forms.Get("bestSeller").ToString();

            ItemObject objItem = new ItemObject();
            objItem.title = title;
            objItem.defaultLocation = defaultLocation;
            objItem.altLocation = altLocation;
            objItem.regularPrice = Convert.ToDouble(regularPrice);
            objItem.currentPrice = Convert.ToDouble(currentPrice);
            objItem.description = description;
            objItem.overview = overview;
            objItem.year = year;
            objItem.type = type;
            objItem.genre = genre;
            objItem.rating = Convert.ToDouble(rating);
            objItem.onSale = Convert.ToBoolean(onSale);
            objItem.bestSeller = Convert.ToBoolean(bestSeller); ;

            if (strOperation == "add")
            {
                new ItemsModel().AddSalesItems(new List<ItemObject> { objItem });
            }
            else if (strOperation == "edit")
            {
                objItem.Id = Convert.ToInt32(forms.Get("ItemId").ToString());
                new ItemsModel().UpdateSalesItems(new List<ItemObject> { objItem });
            }

            strResponse = "Record updated successfully";
        }
    }
}