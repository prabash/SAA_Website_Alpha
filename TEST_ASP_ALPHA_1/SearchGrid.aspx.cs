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
    public partial class SearchGrid : System.Web.UI.Page
    {
        #region Private Variables
        private int currentViewPerPage = CommonManager.GetDefaultValuePerPage();
        private string sortBy = CommonManager.GetDefaultSortByOption();
        private bool ascending = true;
        private int currentPage = 1;
        private Dictionary<string, string> searchCriteria = null;
        private const ItemType pageItemType = ItemType.All;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            searchCriteria = CommonManager.GetSearchByDictionary();

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

            var _title = Request.QueryString["title"];
            if (_title != null)
            {
                searchCriteria[CommonManager.GetTitleCriterionName()] = _title;
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

            GetGridDetails();
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
            CommonHtmlManager.AddItemViewPerPageList(currentViewPerPage, ref this.viewPerPage);
            CommonHtmlManager.AddItemSortByList(sortBy, ref this.sortByList);
            CommonHtmlManager.AddItemPagination(pageItemType, currentPage, currentViewPerPage, ref this.paginationCtrl, searchCriteria);
        }

        #endregion
        
        #region Subscription

        protected void btnSubscribe_Click(object sender, EventArgs e)
        {
            SubscribeNewsletter();
        }

        private void SubscribeNewsletter()
        {
            try
            {
                var email = dfnEmailAdd.Value;
                new CustCommentsModel().SaveSubscription(email);
                successBox.Visible = true;
            }
            catch (Exception)
            {
                errorBox.Visible = true;
            }
        }

        #endregion
    }
}