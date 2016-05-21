using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_ASP_ALPHA_1.Models
{
    public class ConnectionManager
    {
        public static MySqlConnection GetConnection()
        {
            //connectionString
            string connectionString = @"Data Source=localhost; Database=articles; User ID=root; Password=p@ssw0rd";
            return new MySqlConnection(connectionString);
        }
    }
}