using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using EnterpriseAutomation.lumino;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;

namespace EnterpriseAutomation.Enterprise
{
    public partial class Products : System.Web.UI.Page
    {
        string startDate = "";
        string endDate = "";
        string Tag = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string value = hfValue.Value;
            ChangeProductName(value);
            getTotalCost(value, startDate, endDate);
            getTags(value, startDate, endDate);
            getBillsbyTags(value, startDate, endDate);
            TagsBarChart(value, startDate, endDate);
            ConsumedServiceChart(value, startDate, endDate);
            ClientCostFromTags(value, startDate, endDate, Tag);
            Main main = new Main();
            if (main.IsUserReadOnly())
            {
                gridMainTag.Visible = false;
                gridMainTag.Visible = false;
            }
        }

        void ChangeProductName(string val)
        {
            for (int i = 0; i < productValue.Items.Count; i++)
            {
                if (productValue.Items[i].Value == val)
                    productValue.SelectedIndex = i;
            }

            if (val == "Almusnet")
            {
                productName.InnerText = "ALMUSNET";
                Tag = "AN ";
            }
            else if (val == "Vicenna")
            {
                productName.InnerText = "VICENNA";
                Tag = "Vic ";
            }
            else if (val == "TMX")
            {
                productName.InnerText = "TMX";
                Tag = "TMX ";
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            startDate = hfstartDate.Value.ToString();
            endDate = hfendDate.Value.ToString();
            string value = hfValue.Value;

            ChangeProductName(value);
            getTotalCost(value, startDate, endDate);
            getTags(value, startDate, endDate);
            getBillsbyTags(value, startDate, endDate);
            TagsBarChart(value, startDate, endDate);
            ConsumedServiceChart(value, startDate, endDate);
            ClientCostFromTags(value, startDate, endDate, Tag);
        }

        protected void hfValue_ValueChanged(object sender, EventArgs e)
        {
        }

        string getTotalCost(string pname, string sdate, string edate)
        {
            List<object> data = new List<object>();
            string query;

            if (sdate != "" && edate != "")
            {
                query = "Select Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' and Date >= '" + sdate + "' and Date <= '" + edate + "'";
            }
            else
            {
                query = "Select Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "'";
            }

            DataTable dtDataItemsSets = ExecuteQuery(query);
            DataRow dt = dtDataItemsSets.Rows[0];
            string val = dt["Cost"].ToString();

            if (pname != "TLX-Internal")
                lblTotal.Text = "$" + val;

            return val;

        }

        void getTags(string pname, string sdate, string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select Tags from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' and Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Tags";
            }
            else
            {
                query = "Select Tags from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' Group by Tags";
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

        void getBillsbyTags(string pname, string sdate, string edate)
        {
            List<object> data = new List<object>();
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' and Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Tags";
            }
            else
            {
                query = "Select SUM(Cast(dbo.ReportData.Cost as float)) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' Group by Tags";
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

        void TagsBarChart(string pname, string sdate, string edate)
        {
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select Tags, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' and Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Tags Order by Cost Asc";
            }
            else
            {
                query = "Select Tags, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' Group by Tags Order by Cost Asc";
            }
            SqlDataSource1.SelectCommand = query;
            TagChart.ChartAreas[0].AxisX.Interval = 1;
            TagChart.Series[0].ToolTip = "#VALX : $ #VALY";
            TagChart.Series["Series1"].Label = "$ " + "#VALY";
        }

        void ConsumedServiceChart(string pname, string sdate, string edate)
        {
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select ConsumedService, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' and Date >= '" + sdate + "' and Date <= '" + edate + "' Group by ConsumedService Order by Cost Asc";
            }
            else
            {
                query = "Select ConsumedService, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' Group by ConsumedService Order by Cost Asc";
            }
            SqlDataSource2.SelectCommand = query;
            CServiceChart.ChartAreas[0].AxisX.Interval = 1;
            CServiceChart.Series["Series1"].Label = "$ " + "#VALY";
            CServiceChart.Series[0].ToolTip = "#VALX : $ #VALY";
        }

        void ClientCostFromTags(string pname, string sdate, string edate, string mtag)
        {
            string query;
            if (sdate != "" && edate != "")
            {
                query = "Select Tags, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' and Date >= '" + sdate + "' and Date <= '" + edate + "' Group by Tags Order by Tags Asc";
            }
            else
            {
                query = "Select Tags, Round(SUM(Cast(dbo.ReportData.Cost as float)),2) as Cost from dbo.ReportData where dbo.ReportData.Subscriptionname = '" + pname + "' Group by Tags Order by Tags Asc";
            }
            DataTable dtDataItemsSets = ExecuteQuery(query);

            if (dtDataItemsSets.Rows.Count > 0)
            {
                //getting Tlx Internal Cost
                double tlxCost = 0;
                string tempCost = getTotalCost("TLX-Internal", "", "");
                if (tempCost != "")
                {
                    tlxCost = double.Parse(tempCost);
                    tlxCost /= 3;
                }

                int prodClients = 0, preClients = 0, sharedClients = 0;

                //Clients Table
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Client", typeof(string));
                dataTable.Columns.Add("Production", typeof(string));
                dataTable.Columns.Add("Preprod", typeof(string));
                dataTable.Columns.Add("Total", typeof(string));

                //Servers Table
                DataTable TagTable = new DataTable();
                TagTable.Columns.Add(mtag + ": Production", typeof(string));
                TagTable.Columns.Add(mtag + ": Preprod", typeof(string));
                TagTable.Columns.Add(mtag + ": Shared All", typeof(string));
                TagTable.Columns.Add("Total", typeof(string));

                //Getting Values and Creating Grids
                for (int i = 0; i < dtDataItemsSets.Rows.Count; i++)
                {
                    DataRow dr = dtDataItemsSets.Rows[i];
                    string tag = dr["Tags"].ToString();
                    string client = "", env = "";
                    if (tag != "")
                    {
                        string[] values = tag.Split(':');
                        client = values[0];
                        env = values[1];
                    }
                    int index = 0;
                    if (tag != "" && client != mtag)
                    {
                        index = CheckClientInList(dataTable, client);
                        if (index == -1)
                        {
                            DataRow row = dataTable.NewRow();
                            row["Client"] = client;
                            if (env == " Production")
                            {
                                row["Production"] = "$ " + dr["Cost"].ToString();
                                row["Preprod"] = "-";
                                prodClients++;
                            }
                            else
                            {
                                row["Production"] = "-";
                                row["Preprod"] = "$ " + dr["Cost"].ToString();
                                preClients++;
                            }
                            sharedClients++;
                            row["Total"] = "$ " + dr["Cost"].ToString();
                            dataTable.Rows.Add(row);
                        }
                        else
                        {
                            if (env == " Production")
                            {
                                dataTable.Rows[index]["Production"] = "$ " + dr["Cost"].ToString();
                                prodClients++;
                            }
                            else
                            {
                                dataTable.Rows[index]["Preprod"] = "$ " + dr["Cost"].ToString();
                                preClients++;
                            }

                            string[] value;
                            double prod = 0, pre = 0;

                            if (dataTable.Rows[index]["Production"].ToString() != "-")
                            {
                                value = dataTable.Rows[index]["Production"].ToString().Split(' ');
                                prod = Double.Parse(value[1]);
                            }
                            if (dataTable.Rows[index]["Preprod"].ToString() != "-")
                            {
                                value = dataTable.Rows[index]["Preprod"].ToString().Split(' ');
                                pre = Double.Parse(value[1]);
                            }
                            dataTable.Rows[index]["Total"] = "$ " + (prod + pre).ToString();
                            sharedClients++;
                        }
                    }
                    else
                    {
                        if (TagTable.Rows.Count == 0)
                        {
                            DataRow Trow = TagTable.NewRow();
                            if (env == " Production")
                            {
                                Trow[mtag + ": Production"] = "$ " + dr["Cost"].ToString();
                                Trow[mtag + ": Preprod"] = "$ " + 0;
                                Trow[mtag + ": Shared All"] = "$ " + 0;
                            }
                            else if (env == " Preprod")
                            {
                                Trow[mtag + ": Production"] = "$ " + 0;
                                Trow[mtag + ": Preprod"] = "$ " + dr["Cost"].ToString();
                                Trow[mtag + ": Shared All"] = "$ " + 0;
                            }
                            else if (env == " Shared All" || env == "Shared+All" || env == "")
                            {
                                Trow[mtag + ": Production"] = "$ " + 0;
                                Trow[mtag + ": Preprod"] = "$ " + 0;
                                Trow[mtag + ": Shared All"] = "$ " + dr["Cost"].ToString();
                            }
                            Trow["Total"] = "$ " + dr["Cost"].ToString();
                            TagTable.Rows.Add(Trow);
                        }
                        else
                        {
                            if (env == " Production")
                            {
                                TagTable.Rows[TagTable.Rows.Count - 1][mtag + ": Production"] = "$ " + dr["Cost"].ToString();
                            }
                            else if (env == " Preprod")
                            {
                                TagTable.Rows[TagTable.Rows.Count - 1][mtag + ": Preprod"] = "$ " + dr["Cost"].ToString();
                            }
                            else if (env == " Shared All" || env == " Shared+All" || env == "" || env == " SharedAll")
                            {
                                string[] val = TagTable.Rows[TagTable.Rows.Count - 1][mtag + ": Shared All"].ToString().Split(' ');
                                double curr = Double.Parse(val[1]);
                                TagTable.Rows[TagTable.Rows.Count - 1][mtag + ": Shared All"] = "$ " + (Double.Parse(dr["Cost"].ToString()) + curr).ToString();
                            }
                        }
                    }
                } //end of for loop


                //Calculating Total & adding Tlx Cost
                string[] Totalvalue = TagTable.Rows[TagTable.Rows.Count - 1][mtag + ": Production"].ToString().Split(' ');
                double tempprod = Double.Parse(Totalvalue[1]);
                Totalvalue = TagTable.Rows[TagTable.Rows.Count - 1][mtag + ": Preprod"].ToString().Split(' ');
                double temppre = Double.Parse(Totalvalue[1]);
                Totalvalue = TagTable.Rows[TagTable.Rows.Count - 1][mtag + ": Shared All"].ToString().Split(' ');
                double tempshared = Double.Parse(Totalvalue[1]);
                tempshared = tempshared + tlxCost;
                TagTable.Rows[TagTable.Rows.Count - 1][mtag + ": Shared All"] = "$ " + (Math.Round(tempshared, 2)).ToString();
                TagTable.Rows[TagTable.Rows.Count - 1]["Total"] = "$ " + (Math.Round((tempprod + temppre + tempshared), 2)).ToString();


                double prodvalue = 0, prevalue = 0, sharedvalue = 0;
                string[] temp;

                if (mtag != "")
                {
                    //Calculating Each Server Cost Per Client
                    DataRow valrow = TagTable.NewRow();
                    for (int i = 0; i < TagTable.Columns.Count; i++)
                    {
                        if (TagTable.Columns[i].ToString() == mtag + ": Production")
                        {
                            temp = TagTable.Rows[0][mtag + ": Production"].ToString().Split(' ');
                            prodvalue = Double.Parse(temp[1]);
                            prodvalue /= prodClients;
                            prodvalue = Math.Round(prodvalue, 2);
                            valrow[mtag + ": Production"] = "$ " + prodvalue.ToString() + " / client";
                        }
                        else if (TagTable.Columns[i].ToString() == mtag + ": Preprod")
                        {
                            temp = TagTable.Rows[0][mtag + ": Preprod"].ToString().Split(' ');
                            prevalue = Double.Parse(temp[1]);
                            prevalue /= preClients;
                            prevalue = Math.Round(prevalue, 2);
                            valrow[mtag + ": Preprod"] = "$ " + prevalue.ToString() + " / client";
                        }
                        else if (TagTable.Columns[i].ToString() == mtag + ": Shared All")
                        {
                            temp = TagTable.Rows[0][mtag + ": Shared All"].ToString().Split(' ');
                            sharedvalue = Double.Parse(temp[1]);
                            sharedvalue /= sharedClients;
                            sharedvalue = Math.Round(sharedvalue, 2);
                            valrow[mtag + ": Shared All"] = "$ " + sharedvalue.ToString() + " / client";
                        }
                        valrow["Total"] = "";
                    }
                    TagTable.Rows.Add(valrow);


                    //Adding Cost to the Clients Bill
                    double prodtotal, pretotal, finaltotal;
                    double sumProd = 0, sumPre = 0, sumTotal = 0;
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        prodtotal = 0;
                        pretotal = 0;
                        if (dataTable.Rows[i]["Production"].ToString() != "-")
                        {
                            temp = dataTable.Rows[i]["Production"].ToString().Split(' ');
                            prodtotal = Math.Round((Double.Parse(temp[1])), 2);
                            prodtotal = Math.Round((Double.Parse(temp[1]) + prodvalue), 2);
                            prodtotal = prodtotal + sharedvalue;
                            dataTable.Rows[i]["Production"] = "$ " + prodtotal.ToString();
                            sumProd += prodtotal;
                        }
                        if (dataTable.Rows[i]["Preprod"].ToString() != "-")
                        {
                            temp = dataTable.Rows[i]["Preprod"].ToString().Split(' ');
                            pretotal = Math.Round((Double.Parse(temp[1])), 2);
                            pretotal = Math.Round((Double.Parse(temp[1]) + prevalue), 2);
                            pretotal = pretotal + sharedvalue;
                            dataTable.Rows[i]["Preprod"] = "$ " + pretotal.ToString();
                            sumPre += pretotal;
                        }
                        finaltotal = prodtotal + pretotal;
                        dataTable.Rows[i]["Total"] = "$ " + finaltotal.ToString();
                        sumTotal += finaltotal;
                    }

                    //Adding Totals Row
                    DataRow frow = dataTable.NewRow();
                    frow["Client"] = "Total";
                    frow["Production"] = "$ " + sumProd.ToString();
                    frow["Preprod"] = "$ " + sumPre.ToString();
                    frow["Total"] = "$ " + sumTotal.ToString();
                    dataTable.Rows.Add(frow);

                } //end of if mtag

                //Binding Data
                gridTags.DataSource = dataTable;
                gridTags.DataBind();
                gridMainTag.DataSource = TagTable;
                gridMainTag.DataBind();

            } //end of Condition
        }

