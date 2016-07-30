using MySql.Data.MySqlClient;
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
        public void AddPurchaseItems(List<PurchaseItemObject> purchItems)
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
                        new CounterModel().IncrementCounter(CommonManager.Counter_GetPurchasedItemsToday_Name(), purchItems.Count);
                        new CounterModel().IncrementCounter(CommonManager.Counter_GetPurchasedUsersToday_Name());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateItemStatus(int id, ItemStatus status)
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
                        switch (status)
                        {
                            case ItemStatus.New:
                                com.Parameters.AddWithValue("@status", CommonManager.Status_GetNewItemName());
                                break;
                            case ItemStatus.Delivered:
                                com.Parameters.AddWithValue("@status", CommonManager.Status_GetDeliveredItemName());
                                break;
                            case ItemStatus.Cancelled:
                                com.Parameters.AddWithValue("@status", CommonManager.Status_GetCancelledItemName());
                                break;
                            default:
                                break;
                        }
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
            finally
            {
                UpdateCartStatusByItem(id, status);
            }
        }

        private void UpdateCartStatusByItem(int purchItemId, ItemStatus status)
        {
            try
            {
                var currentStatus = "";
                bool allItemsInStatus = false;

                switch (status)
                {
                    case ItemStatus.New:
                        currentStatus = CommonManager.Status_GetNewItemName();
                        break;
                    case ItemStatus.Delivered:
                        currentStatus = CommonManager.Status_GetDeliveredItemName();
                        break;
                    case ItemStatus.Cancelled:
                        currentStatus = CommonManager.Status_GetCancelledItemName();
                        break;
                    default:
                        break;
                }

                var purchItem = GetPurchItemDetails(PurchItemGetType.purchId, 0, null, 0, null, null, purchItemId).First();
                var cartDetails = new PurchCartModel().GetCartDetails(CartGetType.cartId, 0, null, purchItem.PurchCartId).First();
                foreach (var item in cartDetails.Items)
                {
                    if (item.Status == currentStatus)
                        allItemsInStatus = true;
                    else
                    {
                        allItemsInStatus = false;
                        break;
                    }
                }

                if (allItemsInStatus)
                    new PurchCartModel().UpdateCartStatus(cartDetails.CartId, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get

        public List<PurchaseItemObject> GetPurchItemDetails(PurchItemGetType getType, int custId = 0, string custEmail = null, int purchCartId = 0, DateTime? fromPurchDate = null, DateTime? toPurchDate = null, int purchId = -1, PurchItemOrderBy orderBy = PurchItemOrderBy.purchDateAsc, int limit = 0, int currentPage = 0)
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
                    case PurchItemGetType.purchId:
                        sbSqlString.Append("pi.id = @purchId ");
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
                        case PurchItemGetType.purchId:
                            com.Parameters.AddWithValue("@purchId", purchId);
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

        public string Chart_GetPurchasedItemByType(DateTime? fromPurchDate = null, DateTime? toPurchDate = null)
        {
            string chartData = "";
            List<string> typesList = new List<string>();
            List<string> countsList = new List<string>();

            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.Append("SELECT it.type type, COUNT(pi.item_id) count ");
                sbSqlString.Append("FROM purchase_items pi, purchase_cart pc, items it ");
                sbSqlString.Append("WHERE pi.purchase_cart_id = pc.id ");
                sbSqlString.Append("AND pi.item_id = it.id ");
                if (fromPurchDate != null || toPurchDate != null)
                {
                    if (fromPurchDate != null && toPurchDate == null)
                        sbSqlString.Append("AND pc.purchased_date >= @fromPurchDate ");
                    else if (fromPurchDate == null && toPurchDate != null)
                        sbSqlString.Append("AND pc.purchased_date <= @toPurchDate ");
                    else if (fromPurchDate != null && toPurchDate != null)
                        sbSqlString.Append("AND pc.purchased_date BETWEEN @fromPurchDate AND @toPurchDate ");
                }
                sbSqlString.Append("GROUP BY it.type ");

                string sqlString = sbSqlString.ToString();
                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    if (fromPurchDate != null)
                        com.Parameters.AddWithValue("@fromPurchDate", fromPurchDate);
                    if (toPurchDate != null)
                        com.Parameters.AddWithValue("@toPurchDate", toPurchDate);
                    
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        typesList.Add(dr["type"].ToString());
                        countsList.Add(dr["count"].ToString());
                    }
                }
            }
            chartData = FormatListForChart(typesList) + "|" + FormatListForChart(countsList);
            return chartData;
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

        private string FormatListForChart(List<string> list)
        {
            var returnStr = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i < list.Count - 1)
                {
                    returnStr.Append(list[i] + ",");
                }
                else if (i == list.Count - 1)
                {
                    returnStr.Append(list[i]);
                }
            }
            return returnStr.ToString();
        }

        #endregion

    }
}