using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class AdminSalesDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string GetPurchasedItemsToday()
        {
            try
            {
                var purchItemsToday = new CounterModel().GetCounterValue(Common.CommonManager.Counter_GetPurchasedItemsToday_Name()).Count;
                return purchItemsToday.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [System.Web.Services.WebMethod]
        public static string getPieChart_PurchasedItemsByType(string fromDate, string toDate)
        {
            try
            {
                var chartData = "";
                var fromDt = Convert.ToDateTime(fromDate);
                var toDt = Convert.ToDateTime(toDate);

                if (fromDt.Year > 1900 && toDt.Year > 1900)
                    chartData = new PurchItemModel().Chart_GetPurchasedItemByType(fromDt, toDt);
                else if (fromDt.Year > 1900 && toDt.Year == 1900)
                    chartData = new PurchItemModel().Chart_GetPurchasedItemByType(fromDt);
                else if (fromDt.Year == 1900 && toDt.Year > 1900)
                    chartData = new PurchItemModel().Chart_GetPurchasedItemByType(toDt);
                else
                    chartData = new PurchItemModel().Chart_GetPurchasedItemByType();

                return chartData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}