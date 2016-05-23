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
            MySqlConnection con = ConnectionManager.GetOpenConnection();

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

        public List<ItemObject> GetItemDetails(ItemType type, ItemSortBy sortBy, int limit, int currentPage = 0, Dictionary<string, string> searchCriteria = null)
        {
            List<ItemObject> returnList = new List<ItemObject>();
            MySqlConnection con = ConnectionManager.GetOpenConnection();

            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT * FROM items ");
            
           
            using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
            {
                AppendWhereConditionByItemType(sqlString, type);
                if (searchCriteria != null)
                    AppendSearchCriteriatoWhereCondition(sqlString, searchCriteria);
                
                sqlString.Append(" ");

                switch (sortBy)
                {
                    case ItemSortBy.NameAsc:
                        sqlString.Append("ORDER BY title ASC");
                        break;
                    case ItemSortBy.NameDesc:
                        sqlString.Append("ORDER BY title DESC");
                        break;
                    case ItemSortBy.YearAsc:
                        sqlString.Append("ORDER BY year ASC");
                        break;
                    case ItemSortBy.YearDesc:
                        sqlString.Append("ORDER BY year DESC");
                        break;
                    case ItemSortBy.PriceAsc:
                        sqlString.Append("ORDER BY current_price ASC");
                        break;
                    case ItemSortBy.PriceDesc:
                        sqlString.Append("ORDER BY current_price DESC");
                        break;
                    case ItemSortBy.OnSaleAsc:
                        sqlString.Append("ORDER BY on_sale ASC");
                        break;
                    case ItemSortBy.OnSaleDesc:
                        sqlString.Append("ORDER BY on_sale DESC");
                        break;
                    case ItemSortBy.BestSellerAsc:
                        sqlString.Append("ORDER BY best_seller ASC");
                        break;
                    case ItemSortBy.BestSellerDesc:
                        sqlString.Append("ORDER BY best_seller DESC");
                        break;
                    default:
                        sqlString.Append("ORDER BY title ASC");
                        break;
                }

                sqlString.Append(" LIMIT " + currentPage + "," + limit);
                com.CommandText = sqlString.ToString();

                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    returnList.Add(GetInfoAddedItemObject(dr));
                }
            }
            return returnList;
        }

        public int GetItemsCount(ItemType type)
        {
            int count = 0;
            MySqlConnection con = ConnectionManager.GetOpenConnection();

            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT COUNT(1) AS count FROM items ");


            using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
            {
                AppendWhereConditionByItemType(sqlString, type);
                com.CommandText = sqlString.ToString();

                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    count = Convert.ToInt32(dr["count"]);
                }
            }

            return count;
        }

        public ItemObject GetItemDetailById(int id)
        {
            var returnObj = new ItemObject();
            MySqlConnection con = ConnectionManager.GetOpenConnection();

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
                type = dr["type"].ToString(),
                dateAdded = Convert.ToDateTime(dr["date_added"]),
                genre = dr["genre"].ToString()
            };
        }

        #region Search Criteria

        public Dictionary<string, int> GetPriceRangesWithCounts(ItemType type)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            MySqlConnection con = ConnectionManager.GetOpenConnection();

            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT t.current_price as price_range, count(*) as count \n");
            sqlString.Append("FROM ( \n");
            sqlString.Append("SELECT case \n");
            sqlString.Append("WHEN current_price BETWEEN 0 and 500 THEN '0|500' \n");
            sqlString.Append("WHEN current_price BETWEEN 501 and 1000 THEN '501|1000' \n");
            sqlString.Append("ELSE '1000|above' END AS current_price \n");
            sqlString.Append("FROM items \n");


            using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
            {
                AppendWhereConditionByItemType(sqlString, type);
                sqlString.Append(") t \n");
                sqlString.Append("GROUP BY t.current_price");

                com.CommandText = sqlString.ToString();

                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var range = dr["price_range"].ToString();
                    var count = Convert.ToInt32(dr["count"]);
                    returnList.Add(range, count);
                }
            }
            return returnList;
        }

        public Dictionary<string, int> GetYearsWithCounts(ItemType type)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            MySqlConnection con = ConnectionManager.GetOpenConnection();

            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT t.year, count(*) as count \n");
            sqlString.Append("FROM items t \n");


            using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
            {
                AppendWhereConditionByItemType(sqlString, type);
                sqlString.Append("GROUP BY t.year");

                com.CommandText = sqlString.ToString();

                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var range = dr["year"].ToString();
                    var count = Convert.ToInt32(dr["count"]);
                    returnList.Add(range, count);
                }
            }
            return returnList;
        }

        public Dictionary<string, int> GetGenresWithCounts(ItemType type)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            MySqlConnection con = ConnectionManager.GetOpenConnection();
            List<string> genresList = GetDistinctGenres(type);

            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT t.genre, count(*) as count \n");
            sqlString.Append("FROM ( \n");
            
            using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
            {
                for (int i = 0; i < genresList.Count; i++)
                {
                    sqlString.Append("(SELECT CASE \n");
                    sqlString.Append("WHEN genre like '%" + genresList[i] + "%' THEN '" + genresList[i] + "' \n");
                    sqlString.Append("END AS genre \n");
                    sqlString.Append("FROM items \n");
                    AppendWhereConditionByItemType(sqlString, type);
                    sqlString.Append(") \n");

                    if (i < genresList.Count - 1)
                    {
                        sqlString.Append("UNION ALL \n");
                    }
                }

                
                sqlString.Append(") t \n");
                sqlString.Append("WHERE t.genre != ''");
                sqlString.Append("GROUP BY t.genre");

                com.CommandText = sqlString.ToString();

                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var range = dr["genre"].ToString();
                    var count = Convert.ToInt32(dr["count"]);
                    returnList.Add(range, count);
                }
            }
            return returnList;
        }

        private List<string> GetDistinctGenres(ItemType type)
        {
            List<string> distinctGenres = new List<string>();
            List<string> genresList = new List<string>();
            MySqlConnection con = ConnectionManager.GetOpenConnection();

            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT distinct t.genre\n");
            sqlString.Append("FROM items t \n");


            using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
            {
                AppendWhereConditionByItemType(sqlString, type);

                com.CommandText = sqlString.ToString();

                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var genre = dr["genre"].ToString();
                    distinctGenres.Add(genre);
                }
            }

            foreach (var item in distinctGenres)
            {
                if (item.Contains(','))
                {
                    var splittedList = item.Split(',').ToList();
                    foreach (var genre in splittedList)
                    {
                        if (!genresList.Contains(genre))
                            genresList.Add(genre);
                    }
                }
                else
                    if (!genresList.Contains(item))
                        genresList.Add(item);
                
            }
            return genresList;
        }
        #endregion

        #region String SQL Operations

        private void AppendWhereConditionByItemType(StringBuilder sqlString, ItemType type)
        {
            switch (type)
            {
                case ItemType.Games:
                    sqlString.Append("WHERE type = 'Games'");
                    break;
                case ItemType.TvSeries:
                    sqlString.Append("WHERE type = 'TvSeries'");
                    break;
                case ItemType.Movies:
                    sqlString.Append("WHERE type = 'Movies'");
                    break;
                case ItemType.Gifts:
                    sqlString.Append("WHERE type = 'Gifts'");
                    break;
                case ItemType.Electronics:
                    sqlString.Append("WHERE type = 'Electronics'");
                    break;
                default:
                    break;
            }
        }

        private void AppendSearchCriteriatoWhereCondition(StringBuilder sqlString, Dictionary<string, string> searchCriteria)
        {
            if (searchCriteria.Count() > 0)
            {
                foreach (var criterion in searchCriteria)
                {
                    if (!String.IsNullOrEmpty(criterion.Key))
                    {
                        if (criterion.Key == CommonManager.GetPriceRangeCriterionName())
                        {
                            if (!String.IsNullOrEmpty(criterion.Value))
                            {
                                var priceRange = criterion.Value.Split('|');
                                sqlString.Append("AND current_price BETWEEN " + priceRange[0] + " AND " + priceRange[1] + " ");
                            }
                        }
                        else if (criterion.Key == CommonManager.GetYearCriterionName())
                        {
                            if (!String.IsNullOrEmpty(criterion.Value))
                            {
                                sqlString.Append("AND year = " + criterion.Value + " ");
                            }
                        }
                        else if (criterion.Key == CommonManager.GetGenreCriterionName())
                        {
                            if (!String.IsNullOrEmpty(criterion.Value))
                            {
                                sqlString.Append("AND genre like '%" + criterion.Value + "%'" + " ");
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}