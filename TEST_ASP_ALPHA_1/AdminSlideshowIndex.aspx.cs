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
    public partial class AdminSlideshowIndex : System.Web.UI.Page
    {
        public List<SlideShowObj> slideshowDetails { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        public void BindData(bool populateData = true, string search = null)
        {
            Func<string, bool> checkContains = s => s != null && s.Contains("");

            if (populateData)
                slideshowDetails = new IndexModel().GetSlideShowDetails();
            if (search != null)
            {
                slideshowDetails = slideshowDetails.FindAll(rec => rec.title.Contains(search));
            }
            IndexSlideshowGrid.DataSource = slideshowDetails;
            IndexSlideshowGrid.DataBind();
        }

        #region Edit

        protected void lnkEdit_Click(object sender, GridViewEditEventArgs e)
        {
            IndexSlideshowGrid.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void IndexSlideshowGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            IndexSlideshowGrid.EditIndex = -1;
            BindData();
        }

        protected void IndexSlideshowGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((Label)IndexSlideshowGrid.Rows[e.RowIndex].FindControl("lblId")).Text);
                string title = ((TextBox)IndexSlideshowGrid.Rows[e.RowIndex].FindControl("txtEditTitle")).Text;
                string location = ((TextBox)IndexSlideshowGrid.Rows[e.RowIndex].FindControl("txtEditLocation")).Text;

                new IndexModel().UpdateSlideshowItem(new Common.SlideShowObj { id = id, location = location, title = title });
                
                regSuccessBox.Visible = true;
                regSuccessMsg.InnerText = "Successfully Updated the slideshow item!";
                regErrorBox.Visible = false;

                IndexSlideshowGrid.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                regErrMsg.InnerText = ex.Message;
                regSuccessBox.Visible = false;
                regErrorBox.Visible = true;
            }
        }

        #endregion

        #region Add

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var title = ((TextBox)IndexSlideshowGrid.FooterRow.FindControl("txtNewTitle")).Text;
                var location = ((TextBox)IndexSlideshowGrid.FooterRow.FindControl("txtNewLocation")).Text;

                new IndexModel().AddSlideshowItem(new Common.SlideShowObj { location = location, title = title });

                regSuccessBox.Visible = true;
                regSuccessMsg.InnerText = "Successfully Added the slideshow item!";
                regErrorBox.Visible = false;

                IndexSlideshowGrid.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                regErrMsg.InnerText = ex.Message;
                regSuccessBox.Visible = false;
                regErrorBox.Visible = true;
            }
        }

        #endregion

        #region Delete

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkRemove = (LinkButton)sender;
                int id = Convert.ToInt32(lnkRemove.CommandArgument);

                new IndexModel().DeleteSlideshowItem(new Common.SlideShowObj { id = id });

                regSuccessBox.Visible = true;
                regSuccessMsg.InnerText = "Successfully Deleted the slideshow item!";
                regErrorBox.Visible = false;

                IndexSlideshowGrid.EditIndex = -1;
                BindData();
            }
            catch (Exception ex)
            {
                regErrMsg.InnerText = ex.Message;
                regSuccessBox.Visible = false;
                regErrorBox.Visible = true;
            }
        }

        #endregion

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string Search(string criteria)
        {
            try
            {
                return criteria;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}