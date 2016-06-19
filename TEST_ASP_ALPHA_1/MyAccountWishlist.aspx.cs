using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class MyAccountWishlist : System.Web.UI.Page
    {
        AESManager aesMgr;
        EmailManager emailMgr;
        CustWishlistModel custWishListMgr;

        private int currentViewPerPage = CommonManager.GetDefaultValuePerPage();
        private string sortBy = CommonManager.GetDefaultSortByOption();
        private bool ascending = true;
        private int currentPage = 1;
        private Dictionary<string, string> searchCriteria = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            searchCriteria = CommonManager.GetSearchByDictionary();
            aesMgr = new AESManager();
            emailMgr = new EmailManager();
            custWishListMgr = new CustWishlistModel();

            if (!IsPostBack)
            {
                if (Session[CommonManager.GetCustEmailSessionName()] == null)
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    Response.Redirect("Login.aspx");
                }
            }

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
            #endregion

            GetWishList();
            AddSortByOptions();

            GetGridDetails();
        }

        private void AddSortByOptions()
        {
            CommonHtmlManager.AddItemViewPerPageList(currentViewPerPage, ref this.viewPerPage);
            CommonHtmlManager.AddItemSortByList(sortBy, ref this.sortByList);
            CommonHtmlManager.AddItemPagination(ItemType.All, currentPage, currentViewPerPage, ref this.paginationCtrl, searchCriteria);
        }

        private void GetWishList()
        {
            var items = custWishListMgr.GetWishlistItems(Convert.ToInt32(Session[CommonManager.GetCustIdSessionName()]));
            searchCriteria[CommonManager.GetIdCriterionName()] = CommonManager.GetSearchByIdCriterion(items);
        }

        private void GetGridDetails()
        {
            var gridDetails = new ItemsModel().GetItemDetails(ItemType.All, EnumsManager.GetSortByOption(sortBy, ascending), currentViewPerPage, (currentPage - 1) * currentViewPerPage, searchCriteria);
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
    }
}