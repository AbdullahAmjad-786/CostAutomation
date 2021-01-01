using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace EnterpriseAutomation.lumino
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string startDate = "";
        string endDate = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getProductNames(startDate,endDate);
            getBillsbyProduct(startDate,endDate);
            getTotalCost(startDate, endDate);
            getTags(startDate, endDate);
            getBillsbyTags(startDate, endDate);
            getDates(startDate, endDate);
            getBillsbyDate(startDate, endDate);
            ProductPieChart(startDate, endDate);
            TopTenTags(startDate, endDate);
            ProductBarChart(startDate, endDate);
            DateWiseBarChart(startDate, endDate);
        }

        void getProductNames(string sdate, string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select SubscriptionName from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' Group by SubscriptionName";
            }
            else
            {
                query = "Select SubscriptionName from dbo.ReportData Group by SubscriptionName";
            }
            DataTable dtDataItemsSets = ExecuteQuery(query);
            List<string> Product = new List<string>();
            foreach (DataRow dr in dtDataItemsSets.Rows)
            {
                string val;
                val = dr["SubscriptionName"].ToString();
                if (val != "")
                {
                    Product.Add(val);
                }
            }
            data.Add(Product);
            
            JavaScriptSerializer ser = new JavaScriptSerializer();
            hfLabels.Value = ser.Serialize(data);
        }

        void getBillsbyProduct(string sdate , string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' and SubscriptionName != 'TLX-Internal' Group by SubscriptionName ";
            }
            else
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData Where SubscriptionName != 'TLX-Internal' Group by SubscriptionName";
            }

            DataTable dtDataItemsSets = ExecuteQuery(query);
            List<double> bills = new List<double>();
            int i = 0;
            foreach (DataRow dr in dtDataItemsSets.Rows)
            {
                double val;
                if (dr["Cost"].ToString() != "")
                {
                    val =Convert.ToDouble(dr["Cost"].ToString());
                    val = Convert.ToDouble(Math.Round(Convert.ToDecimal(val), 2));
                    bills.Add(val);
                }
                else
                {
                   val = 0;
                }
            }
            data.Add(bills);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            hfBillsProduct.Value = ser.Serialize(data);
        }

        void getTotalCost(string sdate , string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' and SubscriptionName != 'TLX-Internal'";
            }
            else
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where SubscriptionName != 'TLX-Internal'";
            }
            try
            {
                DataTable dtDataItemsSets = ExecuteQuery(query);
                DataRow dt = dtDataItemsSets.Rows[0];
                string val = dt["Cost"].ToString();
                decimal value = Math.Round(Convert.ToDecimal(val), 2);
                lblTotal.Text = "$" + value.ToString();
            }
            catch
            {
                lblTotal.Text = "$" + 0;
            }
            
        }

        void getTags(string sdate, string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select Tags from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Tags";
            }
            else
            {
                query = "Select Tags from dbo.ReportData Group by Tags";
            }
            DataTable dtDataItemsSets = ExecuteQuery(query);
            List<string> Product = new List<string>();
            foreach (DataRow dr in dtDataItemsSets.Rows)
            {
                string val;
                val = dr["Tags"].ToString();
                if (val != "")
                {
                    Product.Add(val);
                }
            }
            data.Add(Product);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            hfTags.Value = ser.Serialize(data);
        }

        void getBillsbyTags(string sdate, string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Tags";
            }
            else
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData Group by Tags";
            }

            DataTable dtDataItemsSets = ExecuteQuery(query);
            List<double> bills = new List<double>();
            int i = 0;
            foreach (DataRow dr in dtDataItemsSets.Rows)
            {
                double val;
                if (dr["Cost"].ToString() != "")
                {
                    val = Convert.ToDouble(dr["Cost"].ToString());
                    val = Convert.ToDouble(Math.Round(Convert.ToDecimal(val), 2));

                    bills.Add(val);
                }
                else
                {
                    val = 0;
                }
            }
            data.Add(bills);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            hfBillsTags.Value = ser.Serialize(data);
        }

        void getDates(string sdate, string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select Date from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Date order by Date";
            }
            else
            {
                query = "Select Date from dbo.ReportData Group by Date order by Date";
            }
            DataTable dtDataItemsSets = ExecuteQuery(query);
            List<string> Product = new List<string>();
            foreach (DataRow dr in dtDataItemsSets.Rows)
            {
                string val;
                val = dr["Date"].ToString();
                if (val != "")
                {
                   string[] value = val.Split('-');
                    Product.Add(value[2]);
                }
            }
            data.Add(Product);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            hfDates.Value = ser.Serialize(data);
        }

        void getBillsbyDate(string sdate, string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Date order by Date";
            }
            else
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData Group by Date order by Date";
            }

            DataTable dtDataItemsSets = ExecuteQuery(query);
            List<double> bills = new List<double>();
            int i = 0;
            foreach (DataRow dr in dtDataItemsSets.Rows)
            {
                double val;
                if (dr["Cost"].ToString() != "")
                {
                    val = Convert.ToDouble(dr["Cost"].ToString());
                    val = Convert.ToDouble(Math.Round(Convert.ToDecimal(val), 2));
                    bills.Add(val);
                }
                else
                {
                    val = 0;
                }
            }
            data.Add(bills);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            hfBillsDates.Value = ser.Serialize(data);
        }

        void ProductPieChart(string sdate,string edate)
        {
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select SubscriptionName, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' and SubscriptionName != 'TLX-Internal' Group by SubscriptionName ";
            }
            else
            {
                query = "Select SubscriptionName, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where SubscriptionName != 'TLX-Internal' Group by SubscriptionName";
            }
            SqlDataSource1.SelectCommand = query;
            PieTemp.Series["Default"].Label = "#VALX" + ": " + "#PERCENT{P1}";
            PieTemp.Series["Default"].LegendText = "#VALX" + " ($ " + "#VAL" + ")";
        }

        void TopTenTags(string sdate, string edate)
        {
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select Top(10) Tags, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where Tags != 'NULL' and Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Tags order by Cost Desc";
            }
            else
            {
                query = "Select Top(10) Tags, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where Tags != 'NULL' Group by Tags order by Cost Desc";
            }

            DataTable dtDataItemsSets = ExecuteQuery(query);
            DataTable dtCloned = dtDataItemsSets.Clone();
            dtCloned.Columns[1].DataType = typeof(string);
            foreach (DataRow row in dtDataItemsSets.Rows)
            {
                dtCloned.ImportRow(row);
            }
            foreach (DataRow dr in dtCloned.Rows)
            {
                string val;
                if (dr["Cost"].ToString() != "")
                {
                    val = dr["Cost"].ToString();
                    val = "$ " + val.ToString();
                    dr["Cost"] = val;
                }
            }
            gridTags.DataSource = dtCloned;
            gridTags.DataBind();
        }

        void ProductBarChart(string sdate, string edate)
        {
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select SubscriptionName, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' and SubscriptionName != 'TLX-Internal' Group by SubscriptionName order by Cost Asc";
            }
            else
            {
                query = "Select SubscriptionName, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where SubscriptionName != 'TLX-Internal' Group by SubscriptionName order by Cost Asc";
            }
            SqlDataSource2.SelectCommand = query;
            ProductChart.ChartAreas[0].AxisX.Interval = 1;
            ProductChart.Series["Default"].Label = "$ " + "#VALY";
            ProductChart.Series[0].ToolTip = "#VALX : $ #VALY";
        }

        void DateWiseBarChart(string sdate, string edate)
        {
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select Date, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Date order by Date Desc";
            }
            else
            {
                query = "Select Date, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData Group by Date order by Date Desc";
            }
            SqlDataSource3.SelectCommand = query;
            DateChart.ChartAreas[0].AxisX.Interval = 1;
            DateChart.Series["Default"].Label = "$ " + "#VALY";
            DateChart.Series[0].ToolTip = "#VALX : $ #VALY";
        }

        public DataTable ExecuteQuery(string query)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AzureDBConnectionString"].ConnectionString);
            SqlDataAdapter dap = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            return ds.Tables[0];
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            startDate = hfstartDate.Value.ToString();
            endDate = hfendDate.Value.ToString();
            getProductNames(startDate, endDate);
            getBillsbyProduct(startDate, endDate);
            getTotalCost(startDate, endDate);
            getTags(startDate, endDate);
            getBillsbyTags(startDate, endDate);
            getDates(startDate, endDate);
            getBillsbyDate(startDate, endDate);
            ProductPieChart(startDate, endDate);
            TopTenTags(startDate, endDate);
            ProductBarChart(startDate, endDate);
            DateWiseBarChart(startDate, endDate);
        }

        protected void PieTemp_Load(object sender, EventArgs e)
        {
        }

        protected void gridTags_RowCreated(object sender, GridViewRowEventArgs e)
        {  }

        protected void gridTags_RowCreated1(object sender, GridViewRowEventArgs e)
        {
            if (gridTags.Rows.Count != 0)
            {
                for (int i = 0; i < gridTags.Rows[gridTags.Rows.Count - 1].Cells.Count; i++)
                {
                    if (i == 0)
                        gridTags.Rows[gridTags.Rows.Count - 1].Cells[i].HorizontalAlign = HorizontalAlign.Left;
                    else
                        gridTags.Rows[gridTags.Rows.Count - 1].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    int NumCells = e.Row.Cells.Count;
                    for (int i = 0; i < NumCells; i++)
                    {
                        string value = e.Row.Cells[i].Text;
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;

                    }
                }
            }
        }
    }
}