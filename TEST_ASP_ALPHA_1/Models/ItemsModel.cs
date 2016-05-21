using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class ItemsModel
    {
        public List<SlideShowObj> GetSlideShowDetails(ItemType type)
        {
            List<SlideShowObj> returnList = new List<SlideShowObj>();
            MySqlConnection con = ConnectionManager.GetConnection();
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string sqlString = "select * from items_slideshowpictures where type = @type";
            
            using (MySqlCommand com = new MySqlCommand(sqlString, con))
            {
                switch (type)
                {
                    case ItemType.Games:
                        com.Parameters.AddWithValue("@type", "Games");
                        break;
                    case ItemType.TvSeries:
                        com.Parameters.AddWithValue("@type", "TvSeries");
                        break;
                    case ItemType.Movies:
                        com.Parameters.AddWithValue("@type", "Movies");
                        break;
                    case ItemType.Gifts:
                        com.Parameters.AddWithValue("@type", "Gifts");
                        break;
                    case ItemType.Electronics:
                        com.Parameters.AddWithValue("@type", "Electronics");
                        break;
                    default:
                        com.Parameters.AddWithValue("@type", "");
                        break;
                }

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

        public List<ItemObject> GetItemDetails(ItemType type, ItemSortBy sortBy, int limit, bool getRelatedItems = false, int currentItemId = 0)
        {
            List<ItemObject> returnList = new List<ItemObject>();
            MySqlConnection con = ConnectionManager.GetConnection();
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT * FROM items ");
            sqlString.Append("WHERE type = @type ");
            if (getRelatedItems)
            {
                sqlString.Append("AND id > " + currentItemId + " ");
            }
            sqlString.Append("ORDER BY @orderBy ");
            sqlString.Append("LIMIT @limit ");

            //string sqlString = "SELECT * FROM items WHERE type = @type ORDER BY @orderBy LIMIT @limit";
            if (getRelatedItems)
            {

            }
            using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
            {
                switch (type)
                {
                    case ItemType.Games:
                        com.Parameters.AddWithValue("@type", "Games");
                        break;
                    case ItemType.TvSeries:
                        com.Parameters.AddWithValue("@type", "TvSeries");
                        break;
                    case ItemType.Movies:
                        com.Parameters.AddWithValue("@type", "Movies");
                        break;
                    case ItemType.Gifts:
                        com.Parameters.AddWithValue("@type", "Gifts");
                        break;
                    case ItemType.Electronics:
                        com.Parameters.AddWithValue("@type", "Electronics");
                        break;
                    default:
                        com.Parameters.AddWithValue("@type", "");
                        break;
                }

                switch (sortBy)
                {
                    case ItemSortBy.NameAsc:
                        com.Parameters.AddWithValue("@orderBy", "title ASC");
                        break;
                    case ItemSortBy.NameDesc:
                        com.Parameters.AddWithValue("@orderBy", "title DESC");
                        break;
                    case ItemSortBy.YearAsc:
                        com.Parameters.AddWithValue("@orderBy", "year ASC");
                        break;
                    case ItemSortBy.YearDesc:
                        com.Parameters.AddWithValue("@orderBy", "year DESC");
                        break;
                    case ItemSortBy.PriceAsc:
                        com.Parameters.AddWithValue("@orderBy", "current_price ASC");
                        break;
                    case ItemSortBy.PriceDesc:
                        com.Parameters.AddWithValue("@orderBy", "current_price DESC");
                        break;
                    case ItemSortBy.OnSaleAsc:
                        com.Parameters.AddWithValue("@orderBy", "on_sale ASC");
                        break;
                    case ItemSortBy.OnSaleDesc:
                        com.Parameters.AddWithValue("@orderBy", "on_sale DESC");
                        break;
                    case ItemSortBy.BestSellerAsc:
                        com.Parameters.AddWithValue("@orderBy", "best_seller ASC");
                        break;
                    case ItemSortBy.BestSellerDesc:
                        com.Parameters.AddWithValue("@orderBy", "best_seller DESC");
                        break;
                    default:
                        com.Parameters.AddWithValue("@orderBy", "title ASC");
                        break;
                }

                com.Parameters.AddWithValue("@limit", limit);


                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    returnList.Add(GetInfoAddedItemObject(dr));
                }
            }
            return returnList;
        }

        public ItemObject GetItemDetailById(int id)
        {
            var returnObj = new ItemObject();
            MySqlConnection con = ConnectionManager.GetConnection();
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string sqlString = "SELECT * FROM items WHERE id = @id";
            using (MySqlCommand com = new MySqlCommand(sqlString, con))
            {
                com.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    returnObj = GetInfoAddedItemObject(dr);
                }
            }
            return returnObj;
        }

        public ItemObject GetInfoAddedItemObject(MySqlDataReader dr)
        {
            return new ItemObject
            {
                Id = Convert.ToInt32(dr["id"]),
                title = dr["title"].ToString(),
                defaultLocation = dr["location_default"].ToString(),
                altLocation = dr["location_alt"].ToString(),
                regularPrice = dr["regular_price"] == null ? 0 : Convert.ToDouble(dr["regular_price"]),
                currentPrice = dr["current_price"] == null ? 0 : Convert.ToDouble(dr["current_price"]),
                rating = dr["rating"] == null ? 0 : Convert.ToDouble(dr["rating"]),
                onSale = dr["on_sale"] == null ? false : Convert.ToBoolean(dr["on_sale"]),
                year = dr["year"].ToString(),
                description = dr["description"].ToString(),
                overview = dr["overview"].ToString(),
                type = dr["type"].ToString()
            };
        }
    }
}