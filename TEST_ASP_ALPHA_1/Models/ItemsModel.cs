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
        #region Set
        public void SaveItems(List<ItemObject> items)
        {
            MySqlTransaction transaction = null;
            try
            {
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    transaction = con.BeginTransaction();
                    string sqlString = @"INSERT INTO items (title, location_default, location_alt, regular_price, current_price, rating, on_sale, type, overview, description, best_seller, year, date_added, genre) 
                                    VALUES (@title, @defLocation, @altLocation, @regPrice, @curPrice, @rating, @onSale, @type, @overview, @description, @bestSeller, @year, @dateAdded, @genre)";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Transaction = transaction;
                        com.Parameters.AddWithValue("@title", "");
                        com.Parameters.AddWithValue("@defLocation", "");
                        com.Parameters.AddWithValue("@altLocation", "");
                        com.Parameters.AddWithValue("@regPrice", 0.00);
                        com.Parameters.AddWithValue("@curPrice", 0.00);
                        com.Parameters.AddWithValue("@rating", 0.00);
                        com.Parameters.AddWithValue("@onSale", 0);
                        com.Parameters.AddWithValue("@type", "");
                        com.Parameters.AddWithValue("@overview", "");
                        com.Parameters.AddWithValue("@description", "");
                        com.Parameters.AddWithValue("@bestSeller", false);
                        com.Parameters.AddWithValue("@year", "");
                        com.Parameters.AddWithValue("@dateAdded", DateTime.Now);
                        com.Parameters.AddWithValue("@genre", "");

                        foreach (var item in items)
                        {
                            com.Parameters["@title"].Value = item.title;
                            com.Parameters["@defLocation"].Value = item.defaultLocation;
                            com.Parameters["@altLocation"].Value = item.altLocation;
                            com.Parameters["@regPrice"].Value= item.regularPrice;
                            com.Parameters["@curPrice"].Value = item.currentPrice;
                            com.Parameters["@rating"].Value = item.rating;
                            com.Parameters["@onSale"].Value =Convert.ToInt32(item.onSale);
                            com.Parameters["@type"].Value = item.type;
                            com.Parameters["@overview"].Value = item.overview;
                            com.Parameters["@description"].Value = item.description;
                            com.Parameters["@bestSeller"].Value = item.bestSeller;
                            com.Parameters["@year"].Value = item.year;
                            com.Parameters["@dateAdded"].Value = DateTime.Now;
                            com.Parameters["@genre"].Value = item.genre;

                            com.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get

        #region General
        public List<SlideShowObj> GetSlideShowDetails(ItemType type)
        {
            List<SlideShowObj> returnList = new List<SlideShowObj>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
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
            }
            return returnList;
        }

        public List<ItemObject> GetItemDetails(ItemType type, ItemSortBy sortBy, int limit = 0, int currentPage = 0, Dictionary<string, string> searchCriteria = null)
        {
            List<ItemObject> returnList = new List<ItemObject>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
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

                    if (limit > 0)
                        sqlString.Append(" LIMIT " + currentPage + "," + limit);

                    com.CommandText = sqlString.ToString();

                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(GetInfoAddedItemObject(dr));
                    }
                }
            }
            return returnList;
        }

        public List<ItemObject> GetRelatedItems(ItemType type, List<int> itemIds)
        {
            List<ItemObject> relatedItems = new List<ItemObject>();
            Dictionary<string, string> searchCriteria = new Dictionary<string, string>();

            double lowestPrice = 0;
            double highestPrice = 0;

            Dictionary<string, int> candidateGenres = new Dictionary<string, int>();
            string selectedGenre = "";

            searchCriteria.Add(CommonManager.GetIdCriterionName(), CommonManager.GetSearchByIdCriterion(itemIds));
            var items = GetItemDetails(type, ItemSortBy.NameAsc, itemIds.Count, 0, searchCriteria);

            for (int i = 0; i < items.Count; i++)
            {
                if (i == 0)
                {
                    if (items.Count > 1)
                        lowestPrice = items[i].currentPrice;
                    highestPrice = items[i].currentPrice;
                }
                else
                {
                    if (lowestPrice > items[i].currentPrice)
                        lowestPrice = items[i].currentPrice;
                    else if (highestPrice < items[i].currentPrice)
                        highestPrice = items[i].currentPrice;
                }

                var genres = items[i].genre.Split(',');
                foreach (var genre in genres)
                {
                    if (!candidateGenres.ContainsKey(genre))
                        candidateGenres.Add(genre, 1);
                    else
                    {
                        var currentCount = candidateGenres[genre];
                        candidateGenres[genre] = currentCount + 1;
                    }
                }
            }

            var genreCounter = 0;
            foreach (var candGenre in candidateGenres)
            {
                int noOfItems = 0;
                if (genreCounter == 0)
                {
                    noOfItems = candGenre.Value;
                    selectedGenre = candGenre.Key;
                }
                else
                {
                    if (candGenre.Value > noOfItems)
                        selectedGenre = candGenre.Key;
                }
            }

            searchCriteria = new Dictionary<string, string>();
            if (lowestPrice == 0)
                searchCriteria.Add(CommonManager.GetPriceRangeCriterionName(), lowestPrice + "|" + (highestPrice + ((highestPrice + lowestPrice) / 2)));
            else
                searchCriteria.Add(CommonManager.GetPriceRangeCriterionName(), (lowestPrice - ((highestPrice + lowestPrice) / 2)) + "|" + (highestPrice + ((highestPrice + lowestPrice) / 2)));
            searchCriteria.Add(CommonManager.GetGenreCriterionName(), selectedGenre);
            relatedItems = GetItemDetails(type, ItemSortBy.NameAsc, 6, 0, searchCriteria);
            return relatedItems;
        }

        public int GetItemsCount(ItemType type, Dictionary<string, string> searchCriteria = null)
        {
            int count = 0;
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("SELECT COUNT(1) AS count FROM items ");

                using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
                {
                    AppendWhereConditionByItemType(sqlString, type);

                    if (searchCriteria != null)
                        AppendSearchCriteriatoWhereCondition(sqlString, searchCriteria);

                    com.CommandText = sqlString.ToString();

                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        count = Convert.ToInt32(dr["count"]);
                    }
                }
            }

            return count;
        }

        public ItemObject GetItemDetailById(int id)
        {
            var returnObj = new ItemObject();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
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
            }
            return returnObj;
        }

        public double GetItemDiscount()
        {
            return 0;
        }

        #endregion

        #region Search Criteria

        public Dictionary<string, int> GetPriceRangesWithCounts(ItemType type, Dictionary<string, string> searchCriteria = null)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
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
                    if (searchCriteria != null)
                        AppendSearchCriteriatoWhereCondition(sqlString, searchCriteria);

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
            }
            return returnList;
        }

        public Dictionary<string, int> GetYearsWithCounts(ItemType type, Dictionary<string, string> searchCriteria = null)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("SELECT t.year, count(*) as count \n");
                sqlString.Append("FROM items t \n");


                using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
                {
                    AppendWhereConditionByItemType(sqlString, type);
                    if (searchCriteria != null)
                        AppendSearchCriteriatoWhereCondition(sqlString, searchCriteria);

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
            }
            return returnList;
        }

        public Dictionary<string, int> GetGenresWithCounts(ItemType type, Dictionary<string, string> searchCriteria = null)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
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
                        if (searchCriteria != null)
                            AppendSearchCriteriatoWhereCondition(sqlString, searchCriteria);
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
            }
            return returnList;
        }

        private List<string> GetDistinctGenres(ItemType type, Dictionary<string, string> searchCriteria = null)
        {
            List<string> distinctGenres = new List<string>();
            List<string> genresList = new List<string>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("SELECT distinct t.genre\n");
                sqlString.Append("FROM items t \n");


                using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
                {
                    AppendWhereConditionByItemType(sqlString, type);
                    if (searchCriteria != null)
                        AppendSearchCriteriatoWhereCondition(sqlString, searchCriteria);

                    com.CommandText = sqlString.ToString();

                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var genre = dr["genre"].ToString();
                        distinctGenres.Add(genre);
                    }
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

        public Dictionary<string, int> GetBestSellersWithCounts(ItemType type, Dictionary<string, string> searchCriteria = null)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("SELECT t.best_seller, count(*) as count \n");
                sqlString.Append("FROM items t \n");


                using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
                {
                    AppendWhereConditionByItemType(sqlString, type);
                    if (searchCriteria != null)
                        AppendSearchCriteriatoWhereCondition(sqlString, searchCriteria);

                    sqlString.Append("AND t.best_seller = '1' \n");
                    sqlString.Append("GROUP BY t.best_seller");

                    com.CommandText = sqlString.ToString();

                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var range = CommonManager.GetBestSellerCriterionName();
                        var count = Convert.ToInt32(dr["count"]);
                        returnList.Add(range, count);
                    }
                }
            }
            return returnList;
        }

        public Dictionary<string, int> GetOnSaleWithCounts(ItemType type, Dictionary<string, string> searchCriteria = null)
        {
            Dictionary<string, int> returnList = new Dictionary<string, int>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("SELECT t.on_sale, count(*) as count \n");
                sqlString.Append("FROM items t \n");


                using (MySqlCommand com = new MySqlCommand(sqlString.ToString(), con))
                {
                    AppendWhereConditionByItemType(sqlString, type);
                    if (searchCriteria != null)
                        AppendSearchCriteriatoWhereCondition(sqlString, searchCriteria);
                    sqlString.Append("AND t.on_sale = '1' \n");
                    sqlString.Append("GROUP BY t.on_sale");

                    com.CommandText = sqlString.ToString();

                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var range = CommonManager.GetOnSaleCriterionName();
                        var count = Convert.ToInt32(dr["count"]);
                        returnList.Add(range, count);
                    }
                }
            }
            return returnList;
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
                case ItemType.All:
                    sqlString.Append("WHERE type like '%'");
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
                        else if (criterion.Key == CommonManager.GetBestSellerCriterionName())
                        {
                            if (!String.IsNullOrEmpty(criterion.Value))
                            {
                                sqlString.Append("AND best_seller = " + criterion.Value + " ");
                            }
                        }
                        else if (criterion.Key == CommonManager.GetOnSaleCriterionName())
                        {
                            if (!String.IsNullOrEmpty(criterion.Value))
                            {
                                sqlString.Append("AND on_sale = " + criterion.Value + " ");
                            }
                        }
                        else if (criterion.Key == CommonManager.GetIdCriterionName())
                        {
                            if (!String.IsNullOrEmpty(criterion.Value))
                            {
                                sqlString.Append("AND id IN (" + criterion.Value + ") ");
                            }
                        }
                        else if (criterion.Key == CommonManager.GetTitleCriterionName())
                        {
                            if (!String.IsNullOrEmpty(criterion.Value))
                            {
                                sqlString.Append("AND title LIKE '%" + criterion.Value + "%' ");
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Objects

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
                bestSeller = dr["best_seller"] == null ? false : Convert.ToBoolean(dr["best_seller"]),
                overview = dr["overview"].ToString(),
                type = dr["type"].ToString(),
                dateAdded = Convert.ToDateTime(dr["date_added"]),
                genre = dr["genre"].ToString()
            };
        }

        #endregion
    }
}