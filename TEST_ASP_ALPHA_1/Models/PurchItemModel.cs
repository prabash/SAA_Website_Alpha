﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class PurchItemModel
    {
        #region Set
        public void SavePurchaseItems(List<PurchaseItemObject> purchItems)
        {
            MySqlTransaction transaction = null;
            try
            {
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    transaction = con.BeginTransaction();
                    string sqlString = @"INSERT INTO purchase_items (purchase_cart_id, item_id, item_quantity, discount, cost_per_item, sub_total, status) 
                                    VALUES (@purchCartId, @itemId, @itemQty, @discount, @costPerItem, @subTotal, @status)";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Transaction = transaction;
                        com.Parameters.AddWithValue("@purchCartId", 0);
                        com.Parameters.AddWithValue("@itemId", 0);
                        com.Parameters.AddWithValue("@itemQty", 0);
                        com.Parameters.AddWithValue("@discount", 0.00);
                        com.Parameters.AddWithValue("@costPerItem", 0.00);
                        com.Parameters.AddWithValue("@subTotal", 0.00);
                        com.Parameters.AddWithValue("@status", "");

                        foreach (var item in purchItems)
                        {
                            com.Parameters["@purchCartId"].Value = item.PurchCartId;
                            com.Parameters["@itemId"].Value = item.ItemId;
                            com.Parameters["@itemQty"].Value = item.ItemQuantity;
                            com.Parameters["@discount"].Value = item.Discount;
                            com.Parameters["@costPerItem"].Value = item.CostPerItem;
                            com.Parameters["@subTotal"].Value = item.SubTotal;
                            com.Parameters["@status"].Value = item.Status;
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

        public void CancelOrder(int id)
        {
            MySqlTransaction transaction = null;
            try
            {
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    transaction = con.BeginTransaction();
                    string sqlString = @"UPDATE purchase_items SET status=@status WHERE id=@id";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Transaction = transaction;

                        com.Parameters.AddWithValue("@status", CommonManager.Status_GetCancelledItemName());
                        com.Parameters.AddWithValue("@id", id);
                        com.ExecuteNonQuery();

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

        public List<PurchaseItemObject> GetPurchItemDetails(PurchItemGetType getType, int custId = 0, string custEmail = null, int purchCartId = 0, DateTime? fromPurchDate = null, DateTime? toPurchDate = null, PurchItemOrderBy orderBy = PurchItemOrderBy.purchDateAsc, int limit = 0, int currentPage = 0)
        {
            List<PurchaseItemObject> returnList = new List<PurchaseItemObject>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.Append("SELECT pi.*, pc.purchased_date, it.title ");
                sbSqlString.Append("FROM purchase_items pi, purchase_cart pc, items it ");
                sbSqlString.Append("WHERE pi.purchase_cart_id = pc.id ");
                sbSqlString.Append("AND pi.item_id = it.id ");
                sbSqlString.Append("AND ");

                switch (getType)
                {
                    case PurchItemGetType.customerEmail:
                        sbSqlString.Append("pc.cust_email_address = @custEmail ");
                        break;
                    case PurchItemGetType.customerId:
                        sbSqlString.Append("pc.cust_id = @custId ");
                        break;
                    case PurchItemGetType.purchCartId:
                        sbSqlString.Append("pc.id = @cartId ");
                        break;
                    case PurchItemGetType.purchDate:
                        sbSqlString.Append("pc.purchased_date BETWEEN @fromPurchDate AND @toPurchDate ");
                        break;
                    default:
                        break;
                }

                switch (orderBy)
                {
                    case PurchItemOrderBy.purchDateAsc:
                        sbSqlString.Append("ORDER BY pc.purchased_date ASC ");
                        break;
                    case PurchItemOrderBy.purchDateDesc:
                        sbSqlString.Append("ORDER BY pc.purchased_date DESC ");
                        break;
                    case PurchItemOrderBy.OrderIdAsc:
                        sbSqlString.Append("ORDER BY pi.id ASC ");
                        break;
                    case PurchItemOrderBy.OrderIdDesc:
                        sbSqlString.Append("ORDER BY pi.id DESC ");
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
                        case PurchItemGetType.customerEmail:
                            com.Parameters.AddWithValue("@custEmail", custEmail);
                            break;
                        case PurchItemGetType.customerId:
                            com.Parameters.AddWithValue("@custId", custId);
                            break;
                        case PurchItemGetType.purchCartId:
                            com.Parameters.AddWithValue("@cartId", purchCartId);
                            break;
                        case PurchItemGetType.purchDate:
                            com.Parameters.AddWithValue("@fromPurchDate", fromPurchDate);
                            com.Parameters.AddWithValue("@toPurchDate", toPurchDate);
                            break;
                        default:
                            break;
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(GetInfoAddedPurchItemObject(dr));
                    }
                }
            }

            return returnList;
        }

        public int GetPurchItemCount(PurchItemGetType getType, int custId = 0, string custEmail = null, int purchCartId = 0, DateTime? fromPurchDate = null, DateTime? toPurchDate = null)
        {
            var purchItems = GetPurchItemDetails(getType, custId, custEmail, purchCartId, fromPurchDate, toPurchDate);
            if (purchItems != null)
                return purchItems.Count();
            else
                return 0;
        }

        #endregion

        #region Objects

        private PurchaseItemObject GetInfoAddedPurchItemObject(MySqlDataReader dr)
        {
            var purchItemOb = new PurchaseItemObject();
            purchItemOb.PurchId = dr["id"] != null ? Convert.ToInt32(dr["id"]) : 0;
            purchItemOb.PurchCartId = dr["purchase_cart_id"] != null ? Convert.ToInt32(dr["purchase_cart_id"]) : 0;
            purchItemOb.ItemId = dr["item_id"] != null ? Convert.ToInt32(dr["item_id"]) : 0;
            purchItemOb.ItemQuantity = dr["item_quantity"] != null ? Convert.ToInt32(dr["item_quantity"]) : 0;
            purchItemOb.Discount = dr["discount"] != null ? Convert.ToDouble(dr["discount"]) : 0;
            purchItemOb.CostPerItem = dr["cost_per_item"] != null ? Convert.ToDouble(dr["cost_per_item"]) : 0;
            purchItemOb.SubTotal = dr["sub_total"] != null ? Convert.ToDouble(dr["sub_total"]) : 0;
            purchItemOb.Status = dr["status"] != null ? dr["status"].ToString() : "";
            purchItemOb.PurchDate = dr["purchased_date"] != null ? Convert.ToDateTime(dr["purchased_date"]) : Convert.ToDateTime("00/00/0000");
            purchItemOb.ItemTitle = dr["title"] != null ? dr["title"].ToString() : "";
            return purchItemOb;
        }

        #endregion

    }
}