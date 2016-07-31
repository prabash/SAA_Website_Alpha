using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class AdminModel
    {
        #region Set

        public void RegisterAdmin(string username, string password, string type)
        {
            try
            {
                var adminDetail = GetAdminDetails(0, username);
                if (adminDetail.Count == 0)
                {
                    using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                    {
                        string sqlString = @"INSERT INTO administrators (username, password, type) 
                                        VALUES (@username, @password, @type)";

                        using (MySqlCommand com = new MySqlCommand(sqlString, con))
                        {
                            com.Parameters.AddWithValue("@username", username);
                            com.Parameters.AddWithValue("@password", password);
                            com.Parameters.AddWithValue("@type", type);
                            com.ExecuteNonQuery();
                        }
                    }
                }
                else
                    throw new Exception("username already exists in the database!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAdminData(int id, string username, string type)
        {
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                string sqlString = @"UPDATE administrators SET username = @username, type = @type WHERE id= @id";

                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    com.Parameters.AddWithValue("@username", username);
                    com.Parameters.AddWithValue("@type", type);
                    com.Parameters.AddWithValue("@id", id);
                    com.ExecuteNonQuery();
                }
            }
        }

        public void ResetPassword(string username, string newPassword, string oldPassword = null)
        {
            try
            {
                var adminDetail = GetAdminDetails(0, username);
                if (adminDetail.Count == 1)
                {
                    //This check is used ONLY when the user knows the current password
                    if (oldPassword != null)
                    {
                        if (adminDetail.First().Password != oldPassword)
                            throw new Exception("The current password you entered is incorrect!");
                    }

                    using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                    {
                        string sqlString = @"UPDATE administrators SET password = @password WHERE id= @id AND username = @username";

                        using (MySqlCommand com = new MySqlCommand(sqlString, con))
                        {
                            com.Parameters.AddWithValue("@password", newPassword);
                            com.Parameters.AddWithValue("@id", adminDetail.First().Id);
                            com.Parameters.AddWithValue("@username", username);

                            com.ExecuteNonQuery();
                        }
                    }
                }
                else
                    throw new Exception("Your username is not registered yet!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAdminUser(int id)
        {
            try
            {
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"DELETE FROM administrators WHERE id=@id";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Parameters.AddWithValue("@id", id);

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

        public bool ValidateLogin(string username, string password, out int adminId, out string adminType)
        {
            bool loginSuccess = false;
            adminId = 0;
            adminType = "";

            var adminDetail = GetAdminDetails(0, username);
            if (adminDetail != null && adminDetail.Count > 0)
            {
                var admin = adminDetail.First();
                
                if (password == admin.Password)
                {
                    adminId = admin.Id;
                    adminType = admin.Type;
                    loginSuccess = true;
                }
                else
                {
                    throw new Exception("The username or password you have entered is incorrect!");
                }
                
            }
            else
                throw new Exception("The username or password you have entered is incorrect!");

            return loginSuccess;
        }

        public List<AdminUserObject> GetAdminDetails(int id = -1, string username = null)
        {
            List<AdminUserObject> returnList = new List<AdminUserObject>();
            using (MySqlConnection con = ConnectionManager.GetOpenConnection())
            {
                var sqlString = "select * from administrators ";
                if (id > 0)
                    sqlString += "where id = " + id;
                else if (!String.IsNullOrEmpty(username))
                    sqlString += "where username = " + username;

                using (MySqlCommand com = new MySqlCommand(sqlString, con))
                {
                    MySqlDataReader dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        returnList.Add(new AdminUserObject
                        {
                            Id = Convert.ToInt32(dr["id"].ToString()),
                            Username = dr["username"].ToString(),
                            Password = dr["password"].ToString(),
                            Type = dr["type"].ToString()
                        });
                    }
                }
            }
            return returnList;
        }

        #endregion
    }
}