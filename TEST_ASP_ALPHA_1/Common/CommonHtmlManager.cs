using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1.Common
{
    public class CommonHtmlManager
    {
        #region Items

        #region Slide Show

        public static HtmlGenericControl GetItemsPageSlideShowHtml(string location, string title)
        {
            var itemDivControl = HTMLControlsManager.GetDivTag(new[] { "item" });
            {
                var itemAnchor = HTMLControlsManager.GetAnchorTag("#");
                itemDivControl.Controls.Add(itemAnchor);
                {
                    var altImage = HTMLControlsManager.GetImageTag(location, null, "Games_banner");
                    itemAnchor.Controls.Add(altImage);
                }

                var catDivControl = HTMLControlsManager.GetDivTag(new[] { "cat-img-title", "cat-bg", "cat-box" });
                itemDivControl.Controls.Add(catDivControl);
                {
                    var headingControl = HTMLControlsManager.GetCustomTag("h2", new[] { "cat-heading" }, title);
                    catDivControl.Controls.Add(headingControl);
                }
            }
            return itemDivControl;
        }

        #endregion

        #region Sorting

        public static void AddItemViewPerPageList(int currentViewPerPage, ref HtmlGenericControl viewPerPage)
        {
            viewPerPage.Controls.Add(GetItemsPerPageList(CommonManager.GetValuesPerPage(), currentViewPerPage));
        }

        public static void AddItemSortByList(string sortBy, ref HtmlGenericControl sortByList)
        {
            sortByList.Controls.Add(GetItemSortValuesList(CommonManager.GetSortByOptions(), sortBy));
        }

        public static HtmlGenericControl GetItemSortValuesList(List<string> valuesList, string currentValue)
        {
            return GetCommonListItems<string>(valuesList, currentValue, "sortby");
        }

        public static HtmlGenericControl GetItemsPerPageList(List<int> valuesList, int currentValue)
        {
            return GetCommonListItems<int>(valuesList, currentValue, "count|page", "|1");
        }

        public static void AddItemPagination(ItemType pageItemType, int currentPage, int currentViewPerPage, ref HtmlGenericControl paginationCtrl, Dictionary<string, string> searchCriteria)
        {
            var itemCount = new ItemsModel().GetItemsCount(pageItemType, searchCriteria);
            var noOfPages = Convert.ToInt32(Math.Ceiling(((double)itemCount / (double)currentViewPerPage)));
            var displayingPages = CommonManager.GetDisplayingPages(currentPage, noOfPages);

            CreateCommonPaginationControl(currentPage, ref paginationCtrl, displayingPages, noOfPages);
        }

        #endregion

        #region Search

        public static void GetItemPriceSearchByOptions(ItemType pageItemType, Dictionary<string, string> searchCriteria, ref HtmlGenericControl priceRangeSearch)
        {
            var priceRanges = new ItemsModel().GetPriceRangesWithCounts(pageItemType);

            foreach (var item in priceRanges)
            {
                var liPriceRangeControl = HTMLControlsManager.GetCustomTag("li", null);
                priceRangeSearch.Controls.Add(liPriceRangeControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('priceRange', '" + item.Key + "');" } };
                    if (item.Key == searchCriteria[CommonManager.GetPriceRangeCriterionName()])
                        addAtt.Add("style", "font-weight: bold");

                    var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, null, null, null, addAtt);
                    liPriceRangeControl.Controls.Add(anchorCtrl);
                    {
                        var priceRange = item.Key.Split('|');
                        var spanPrice1 = HTMLControlsManager.GetCustomTag("span", null, priceRange[0] + " LKR - ");
                        anchorCtrl.Controls.Add(spanPrice1);

                        var spanPrice2 = HTMLControlsManager.GetCustomTag("span", null, priceRange[1] == "above" ? priceRange[1] : priceRange[1] + " LKR (" + item.Value + ")");
                        anchorCtrl.Controls.Add(spanPrice2);
                    }
                }
            }

            var liClearAllControl = HTMLControlsManager.GetCustomTag("li", null);
            priceRangeSearch.Controls.Add(liClearAllControl);
            {
                var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('priceRange', '');" } };
                var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, null, null, null, addAtt);
                liClearAllControl.Controls.Add(anchorCtrl);
                {
                    var spanPrice1 = HTMLControlsManager.GetCustomTag("span", null, "Clear All");
                    anchorCtrl.Controls.Add(spanPrice1);

                }
            }
        }

        public static void GetItemYearSearchByOptions(ItemType pageItemType, Dictionary<string, string> searchCriteria, ref HtmlGenericControl yearSearch)
        {
            var yearRanges = new ItemsModel().GetYearsWithCounts(pageItemType);

            foreach (var item in yearRanges)
            {
                var liYearRangeControl = HTMLControlsManager.GetCustomTag("li", null);
                yearSearch.Controls.Add(liYearRangeControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('year', '" + item.Key + "');" } };
                    if (item.Key == searchCriteria[CommonManager.GetYearCriterionName()])
                        addAtt.Add("style", "font-weight: bold");

                    var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, item.Key + " (" + item.Value + ")", null, null, addAtt);
                    liYearRangeControl.Controls.Add(anchorCtrl);
                }
            }

            var liClearAllControl = HTMLControlsManager.GetCustomTag("li", null);
            yearSearch.Controls.Add(liClearAllControl);
            {
                var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('year', '');" } };
                var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, "Clear All", null, null, addAtt);
                liClearAllControl.Controls.Add(anchorCtrl);
            }
        }

        public static void GetItemGenreSearchByOptions(ItemType pageItemType, Dictionary<string, string> searchCriteria, ref HtmlGenericControl genreSearch)
        {
            var genres = new ItemsModel().GetGenresWithCounts(pageItemType);

            foreach (var item in genres)
            {
                var liGenreControl = HTMLControlsManager.GetCustomTag("li", null);
                genreSearch.Controls.Add(liGenreControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('genre', '" + item.Key + "');" } };
                    if (item.Key == searchCriteria[CommonManager.GetGenreCriterionName()])
                        addAtt.Add("style", "font-weight: bold");
                    var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, item.Key + " (" + item.Value + ")", null, null, addAtt);
                    liGenreControl.Controls.Add(anchorCtrl);
                }
            }

            var liClearAllControl = HTMLControlsManager.GetCustomTag("li", null);
            genreSearch.Controls.Add(liClearAllControl);
            {
                var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('genre', '');" } };
                var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, "Clear All", null, null, addAtt);
                liClearAllControl.Controls.Add(anchorCtrl);
            }
        }

        public static void GetItemSalesSearchByOptions(ItemType pageItemType, Dictionary<string, string> searchCriteria, ref HtmlGenericControl salesSearch)
        {
            var bestSellers = new ItemsModel().GetBestSellersWithCounts(pageItemType);
            foreach (var item in bestSellers)
            {
                var liBestSellersControl = HTMLControlsManager.GetCustomTag("li", null);
                salesSearch.Controls.Add(liBestSellersControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('onSale|bestSeller', '|1');" } };
                    if ("1" == searchCriteria[CommonManager.GetBestSellerCriterionName()])
                        addAtt.Add("style", "font-weight: bold");
                    var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, item.Key + " (" + item.Value + ")", null, null, addAtt);
                    liBestSellersControl.Controls.Add(anchorCtrl);
                }
            }

            var onSales = new ItemsModel().GetOnSaleWithCounts(pageItemType);
            foreach (var item in onSales)
            {
                var liOnSaleControl = HTMLControlsManager.GetCustomTag("li", null);
                salesSearch.Controls.Add(liOnSaleControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('onSale|bestSeller', '1|');" } };
                    if ("1" == searchCriteria[CommonManager.GetOnSaleCriterionName()])
                        addAtt.Add("style", "font-weight: bold");

                    var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, item.Key + " (" + item.Value + ")", null, null, addAtt);
                    liOnSaleControl.Controls.Add(anchorCtrl);
                }
            }

            var liClearAllControl = HTMLControlsManager.GetCustomTag("li", null);
            salesSearch.Controls.Add(liClearAllControl);
            {
                var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('onSale|bestSeller', '')" } };
                var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, "Clear All", null, null, addAtt);
                liClearAllControl.Controls.Add(anchorCtrl);
            }
        }
        #endregion

        #endregion

        #region PurchItems

        public static void AddPurchItemPagination(int currentPage, int currentViewPerPage, ref HtmlGenericControl paginationCtrl, PurchItemGetType getType, int custId = 0, string custEmail = null, int purchCartId = 0, DateTime? fromPurchDate = null, DateTime? toPurchDate = null)
        {
            var itemCount = new PurchItemModel().GetPurchItemCount(getType, custId, custEmail, purchCartId, fromPurchDate, toPurchDate);
            var noOfPages = Convert.ToInt32(Math.Ceiling(((double)itemCount / (double)currentViewPerPage)));
            var displayingPages = CommonManager.GetDisplayingPages(currentPage, noOfPages);

            CreateCommonPaginationControl(currentPage, ref paginationCtrl, displayingPages, noOfPages);
        }

        #endregion

        #region Common

        private static HtmlGenericControl GetCommonListItems<T>(List<T> valuesList, T currentValue, string queryStringVal, string appendValueForParam = null)
        {
            HtmlGenericControl itemSelectedValueControl = HTMLControlsManager.GetCustomTag("li", null);
            {
                var ancherCurrentValue = HTMLControlsManager.GetAnchorTag(queryStringVal + "=" + currentValue, null, currentValue.ToString());
                itemSelectedValueControl.Controls.Add(ancherCurrentValue);
                {
                    var spanRightArrow = HTMLControlsManager.GetCustomTag("span", new[] { "right-arrow" });
                    ancherCurrentValue.Controls.Add(spanRightArrow);
                }

                var otherValuesUnorderedList = HTMLControlsManager.GetCustomTag("ul", null);
                itemSelectedValueControl.Controls.Add(otherValuesUnorderedList);
                {
                    foreach (var item in valuesList.Except(new List<T> { currentValue }).OrderBy(rec => rec))
                    {
                        var otherValueListItemControl = HTMLControlsManager.GetCustomTag("li", null);
                        otherValuesUnorderedList.Controls.Add(otherValueListItemControl);
                        {
                            var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('" + queryStringVal + "', '" + item + appendValueForParam + "');" } };

                            var anchorOtherValueControl = HTMLControlsManager.GetAnchorTag("#", null, item.ToString(), null, null, addAtt);
                            otherValueListItemControl.Controls.Add(anchorOtherValueControl);
                        }
                    }
                }
            }
            return itemSelectedValueControl;
        }

        private static void CreateCommonPaginationControl(int currentPage, ref HtmlGenericControl paginationCtrl, List<int> displayingPages, int noOfPages)
        {
            HtmlGenericControl itemLiControl;
            HtmlAnchor anchorControl;

            if (currentPage > 1)
            {
                itemLiControl = HTMLControlsManager.GetCustomTag("li", null);
                paginationCtrl.Controls.Add(itemLiControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('page', '" + (currentPage - 1) + "');" } };
                    anchorControl = HTMLControlsManager.GetAnchorTag("#", null, "\u00AB", null, null, addAtt);

                    itemLiControl.Controls.Add(anchorControl);
                }
            }

            foreach (var i in displayingPages)
            {
                if (currentPage == i)
                    itemLiControl = HTMLControlsManager.GetCustomTag("li", new[] { "active" });
                else
                    itemLiControl = HTMLControlsManager.GetCustomTag("li", null);
                paginationCtrl.Controls.Add(itemLiControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('page', '" + i + "');" } };
                    anchorControl = HTMLControlsManager.GetAnchorTag("#", null, i.ToString(), null, null, addAtt);
                    itemLiControl.Controls.Add(anchorControl);
                }
            }

            if (currentPage < noOfPages)
            {
                itemLiControl = HTMLControlsManager.GetCustomTag("li", null);
                paginationCtrl.Controls.Add(itemLiControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('page', '" + (currentPage + 1) + "');" } };
                    anchorControl = HTMLControlsManager.GetAnchorTag("#", null, "\u00BB", null, null, addAtt);
                    itemLiControl.Controls.Add(anchorControl);
                }
            }
        }

        #endregion

    }
}