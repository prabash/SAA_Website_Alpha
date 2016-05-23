﻿using System;
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
        private int currentViewPerPage = CommonManager.GetDefaultValuePerPage();
        private string sortBy = CommonManager.GetDefaultSortByOption();
        private bool ascending = true;
        private int currentPage = 1;
        private Dictionary<string, string> searchCriteria = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            searchCriteria = CommonManager.GetSearchByDictionary();
           
            if (!IsPostBack)
            {
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

                AddViewPerPageList();
                AddSortByList();
                AddPagination();

                GetPriceSearchByOptions();
                GetYearSearchByOptions();
                GetGenreSearchByOptions();

                GetListDetails();
            }
        }

        private void GetSlideshowDetails()
        {
            var slideShowDetails = new ItemsModel().GetSlideShowDetails(ItemType.Games);
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
            var gridDetails = new ItemsModel().GetItemDetails(ItemType.Games, EnumsManager.GetSortByOption(sortBy, ascending), currentViewPerPage, (currentPage - 1) * currentViewPerPage, searchCriteria);
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

        private void AddPagination()
        {
            var itemCount = new ItemsModel().GetItemsCount(ItemType.Games);
            var noOfPages = itemCount / currentViewPerPage;

            for (int i = 0; i <= noOfPages + 1; i++)
            {
                HtmlGenericControl itemLiControl;
                if (currentPage == i)
                    itemLiControl = HTMLControlsManager.GetCustomTag("li", new[] { "active" });
                else
                    itemLiControl = HTMLControlsManager.GetCustomTag("li", null);

                paginationCtrl.Controls.Add(itemLiControl);
                {
                    HtmlAnchor anchorControl = null;
                    if (i == 0)
                    {
                        var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('page', '" + (currentPage - 1) + "');" } };
                        anchorControl = HTMLControlsManager.GetAnchorTag("#", null, "\u00AB", null, null, addAtt);
                    }
                    if (i > 0 && i < noOfPages + 1)
                    {
                        var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('page', '" + i + "');" } };
                        anchorControl = HTMLControlsManager.GetAnchorTag("#", null, i.ToString(), null, null, addAtt);
                    }
                    if (i == noOfPages + 1)
                    {
                        var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('page', '" + (currentPage + 1) + "');" } };
                        anchorControl = HTMLControlsManager.GetAnchorTag("#", null, "\u00BB", null, null, addAtt);
                    }
                    itemLiControl.Controls.Add(anchorControl);
                }
            }
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

        private void GetPriceSearchByOptions()
        {
            var priceRanges = new ItemsModel().GetPriceRangesWithCounts(ItemType.Games);

            foreach (var item in priceRanges)
            {
                var liPriceRangeControl = HTMLControlsManager.GetCustomTag("li", null);
                priceRangeSearch.Controls.Add(liPriceRangeControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('priceRange', '" + item.Key + "');" } };
                    var anchorCtrl = HTMLControlsManager.GetAnchorTag("#", null, null, null, null, addAtt);
                    liPriceRangeControl.Controls.Add(anchorCtrl);
                    {
                        var priceRange = item.Key.Split('|');
                        var spanPrice1 = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, priceRange[0] + " LKR - ");
                        anchorCtrl.Controls.Add(spanPrice1);

                        var spanPrice2 = HTMLControlsManager.GetCustomTag("span", new[] { "price" }, priceRange[1] == "above" ? priceRange[1] : priceRange[1] + " LKR (" + item.Value + ")");
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
            var yearRanges = new ItemsModel().GetYearsWithCounts(ItemType.Games);

            foreach (var item in yearRanges)
            {
                var liYearRangeControl = HTMLControlsManager.GetCustomTag("li", null);
                yearSearch.Controls.Add(liYearRangeControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('year', '" + item.Key + "');" } };
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
            var genres = new ItemsModel().GetGenresWithCounts(ItemType.Games);

            foreach (var item in genres)
            {
                var liGenreControl = HTMLControlsManager.GetCustomTag("li", null);
                genreSearch.Controls.Add(liGenreControl);
                {
                    var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('genre', '" + item.Key + "');" } };
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

        #endregion
    }
}