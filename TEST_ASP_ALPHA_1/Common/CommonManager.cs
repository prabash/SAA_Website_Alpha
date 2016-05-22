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
    }
}