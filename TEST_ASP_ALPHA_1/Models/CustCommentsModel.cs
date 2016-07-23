using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class CustCommentsModel
    {
        #region Set

        public void SaveCustComment(string name, string email, string tel, string subject, string comment)
        {
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                string sqlString = @"INSERT INTO cust_comments (name, email_address, telephone_no, subject, comments, date) 
                                        VALUES (@name, @emailAdd, @telephonNo, @subject, @comments, @date)";

                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    com.Parameters.AddWithValue("@name", name);
                    com.Parameters.AddWithValue("@emailAdd", email);
                    com.Parameters.AddWithValue("@telephonNo", tel);
                    com.Parameters.AddWithValue("@subject", subject);
                    com.Parameters.AddWithValue("@comments", comment);
                    com.Parameters.AddWithValue("@date", DateTime.Now);

                    com.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustComments(List<int> Ids)
        {
            try
            {
                var idSet = "";
                for (int i = 0; i < Ids.Count; i++)
                {
                    if (i < Ids.Count - 1)
                        idSet += Ids[i] + ",";
                    else if (i == Ids.Count - 1)
                        idSet += Ids[i];
                }

                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"DELETE FROM cust_comments WHERE id IN (" + idSet + ")";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveSubscription(string email)
        {
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                string sqlString = @"INSERT INTO newsletter_subs (email_address, date) 
                                        VALUES (@emailAdd, @date)";

                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    com.Parameters.AddWithValue("@emailAdd", email);
                    com.Parameters.AddWithValue("@date", DateTime.Now);

                    com.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Get
 
        public List<CustomerCommentObject> GetCustomerComments()
        {
            try
            {
                List<CustomerCommentObject> custComments = new List<CustomerCommentObject>();
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    int count = 0;
                    StringBuilder sbSqlString = new StringBuilder();
                    sbSqlString.Append("SELECT * FROM cust_comments ORDER BY date DESC");

                    string sqlString = sbSqlString.ToString();
                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        MySqlDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            custComments.Add(GetCustCommentObject(dr));
                        }
                    }
                    return custComments;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Objects

        private CustomerCommentObject GetCustCommentObject(MySqlDataReader dr)
        {
            var returnOb = new CustomerCommentObject();
            returnOb.Id = Convert.ToInt32(dr["id"]);
            returnOb.Name = dr["name"].ToString();
            returnOb.EmailAddress = dr["email_address"].ToString();
            returnOb.TelephoneNo = dr["telephone_no"] != System.DBNull.Value ? dr["telephone_no"].ToString() : "";
            returnOb.Subject = dr["subject"] != System.DBNull.Value ? dr["subject"].ToString() : "";
            returnOb.Comment = dr["comments"] != System.DBNull.Value ? dr["comments"].ToString() : "";
            returnOb.Date = Convert.ToDateTime(dr["date"]);

            return returnOb;
        }

        #endregion
    }
}