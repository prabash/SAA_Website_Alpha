using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TEST_ASP_ALPHA_1.Common;

namespace TEST_ASP_ALPHA_1.Models
{
    public class CounterModel
    {
        #region Set

        public void IncrementCounter(string counterName, int incrementBy = 1)
        {
            try
            {
                int currentCount = GetCounterValue(counterName);
                int newCount = currentCount + incrementBy;
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"UPDATE system_counters SET count = @count WHERE counter_name = @counterName";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Parameters.AddWithValue("@count", newCount);
                        com.Parameters.AddWithValue("@counterName", counterName);

                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ResetCounter(string counterName, int resetCount = 0)
        {
            try
            {
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"UPDATE system_counters SET count = @count WHERE counter_name = @counterName";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Parameters.AddWithValue("@count", resetCount);
                        com.Parameters.AddWithValue("@counterName", counterName);

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

        public int GetCounterValue(string counterName)
        {
            try
            {
                int count = 0;
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"SELECT count FROM system_counters WHERE counter_name = @counterName";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Parameters.AddWithValue("@counterName", counterName);

                        MySqlDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            count = Convert.ToInt32(dr["count"]);
                        }
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}