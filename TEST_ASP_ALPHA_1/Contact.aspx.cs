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
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                shopAddress.InnerHtml = CommonManager.GetAddress();
                shopTelephone.InnerHtml = CommonManager.GetTelephoneNo();

            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                var name = dfnFirstname.Value;
                var email = dfnEmail.Value;
                var telephone = dfnTelephone.Value;
                var subject = dfnSubject.Value;
                var comment = dfnComment.Value;
                new CustCommentsModel().SaveCustComment(name, email, telephone, subject, comment);
                
                ClearFields();
                successBox.Visible = true;
            }
            catch (Exception)
            {
                errorBox.Visible = true;
            }
        }

        private void ClearFields()
        {
            dfnFirstname.Value = String.Empty;
            dfnEmail.Value = String.Empty;
            dfnTelephone.Value = String.Empty;
            dfnSubject.Value = String.Empty;
            dfnComment.Value = String.Empty;
        }
    }
}