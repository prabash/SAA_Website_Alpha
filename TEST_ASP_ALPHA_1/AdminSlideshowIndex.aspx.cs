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
        }
    }
}