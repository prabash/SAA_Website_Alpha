using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class CustWishlistModel
    {
        #region Set
        public void AddToWishList(int custId, int itemId)
        {
            try
            {
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"INSERT INTO customer_wishlist (customer_id, item_id, added_date) 
                                    VALUES (@custId, @itemId, @date, @registerDate, @active)";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Parameters.AddWithValue("@custId", custId);
                        com.Parameters.AddWithValue("@itemId", itemId);
                        com.Parameters.AddWithValue("@date", DateTime.Now);

                        com.ExecuteNonQuery();
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

        public List<int> GetWishlistItems(int custId)
        {
            List<int> returnList = new List<int>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.Append("SELECT * FROM customer_wishlist ");
                sbSqlString.Append("WHERE customer_id = @custId");

                string sqlString = sbSqlString.ToString();
                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    com.Parameters.AddWithValue("@custId", custId);

                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(Convert.ToInt32(dr["item_id"]));
                    }
                }
            }

            return returnList;
        }

        #endregion
    }
}