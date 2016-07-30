using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace TEST_ASP_ALPHA_1.Common
{
    public class CommonManager
    {
        const string ses_custId = "CustId";
        const string ses_custEmail = "CustEmail";
        const string ses_custName = "CustName";
        const string ses_cartItems = "CartItems";
        const string ses_cartItemsWithQty = "CartItemsWithQty";
        const string ses_checkoutEligible = "CheckoutEligible";

        #region Web Config Manager

        #region Sort By
        public static List<int> GetValuesPerPage()
        {
            List<int> valuesPerPage = new List<int>();
            var values = ConfigurationManager.AppSettings["ValuesPerPage"];
            foreach (var item in values.Split('|'))
            {
                valuesPerPage.Add(Convert.ToInt32(item));
            }
            return valuesPerPage;
        }

        public static int GetDefaultValuePerPage()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultValuePerPage"]);
        }

        public static List<string> GetSortByOptions()
        {
            List<string> sortByOptions = new List<string>();
            var values = ConfigurationManager.AppSettings["SortBy"];
            foreach (var item in values.Split('|'))
            {
                sortByOptions.Add(item);
            }
            return sortByOptions;
        }

        public static string GetDefaultSortByOption()
        {
            return ConfigurationManager.AppSettings["DefaultSortBy"];
        }
        
        public static List<int> GetDisplayingPages(int currentPage, int noOfPages)
        {
            List<int> displayingPages = new List<int>();
            var calcMaxPage = 0;
            var calcMinPage = 0;

            if (noOfPages > 5 && currentPage > 3)
            {
                calcMaxPage = currentPage + 2;
                calcMinPage = currentPage - 2;

                var extraPagesForMax = 0;

                if (calcMaxPage > noOfPages)
                {
                    extraPagesForMax = calcMaxPage - noOfPages;
                    calcMinPage = calcMinPage - extraPagesForMax;
                    calcMaxPage = noOfPages;
                }
            }
            else
            {
                calcMinPage = 1;

                if (noOfPages > 5)
                    calcMaxPage = 5;
                else
                    calcMaxPage = noOfPages;
            }

            for (int i = calcMinPage; i <= calcMaxPage; i++)
            {
                displayingPages.Add(i);
            }
            return displayingPages;
        }

        public static int GetDefaultValuePerTable()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["DefaultValuePerTable"]);
        }

        #endregion

        #region Search By

        public static Dictionary<string,string> GetSearchByDictionary()
        {
            var searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"];
            var splittedCriteria = searchCriteria.Split('|');
            Dictionary<string, string> returnDic = new Dictionary<string, string>();
            foreach (var item in splittedCriteria)
            {
                returnDic.Add(item, "");
            }
            return returnDic;
        }

        public static string GetPriceRangeCriterionName()
        {
            return GetSearchCriterionName(0);
        }

        public static string GetYearCriterionName()
        {
            return GetSearchCriterionName(1);
        }

        public static string GetGenreCriterionName()
        {
            return GetSearchCriterionName(2);
        }

        public static string GetBestSellerCriterionName()
        {
            return GetSearchCriterionName(3);
        }

        public static string GetOnSaleCriterionName()
        {
            return GetSearchCriterionName(4);
        }

        public static string GetIdCriterionName()
        {
            return GetSearchCriterionName(5);
        }
        
        public static string GetTitleCriterionName()
        {
            return GetSearchCriterionName(6);
        }

        private static string GetSearchCriterionName(int index)
        {
            var searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"];
            return searchCriteria.Split('|')[index];
        }

        public static string GetSearchByIdCriterion(List<int> items)
        {
            StringBuilder searchCriterion = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                if (i < items.Count - 1)
                    searchCriterion.Append(items[i] + ",");
                else if (i == items.Count - 1)
                    searchCriterion.Append(items[i]);
            }
            return searchCriterion.ToString();
        }

        #endregion

        #region Contact Details

        public static string GetAddress()
        {
            string address = ConfigurationManager.AppSettings["Address"];
            StringBuilder add = new StringBuilder();

            var splitAddress = address.Split(',');
            for (int i = 0; i < splitAddress.Count(); i++)
			{
                if (i < splitAddress.Count() - 1)
                    add.Append(splitAddress[i] + ", <br/>");
                else if (i == splitAddress.Count() - 1)
                    add.Append(splitAddress[i]);
            }

            return add.ToString();
        }

        public static string GetTelephoneNo()
        {
            string telephone = ConfigurationManager.AppSettings["Telephone"];
            return telephone;
        }

        #endregion

        #region Login

        public static int GetRequestTimeoutMins()
        {
            string reqTimeOutMins = ConfigurationManager.AppSettings["ReqTimeoutMinutes"];
            return Convert.ToInt32(reqTimeOutMins);
        }

        #endregion

        #region Sessions

        public static string GetCustIdSessionName()
        {
            return ses_custId;
        }

        public static string GetCustEmailSessionName()
        {
            return ses_custEmail;
        }

        public static string GetCustNameSessionName()
        {
            return ses_custName;
        }

        public static string GetCartItemsSessionName()
        {
            return ses_cartItems;
        }

        public static string GetCartItemsWithQtySessionName()
        {
            return ses_cartItemsWithQty;
        }

        public static string GetCheckoutEligibleSessionName()
        {
            return ses_checkoutEligible;
        }

        #endregion

        #region Item Status
        public static string Status_GetNewItemName()
        {
            var status = ConfigurationManager.AppSettings["ItemStatuses"];
            return status.Split('|')[0];
        }

        public static string Status_GetDeliveredItemName()
        {
            var status = ConfigurationManager.AppSettings["ItemStatuses"];
            return status.Split('|')[1];
        }

        public static string Status_GetCancelledItemName()
        {
            var status = ConfigurationManager.AppSettings["ItemStatuses"];
            return status.Split('|')[2];
        }


        #endregion

        #region Colors

        public static string Colors_GetItemStatusColorNew()
        {
            var colors = ConfigurationManager.AppSettings["ItemSatatusColors"];
            return colors.Split('|')[0];
        }

        public static string Colors_GetItemStatusColorDelivered()
        {
            var colors = ConfigurationManager.AppSettings["ItemSatatusColors"];
            return colors.Split('|')[1];
        }

        public static string Colors_GetItemStatusColorCancelled()
        {
            var colors = ConfigurationManager.AppSettings["ItemSatatusColors"];
            return colors.Split('|')[2];
        }

        #endregion

        #region Counter Names

        public static string Counter_GetLoggedInToday_Name()
        {
            return ConfigurationManager.AppSettings["Count_LoggedInToday"];
        }

        public static string Counter_GetPurchasedUsersToday_Name()
        {
            return ConfigurationManager.AppSettings["Count_PurchasedUsersToday"];
        }

        public static string Counter_GetPurchasedItemsToday_Name()
        {
            return ConfigurationManager.AppSettings["Count_PurchasedItemsToday"];
        }

        public static int Counter_GetRefreshTimeOut()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["GetRefreshTimeOut"]);
        }


        #endregion

        #endregion
    }
}