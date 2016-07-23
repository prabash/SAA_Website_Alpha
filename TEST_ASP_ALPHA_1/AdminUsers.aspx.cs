﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TEST_ASP_ALPHA_1.Common;
using TEST_ASP_ALPHA_1.Models;

namespace TEST_ASP_ALPHA_1
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string GetLoggedInToday()
        {
            try
            {
                var loggedCountToday = new CounterModel().GetCounterValue(CommonManager.Counter_GetLoggedInToday_Name());
                return loggedCountToday.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetTotalActiveUsers()
        {
            try
            {
                var totalActive = new CustomerModel().Count_GetTotalActiveUsersCount();
                return totalActive.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}