using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class BrowseTvSeriesGrid : System.Web.UI.Page
    {
        #region Private Variables
        private int currentViewPerPage = CommonManager.GetDefaultValuePerPage();
        private string sortBy = CommonManager.GetDefaultSortByOption();
        private bool ascending = true;
        private int currentPage = 1;
        private Dictionary<string, string> searchCriteria = null;
        private const ItemType pageItemType = ItemType.TvSeries;
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

                GetGridDetails();
            }
        }

        private void GetSlideshowDetails()
        {
            var slideShowDetails = new ItemsModel().GetSlideShowDetails(pageItemType);
            foreach (var item in slideShowDetails)
            {
                var itemDivControl = CommonHtmlManager.GetItemsPageSlideShowHtml(item.location, item.title);
                itemSlideShow.Controls.Add(itemDivControl);
            }
        }

        private void GetGridDetails()
        {
            var gridDetails = new ItemsModel().GetItemDetails(pageItemType, EnumsManager.GetSortByOption(sortBy, ascending), currentViewPerPage, (currentPage - 1) * currentViewPerPage, searchCriteria);
            foreach (var item in gridDetails)
            {
                var liControl = HTMLControlsManager.GetCustomTag("li", new[] { "item col-lg-4", "col-md-3", "col-sm-4", "col-xs-12" });
                gridItemsList.Controls.Add(liControl);
                {
                    var itemInnerDivControl = ItemHtmlManager.Get_ItemInner_Grid_DivControl(item);
                    liControl.Controls.Add(itemInnerDivControl);

                }
            }
        }


        #region Sort By

        private void AddSortByOptions()
        {
            CommonHtmlManager.AddViewPerPageList(currentViewPerPage, ref this.viewPerPage);
            CommonHtmlManager.AddSortByList(sortBy, ref this.sortByList);
            CommonHtmlManager.AddPagination(pageItemType, currentPage, currentViewPerPage, ref this.paginationCtrl, searchCriteria);
        }

        #endregion

        #region Search By

        private void AddSearchByOptions()
        {
            CommonHtmlManager.GetPriceSearchByOptions(pageItemType, searchCriteria, ref this.priceRangeSearch);
            CommonHtmlManager.GetYearSearchByOptions(pageItemType, searchCriteria, ref this.yearSearch);
            CommonHtmlManager.GetGenreSearchByOptions(pageItemType, searchCriteria, ref this.genreSearch);
            CommonHtmlManager.GetSalesSearchByOptions(pageItemType, searchCriteria, ref this.salesSearch);
        }

        #endregion
    }
}