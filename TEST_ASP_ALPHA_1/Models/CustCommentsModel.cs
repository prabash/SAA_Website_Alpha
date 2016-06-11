using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_ASP_ALPHA_1.Models
{
    public class CustCommentsModel
    {
        public void SaveComment(string name, string email, string company, string tel, string address, string comment)
        {
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                string sqlString = @"INSERT INTO cust_comments (name, email_address, company, telephone_no, address, comments, date) 
                                        VALUES (@name, @emailAdd, @company, @telephonNo, @address, @comments, @date)";

                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    com.Parameters.AddWithValue("@name", name);
                    com.Parameters.AddWithValue("@emailAdd", email);
                    com.Parameters.AddWithValue("@company", company);
                    com.Parameters.AddWithValue("@telephonNo", tel);
                    com.Parameters.AddWithValue("@address", address);
                    com.Parameters.AddWithValue("@comments", comment);
                    com.Parameters.AddWithValue("@date", DateTime.Now);

                    com.ExecuteNonQuery();
                }
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
    }
}