using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class LoginManager
    {
        public void RegisterUser(string email, string username, string password)
        {
            try
            {
                var custDetail = GetCustomerDetails(CustomerGetType.email, 0, email);
                if (custDetail.Count == 0)
                {
                    using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                    {
                        string sqlString = @"INSERT INTO customers (email_address, username, password, registered_date, active) 
                                        VALUES (@emailAdd, @username, @password, @registerDate, @active)";

                        using (MySqlCommand com = new MySqlCommand(sqlString, con))
                        {
                            com.Parameters.AddWithValue("@emailAdd", email);
                            com.Parameters.AddWithValue("@username", username);
                            com.Parameters.AddWithValue("@password", password);
                            com.Parameters.AddWithValue("@registerDate", DateTime.Now);
                            com.Parameters.AddWithValue("@active", Convert.ToInt32(false));

                            com.ExecuteNonQuery();
                        }
                    }
                }
                else
                    throw new Exception("Email already exists in the database");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUserData(int id, string email, string name, string nicNo, string tel, string address)
        {
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                string sqlString = @"UPDATE customers SET nic_no = @nicNo, name = @name, telephone_no = @telephonNo, address = @address
                                     WHERE id= @id AND email_address = @emailAdd";

                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    com.Parameters.AddWithValue("@nicNo", nicNo);
                    com.Parameters.AddWithValue("@name", name);
                    com.Parameters.AddWithValue("@telephonNo", tel);
                    com.Parameters.AddWithValue("@address", address);
                    com.Parameters.AddWithValue("@id", id);
                    com.Parameters.AddWithValue("@emailAdd", email);

                    com.ExecuteNonQuery();
                }
            }
        }

        public void ActivateUser(string email)
        {
            try
            {
                 var custDetail = GetCustomerDetails(CustomerGetType.email, 0, email);
                 if (custDetail.Count > 0)
                 {
                     using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                     {
                         string sqlString = @"UPDATE customers SET active = @active WHERE email_address = @emailAdd";

                         using (MySqlCommand com = new MySqlCommand(sqlString, con))
                         {
                             com.Parameters.AddWithValue("@active", Convert.ToInt32(true));
                             com.Parameters.AddWithValue("@emailAdd", email);

                             com.ExecuteNonQuery();
                         }
                     }
                 }
                 else
                     throw new Exception("Not registered in the database yet!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerObject> GetCustomerDetails(CustomerGetType getType, int id = 0, string email = null, string name = null, string tel = null, DateTime? fromRegDate = null, DateTime? toRegDate = null)
        {
            List<CustomerObject> returnList = new List<CustomerObject>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.Append("SELECT * FROM customers ");
                sbSqlString.Append("WHERE ");

                switch (getType)
                {
                    case CustomerGetType.id:
                        sbSqlString.Append("id = @id");
                        break;
                    case CustomerGetType.email:
                        sbSqlString.Append("email_address = @email");
                        break;
                    case CustomerGetType.name:
                        sbSqlString.Append("name like '% @name %'");
                        break;
                    case CustomerGetType.telephone:
                        sbSqlString.Append("telephone_no like '% @tel %'");
                        break;
                    case CustomerGetType.registeredDate:
                        sbSqlString.Append("registered_date BETWEEN @fromRegDate AND @toRegDate");
                        break;
                    default:
                        break;
                }

                string sqlString = sbSqlString.ToString();
                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    switch (getType)
                    {
                        case CustomerGetType.id:
                            com.Parameters.AddWithValue("@id", id);
                            break;
                        case CustomerGetType.email:
                            com.Parameters.AddWithValue("@email", email);
                            break;
                        case CustomerGetType.name:
                            com.Parameters.AddWithValue("@name", name);
                            break;
                        case CustomerGetType.telephone:
                            com.Parameters.AddWithValue("@tel", tel);
                            break;
                        case CustomerGetType.registeredDate:
                            com.Parameters.AddWithValue("@fromRegDate", fromRegDate);
                            com.Parameters.AddWithValue("@toRegDate", toRegDate);
                            break;
                        default:
                            break;
                    }
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(GetInfoAddedCustomerObject(dr));
                    }
                }
            }

            return returnList;
        }

        public bool ValidateLogin(string email, string password, out int custId, out string custName)
        {
            bool loginSuccess = false;
            custId = 0;
            custName = "";

            var custDetail = GetCustomerDetails(CustomerGetType.email, 0, email);
            if (custDetail != null && custDetail.Count > 0)
            {
                var customer = custDetail.First();
                if (customer.active)
                {
                    if (password == customer.password)
                    {
                        custId = customer.Id;
                        custName = customer.username;
                        loginSuccess = true;
                    }
                }
                else
                {
                    throw new Exception("Your account has not been activated yet!");
                }
            }

            return loginSuccess;
        }

        public CustomerObject GetInfoAddedCustomerObject(MySqlDataReader dr)
        {
            return new CustomerObject
            {
                Id = Convert.ToInt32(dr["id"]),
                nicNo = dr["nic_no"].ToString(),
                name = dr["name"].ToString(),
                emailAddress = dr["email_address"].ToString(),
                telephoneNo = dr["telephone_no"].ToString(),
                address = dr["address"].ToString(),
                username = dr["username"].ToString(),
                password = dr["password"].ToString(),
                registeredDate = Convert.ToDateTime(dr["registered_date"]),
                active = Convert.ToBoolean(Convert.ToInt32(dr["active"].ToString()))
            };
        }

    }
}