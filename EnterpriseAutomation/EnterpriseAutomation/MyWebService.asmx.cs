using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace EnterpriseAutomation
{
    /// <summary>
    /// Summary description for MyWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MyWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public List<object> GenerateChart()
        {
            List<object> data = new List<object>();
            string query = "Select Product,SUM(Cast(dbo.ReportData.Cost as float)) from dbo.ReportData Group by Product";
            DataTable dtDataItemsSets = ExecuteQuery(query);
            List<int> lst_dataItem = new List<int>();
            foreach (DataRow dr in dtDataItemsSets.Rows)
            {
                lst_dataItem.Add(Convert.ToInt32(dr["Cost"].ToString()));
            }
            data.Add(lst_dataItem);

            return data;
        }

        public DataTable ExecuteQuery(string query)
        {
            SqlConnection conn = new SqlConnection("Data Source=ABDULLAHAMJAD;Initial Catalog=EnterpriseDB;Persist Security Info=True;User ID=sa;Password=Admin@123");
            SqlDataAdapter dap = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            return ds.Tables[0];
        }
    }
}
