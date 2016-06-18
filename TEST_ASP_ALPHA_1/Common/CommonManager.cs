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
        const string custId = "CustId";
        const string custEmail = "CustEmail";
        const string custName = "CustName";

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
            var searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"];
            return searchCriteria.Split('|')[0];
        }

        public static string GetYearCriterionName()
        {
            var searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"];
            return searchCriteria.Split('|')[1];
        }

        public static string GetGenreCriterionName()
        {
            var searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"];
            return searchCriteria.Split('|')[2];
        }

        public static string GetBestSellerCriterionName()
        {
            var searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"];
            return searchCriteria.Split('|')[3];
        }

        public static string GetOnSaleCriterionName()
        {
            var searchCriteria = ConfigurationManager.AppSettings["SearchCriteria"];
            return searchCriteria.Split('|')[4];
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
            return custId;
        }

        public static string GetCustEmailSessionName()
        {
            return custEmail;
        }

        public static string GetCustNameSessionName()
        {
            return custName;
        }

        #endregion

        #endregion
    }
}