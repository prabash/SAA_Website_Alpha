using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class PurchCartModel
    {
        #region Set

        public void SaveCart(CartObject cartOb)
        {
            MySqlTransaction transaction = null;
            try
            {
                long cartId = 0;
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    transaction = con.BeginTransaction();
                    string sqlString = @"INSERT INTO purchase_cart (cust_id, cust_email_address, purchased_date, cart_discount, car_delivery, cart_total, cart_status) 
                                    VALUES (@custId, @custEmailAdd, @purchDate, @cartDiscount, @cartDelivery, @cartTotal, @cartStatus)";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Transaction = transaction;
                        com.Parameters.AddWithValue("@custId", cartOb.CustId);
                        com.Parameters.AddWithValue("@custEmailAdd", cartOb.CustEmailAddress);
                        com.Parameters.AddWithValue("@purchDate", cartOb.PurchaseDate);
                        com.Parameters.AddWithValue("@cartDiscount", cartOb.CartDiscount);
                        com.Parameters.AddWithValue("@cartDelivery", cartOb.CartDelivery);
                        com.Parameters.AddWithValue("@cartTotal", cartOb.CartTotal);
                        com.Parameters.AddWithValue("@cartStatus", cartOb.CartStatus);

                        com.ExecuteNonQuery();
                        cartId = com.LastInsertedId;
                        transaction.Commit();
                    }
                }
                cartOb.Items.ForEach(rec => rec.PurchCartId = Convert.ToInt32(cartId));
                new PurchItemModel().SavePurchaseItems(cartOb.Items);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get

        public List<CartObject> GetCartDetails(CartGetType getType, int custId = 0, string custEmail = null, int purchCartId = 0, double fromTotal = 0, double toTotal = 0,
            DateTime? fromPurchDate = null, DateTime? toPurchDate = null, CartOrderBy orderBy = CartOrderBy.purchDateAsc, int limit = 0, int currentPage = 0)
        {
            List<CartObject> returnList = new List<CartObject>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.Append("SELECT pc.* ");
                sbSqlString.Append("FROM purchase_cart pc ");
                sbSqlString.Append("WHERE ");

                switch (getType)
                {
                    case CartGetType.customerEmail:
                        sbSqlString.Append("pc.cust_email_address = @custEmail ");
                        break;
                    case CartGetType.customerId:
                        sbSqlString.Append("pc.cust_id = @custId ");
                        break;
                    case CartGetType.totalValue:
                        sbSqlString.Append("pc.cart_total BETWEEN @fromTotal AND @toTotal ");
                        break;
                    case CartGetType.purchDate:
                        sbSqlString.Append("pc.purchased_date BETWEEN @fromPurchDate AND @toPurchDate ");
                        break;
                    default:
                        break;
                }

                switch (orderBy)
                {
                    case CartOrderBy.purchDateAsc:
                        sbSqlString.Append("ORDER BY pc.purchased_date ASC ");
                        break;
                    case CartOrderBy.purchDateDesc:
                        sbSqlString.Append("ORDER BY pc.purchased_date DESC ");
                        break;
                    case CartOrderBy.OrderIdAsc:
                        sbSqlString.Append("ORDER BY pc.id ASC ");
                        break;
                    case CartOrderBy.OrderIdDesc:
                        sbSqlString.Append("ORDER BY pc.id DESC ");
                        break;
                    default:
                        break;
                }

                if (limit > 0)
                {
                    sbSqlString.Append("LIMIT " + currentPage + "," + limit);
                }

                string sqlString = sbSqlString.ToString();
                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    switch (getType)
                    {
                        case CartGetType.customerEmail:
                            com.Parameters.AddWithValue("@custEmail", custEmail);
                            break;
                        case CartGetType.customerId:
                            com.Parameters.AddWithValue("@custId", custId);
                            break;
                        case CartGetType.totalValue:
                            com.Parameters.AddWithValue("@fromTotal", fromTotal);
                            com.Parameters.AddWithValue("@toTotal", toTotal);
                            break;
                        case CartGetType.purchDate:
                            com.Parameters.AddWithValue("@fromPurchDate", fromPurchDate);
                            com.Parameters.AddWithValue("@toPurchDate", toPurchDate);
                            break;
                        default:
                            break;
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(GetInfoAddedCartObject(dr));
                    }
                }
            }

            return returnList;
        }

        #endregion

        #region Objects

        private CartObject GetInfoAddedCartObject(MySqlDataReader dr)
        {
            var cartOb = new CartObject();
            cartOb.CartId = Convert.ToInt32(dr["id"]);
            cartOb.CustId = Convert.ToInt32(dr["cust_id"]);
            cartOb.CustEmailAddress = dr["cust_email_address"].ToString();
            cartOb.PurchaseDate = Convert.ToDateTime(dr["purchase_cart"]);
            cartOb.CartDiscount = dr["cart_discount"] != null ? Convert.ToDouble(dr["cart_discount"]) : 0.00;
            cartOb.CartDelivery = dr["car_delivery"] != null ? Convert.ToDouble(dr["car_delivery"]) : 0.00;
            cartOb.CartTotal = dr["cart_total"] != null ? Convert.ToDouble(dr["cart_total"]) : 0.00;
            cartOb.CartStatus = dr["cart_status"] != null ? dr["cart_status"].ToString() : "";
            cartOb.Items = new PurchItemModel().GetPurchItemDetails(PurchItemGetType.purchCartId, 0, "", cartOb.CartId);

            return cartOb;
        }

        #endregion
    }
}