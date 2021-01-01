using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace EnterpriseAutomation.lumino.appcode
{
    public class UserLogin
    {
        SqlConnection conn;
        string query;

        public void SetConnection()
        {
            conn = new SqlConnection(System.Configuration.ConfigurationManager.
        ConnectionStrings["AzureDBConnectionString"].ConnectionString);
            conn.Open();
        }

        public bool CheckUser(string name, string pass)
        {
            SetConnection();
            query = "SELECT * FROM Login WHERE Email = '" + name + "' and Password = '" + pass + "'";
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                conn.Close();
                if (temp > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool AddUser(string username, string pass)
        {
            SetConnection();
            query = "insert into Login(Email,Password) " +
                        "values ('" + username + "','" + pass + "')";
            // query = "Select Username from Login";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException se)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}