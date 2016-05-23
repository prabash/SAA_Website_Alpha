using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class BrowseGamesList : System.Web.UI.Page
    {
        #region Private Variables
        private int currentViewPerPage = CommonManager.GetDefaultValuePerPage();
        private string sortBy = CommonManager.GetDefaultSortByOption();
        private bool ascending = true;
        private int currentPage = 1;
        private Dictionary<string, string> searchCriteria = null;
        private const ItemType pageItemType = ItemType.Games;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                searchCriteria = CommonManager.GetSearchByDictionary();
                GetSlideshowDetails();

                #region Sort By
                var _count = Request.QueryString["count"];
                if (_count != null)
                {
                    currentViewPerPage = Convert.ToInt32(_count);
                }

                var _sortBy = Request.QueryString["sortby"];
                if (_sortBy != null)
                {
                    sortBy = _sortBy;
                }

                var _asc = Request.QueryString["asc"];
                if (_asc != null)
                {
                    ascending = Convert.ToBoolean(_asc);
                }
                #endregion

                #region Search By
                var _priceRange = Request.QueryString["priceRange"];
                if (_priceRange != null)
                {
                    searchCriteria[CommonManager.GetPriceRangeCriterionName()] = _priceRange;
                }

                var _year = Request.QueryString["year"];
                if (_year != null)
                {
                    searchCriteria[CommonManager.GetYearCriterionName()] = _year;
                }

                var _genre = Request.QueryString["genre"];
                if (_genre != null)
                {
                    searchCriteria[CommonManager.GetGenreCriterionName()] = _genre;
                }

                var _bestSeller = Request.QueryString["bestSeller"];
                if (_bestSeller != null)
                {
                    searchCriteria[CommonManager.GetBestSellerCriterionName()] = _bestSeller;
                }

                var _onSale = Request.QueryString["onSale"];
                if (_onSale != null)
                {
                    searchCriteria[CommonManager.GetOnSaleCriterionName()] = _onSale;
                }
                #endregion

                var _page = Request.QueryString["page"];
                if (_page != null)
                {
                    currentPage = Convert.ToInt32(_page);
                }

                if (ascending)
                {
                    anchorAsc.Attributes["style"] = "color:#33cc99;font-size:11px;";
                    anchorDesc.Attributes["style"] = "color:#999;font-size:11px;";
                }
                else
                {
                    anchorAsc.Attributes["style"] = "color:#999;font-size:11px;";
                    anchorDesc.Attributes["style"] = "color:#33cc99;font-size:11px;";
                }

                AddSortByOptions();
                AddSearchByOptions();

                GetListDetails();
            }
        }

        private void GetSlideshowDetails()
        {
            var slideShowDetails = new ItemsModel().GetSlideShowDetails(pageItemType);
            foreach (var item in slideShowDetails)
            {
                var itemDivControl = HTMLControlsManager.GetDivTag(new[] { "item" });
                itemSlideShow.Controls.Add(itemDivControl);
                {
                    var itemAnchor = HTMLControlsManager.GetAnchorTag("#");
                    itemDivControl.Controls.Add(itemAnchor);
                    {
                        var altImage = HTMLControlsManager.GetImageTag(item.location, null, "Games_banner");
                        itemAnchor.Controls.Add(altImage);
                    }

                    var catDivControl = HTMLControlsManager.GetDivTag(new[] { "cat-img-title", "cat-bg", "cat-box" });
                    itemDivControl.Controls.Add(catDivControl);
                    {
                        var headingControl = HTMLControlsManager.GetCustomTag("h2", new[] { "cat-heading" }, item.title);
                        catDivControl.Controls.Add(headingControl);
                    }
                }
            }
        }

        private void GetListDetails()
        {
            var gridDetails = new ItemsModel().GetItemDetails(pageItemType, EnumsManager.GetSortByOption(sortBy, ascending), currentViewPerPage, (currentPage - 1) * currentViewPerPage, searchCriteria);
            foreach (var item in gridDetails)
            {
                var liControl = HTMLControlsManager.GetCustomTag("li", new[] { "item odd" });
                productsList.Controls.Add(liControl);
                {
                    var prodImageDivControl = ItemHtmlManager.Get_ProductImage_List_DivControl(item);
                    liControl.Controls.Add(prodImageDivControl);

                    var prodDetailsDivControl = ItemHtmlManager.Get_ProductDetails_List_DivControl(item);
                    liControl.Controls.Add(prodDetailsDivControl);
                }
            }
        }

        #region Sort By

        private void AddSortByOptions()
        {
            AddViewPerPageList();
            AddSortByList();
            AddPagination();
        }

        private void AddPagination()
        {
            var itemCount = new ItemsModel().GetItemsCount(pageItemType);
            var noOfPages = Convert.ToInt32(Math.Ceiling(((double)itemCount / (double)currentViewPerPage)));
            var displayingPages = GetDisplayingPages(currentPage, noOfPages);

            HtmlGenericControl itemLiControl;
            HtmlAnchor anchorControl;

            itemLiControl = HTMLControlsManager.GetCustomTag("li", null);
            paginationCtrl.Controls.Add(itemLiControl);
            {
                var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('page', '" + (currentPage - 1) + "');" } };
                anchorControl = HTMLControlsManager.GetAnchorTag("#", null, "\u00AB", null, null, addAtt);
                itemLiControl.Controls.Add(anchorControl);
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

            itemLiControl = HTMLControlsManager.GetCustomTag("li", null);
            paginationCtrl.Controls.Add(itemLiControl);
            {
                var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('page', '" + (currentPage + 1) + "');" } };
                anchorControl = HTMLControlsManager.GetAnchorTag("#", null, "\u00BB", null, null, addAtt);
                itemLiControl.Controls.Add(anchorControl);
            }
        }

        private List<int> GetDisplayingPages(int currentPage, int noOfPages)
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

        private void AddViewPerPageList()
        {
            viewPerPage.Controls.Add(CommonHtmlManager.GetItemsPerPageList(CommonManager.GetValuesPerPage(), currentViewPerPage));
        }

        private void AddSortByList()
        {
            sortByList.Controls.Add(CommonHtmlManager.GetSortValuesList(CommonManager.GetSortByOptions(), sortBy));
        }

        #endregion

        #region Search By

        private void AddSearchByOptions()
        {
            GetPriceSearchByOptions();
            GetYearSearchByOptions();
            GetGenreSearchByOptions();
            GetSalesSearchByOptions();
        }

        private void GetPriceSearchByOptions()
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
                    var spanPrice1 = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, "Clear All");
                    anchorCtrl.Controls.Add(spanPrice1);

                }
            }
        }

        private void GetYearSearchByOptions()
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

        private void GetGenreSearchByOptions()
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

        private void GetSalesSearchByOptions()
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
    }
}