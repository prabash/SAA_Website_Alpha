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
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[CommonManager.Session_GetAdminUsernameSessionName()] == null)
                {
                    throw new HttpException(403, "Forbidden");
                }

                GetSlideshowDetails();
            }
        }


        private void GetSlideshowDetails()
        {
            var slideShowDetails = new IndexModel().GetSlideShowDetails();
            foreach (var item in slideShowDetails)
            {
                HtmlGenericControl liControl = new HtmlGenericControl();
                liControl.Attributes["class"] = "slide";
                liControl.TagName = "li";
                slideShowList.Controls.Add(liControl);

                HtmlGenericControl paraControl = new HtmlGenericControl();
                paraControl.TagName = "p";
                liControl.Controls.Add(paraControl);
                HtmlAnchor anchorControl = new HtmlAnchor();
                anchorControl.HRef = "#";
                paraControl.Controls.Add(anchorControl);
                HtmlImage imageControl = new HtmlImage();
                imageControl.Src = item.location;
                anchorControl.Controls.Add(imageControl);

                HtmlGenericControl divControl = new HtmlGenericControl();
                divControl.TagName = "div";
                divControl.Attributes["class"] = "caption light1 top-right";
                liControl.Controls.Add(divControl);
                HtmlGenericControl innerDivControl = new HtmlGenericControl();
                innerDivControl.TagName = "div";
                innerDivControl.Attributes["class"] = "caption-inner";
                divControl.Controls.Add(innerDivControl);
                HtmlGenericControl titleParaControl = new HtmlGenericControl();
                titleParaControl.TagName = "p";
                titleParaControl.Attributes["class"] = "heading";
                titleParaControl.InnerText = item.title;
                innerDivControl.Controls.Add(titleParaControl);
            }
        }

    }
}