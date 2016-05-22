using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class IndexModel
    {
        public List<SlideShowObj> GetSlideShowDetails()
        {
            List<SlideShowObj> returnList = new List<SlideShowObj>();
            MySqlConnection con = ConnectionManager.GetOpenConnection();

            using (MySqlCommand com = new MySqlCommand("select * from index_slideshowpictures;", con))
            {
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    returnList.Add(new SlideShowObj
                    {
                        location = dr["location"].ToString(),
                        title = dr["title"].ToString()
                    });
                }
            }
            return returnList;
        }

        public List<ItemObject> GetBestSellersDetails()
        {
            List<ItemObject> returnList = new List<ItemObject>();
            MySqlConnection con = ConnectionManager.GetOpenConnection();

            using (MySqlCommand com = new MySqlCommand("SELECT * FROM items where best_seller IS TRUE;", con))
            {
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    returnList.Add(new ItemsModel().GetInfoAddedItemObject(dr));
                }
            }
            return returnList;
        }

        public List<ItemObject> GetGiftItemsDetails()
        {
            List<ItemObject> returnList = new List<ItemObject>();
            MySqlConnection con = ConnectionManager.GetOpenConnection();

            using (MySqlCommand com = new MySqlCommand("SELECT * FROM items where type = 'Gifts';", con))
            {
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    returnList.Add(new ItemsModel().GetInfoAddedItemObject(dr));
                }
            }
            return returnList;
        }

    }

}