        int CheckClientInList(DataTable data, string client)
        {
            int val = -1;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i]["Client"].ToString() == client)
                {
                    val = i;
                }
            }
            return val;
        }

        public DataTable ExecuteQuery(string query)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AzureDBConnectionString"].ConnectionString);
            SqlDataAdapter dap = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            return ds.Tables[0];
        }

        protected void gridTags_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gridTags_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            if (gridTags.Rows.Count != 0)
            {
                for (int i = 0; i < gridTags.Rows[gridTags.Rows.Count - 1].Cells.Count; i++)
                {
                    if (i == 0)
                    {
                        gridTags.Rows[gridTags.Rows.Count - 1].Cells[i].HorizontalAlign = HorizontalAlign.Left;
                        string val = gridTags.Rows[gridTags.Rows.Count - 1].Cells[i].Text;
                    }
                    else
                        gridTags.Rows[gridTags.Rows.Count - 1].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }

                if (gridTags.Rows[gridTags.Rows.Count - 1].Cells[0].Text == "Total")
                {
                    for (int i = 0; i < gridTags.Rows[gridTags.Rows.Count - 1].Cells.Count; i++)
                        gridTags.Rows[gridTags.Rows.Count - 1].Cells[i].Font.Bold = true;
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

        protected void btnResource_Click1(object sender, EventArgs e)
        {
           // string csvpath = Path.Combine(HttpContext.Current.Server.MapPath("~"), "Database+App-Details-updated.ps1");
            RunScript(LoadScript(@"E:\Database+App-Details-updated.ps1"));
            //RunScript(LoadScript(csvpath));
        }

        private string RunScript(string scriptText)
        {
            // create Powershell runspace 
            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it 
            runspace.Open();

            RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);
            scriptInvoker.Invoke("Set-ExecutionPolicy -Scope CurrentUser Unrestricted");

            // create a pipeline and feed it the script text 
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);

            // add an extra command to transform the script output objects into nicely formatted strings 
            // remove this line to get the actual objects that the script returns. For example, the script 
            // "Get-Process" returns a collection of System.Diagnostics.Process instances. 
            pipeline.Commands.Add("Out-String");

            // execute the script 
            Collection<PSObject> results = pipeline.Invoke();

            // close the runspace 
            runspace.Close();

            // convert the script result into a single string 
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }

            // return the results of the script that has 
            // now been converted to text 
            return stringBuilder.ToString();
        }

        private string LoadScript(string filename)
        {
            try
            {
                // Create an instance of StreamReader to read from our file. 
                // The using statement also closes the StreamReader. 
                using (StreamReader sr = new StreamReader(filename))
                {

                    // use a string builder to get all our lines from the file 
                    StringBuilder fileContents = new StringBuilder();

                    // string to hold the current line 
                    string curLine;

                    // loop through our file and read each line into our 
                    // stringbuilder as we go along 
                    while ((curLine = sr.ReadLine()) != null)
                    {
                        // read each line and MAKE SURE YOU ADD BACK THE 
                        // LINEFEED THAT IT THE ReadLine() METHOD STRIPS OFF 
                        fileContents.Append(curLine + "\n");
                    }

                    // call RunScript and pass in our file contents 
                    // converted to a string 
                    return fileContents.ToString();
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong. 
                string errorText = "The file could not be read:";
                errorText += e.Message + "\n";
                return errorText;
            }

        }
    }
}