using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TEST_ASP_ALPHA_1.Common
{
    public class CommonManager
    {
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

        #endregion
    }
}