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
                var counterOb  = GetCounterValue(counterName);
                int currentCount = counterOb.Count;

                DateTime currentDate = counterOb.Date;
                if (currentDate.Date != DateTime.Today)
                {
                    ResetCounter(counterName);
                    currentCount = 0;
                }
                    
                int newCount = currentCount + incrementBy;
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"UPDATE system_counters_per_day SET count = @count WHERE counter_name = @counterName";

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
                    string sqlString = @"UPDATE system_counters_per_day SET count = @count, date = @date WHERE counter_name = @counterName";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Parameters.AddWithValue("@date", DateTime.Today);
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

        public CounterObject GetCounterValue(string counterName)
        {
            try
            {
                CounterObject returnOb = new CounterObject();
                using (MySqlConnection con = ConnectionManager.GetOpenConnection())
                {
                    string sqlString = @"SELECT * FROM system_counters_per_day WHERE counter_name = @counterName";

                    using (MySqlCommand com = new MySqlCommand(sqlString, con))
                    {
                        com.Parameters.AddWithValue("@counterName", counterName);

                        MySqlDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            returnOb = GetCounterObject(dr);
                        }
                    }
                }
                return returnOb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CounterObject GetCounterObject(MySqlDataReader dr)
        {
            CounterObject returnOb = new CounterObject();
            returnOb.CounterName = dr["counter_name"].ToString();
            returnOb.Count = Convert.ToInt32(dr["count"]);
            returnOb.Date = dr["date"] != System.DBNull.Value ? Convert.ToDateTime(dr["date"]) : Convert.ToDateTime("01/01/0001");

            return returnOb;
        }

        #endregion
    }
}
