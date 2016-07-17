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
        #region Set

        public void AddIndexSlideshowItem(SlideShowObj item)
        {
            try
            {
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"INSERT INTO index_slideshowpictures (title, location) 
                                    VALUES (@title, @location)";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Parameters.AddWithValue("@title", item.title);
                        com.Parameters.AddWithValue("@location", item.location);

                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSlideshowItem(SlideShowObj item)
        {
            try
            {
                var slideshowItem = GetSlideShowDetails(Convert.ToInt32(item.id));
                if (slideshowItem.Count > 0)
                {
                    using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                    {
                        string sqlString = @"UPDATE index_slideshowpictures SET title=@title, location=@location WHERE id=@id";

                        using (MySqlCommand com = new MySqlCommand(sqlString, con))
                        {
                            com.Parameters.AddWithValue("@title", item.title);
                            com.Parameters.AddWithValue("@location", item.location);
                            com.Parameters.AddWithValue("@id", Convert.ToInt32(item.id));

                            com.ExecuteNonQuery();
                        }
                    }
                }
                else
                    throw new Exception("slideshow item does not exist!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteSlideshowItem(int id)
        {
            try
            {
                var slideshowItem = GetSlideShowDetails(Convert.ToInt32(id));
                if (slideshowItem.Count > 0)
                {
                    using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                    {
                        string sqlString = @"DELETE FROM index_slideshowpictures WHERE id=@id";

                        using (MySqlCommand com = new MySqlCommand(sqlString, con))
                        {
                            com.Parameters.AddWithValue("@id", id);

                            com.ExecuteNonQuery();
                        }
                    }
                }
                else
                    throw new Exception("slideshow item does not exist!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get

        public List<SlideShowObj> GetSlideShowDetails(int id = -1)
        {
            List<SlideShowObj> returnList = new List<SlideShowObj>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                var sqlString = "select * from index_slideshowpictures ";
                if (id > -1)
                    sqlString += "where id = " + id;

                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(new SlideShowObj
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            location = dr["location"].ToString(),
                            title = dr["title"].ToString()
                        });
                    }
                }
            }
            return returnList;
        }

        public List<ItemObject> GetBestSellersDetails()
        {
            List<ItemObject> returnList = new List<ItemObject>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                using (MySqlCommand com = new MySqlCommand("SELECT * FROM items where best_seller IS TRUE;", con))
                {
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(new ItemsModel().GetInfoAddedItemObject(dr));
                    }
                }
            }
            return returnList;
        }

        public List<ItemObject> GetGiftItemsDetails()
        {
            List<ItemObject> returnList = new List<ItemObject>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                using (MySqlCommand com = new MySqlCommand("SELECT * FROM items where type = 'Gifts';", con))
                {
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(new ItemsModel().GetInfoAddedItemObject(dr));
                    }
                }
            }
            return returnList;
        }

        #endregion

    }

}