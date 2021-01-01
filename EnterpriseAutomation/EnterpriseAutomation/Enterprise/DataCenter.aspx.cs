using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using EnterpriseAutomation.lumino;

namespace EnterpriseAutomation.Enterprise
{
    public partial class DataCenter : System.Web.UI.Page
    {
        string startdate = "";
        string enddate = "";
        DataTable dtCSV;
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentDataDates("start");
            CurrentDataDates("end");
            Main main = new Main();
            if(main.IsUserReadOnly())
            {
                btnImport.Visible = false;
                btnClear.Visible = false;
            }
            getTagsList();
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            startdate = hfstartDate.Value.ToString();
            enddate = hfendDate.Value.ToString();
            if (Convert.ToDateTime(startdate) <= Convert.ToDateTime(enddate))
            {
                if (startdate != "" || enddate != "")
                {
                    const string GetUsageByMonthUrl = "https://consumption.azure.com/v3/enrollments/{0}/{1}/download?startTime={2}&endTime={3}";
                    const string GetUsageListUrl = "https://consumption.azure.com/v3/enrollments/{0}/usagedetails";
                    string EnrollmentNumber = "63898228";
                    string AccessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IlhYeWstcjBONXlVb3dkeVh0c29zMmhsTXFOUSJ9.eyJFbnJvbGxtZW50TnVtYmVyIjoiNjM4OTgyMjgiLCJJZCI6IjM3MjFhNzQ5LWJiZmQtNGNiZS05NjBhLTNiMmNjNTI3NTgzOCIsIlJlcG9ydFZpZXciOiJJbmRpcmVjdEVudGVycHJpc2UiLCJQYXJ0bmVySWQiOiIiLCJEZXBhcnRtZW50SWQiOiIiLCJBY2NvdW50SWQiOiIiLCJpc3MiOiJlYS5taWNyb3NvZnRhenVyZS5jb20iLCJhdWQiOiJjbGllbnQuZWEubWljcm9zb2Z0YXp1cmUuY29tIiwiZXhwIjoxNjE1MzU0OTU5LCJuYmYiOjE1OTk3MTY1NTl9.ltAbpAReUJAV_G4grVRiGcCutK4rTvqjOePEhWjViCqh36GRyPpfbh6VuqAO3RNnz1-ZbgG8m_5PrpizlMkTV4UbYGDIGGxyWibWEwZm9V2RVU7bg8QL_BkSgWGLYgy6pgSUBimV6BgMWXOo5vnbuGR6WrBeRUVBFMr4w8RXdV5z3Fr0CCdG0Oz490AoFsz_WTiQ_KAGUIt1ExEfHF1CNcwNDPCsZbkGISk9ImHZXU2CD2_H5v820Pvr3RbezX1LcYp98FgSJhNVrw1FkC5fHA9NrbOJ-0Gkg9_LwdSu5xIqzRNwfOF-lF-ygSMpujahpCduQ1oHWezH8609d2FhsQ";
                    //"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IlhYeWstcjBONXlVb3dkeVh0c29zMmhsTXFOUSJ9.eyJFbnJvbGxtZW50TnVtYmVyIjoiNjM4OTgyMjgiLCJJZCI6IjY5NDMzOTY3LTQ4OTctNDZjZi05NzY5LTdiZGFjNjdmYjNkMyIsIlJlcG9ydFZpZXciOiJJbmRpcmVjdEVudGVycHJpc2UiLCJQYXJ0bmVySWQiOiIiLCJEZXBhcnRtZW50SWQiOiIiLCJBY2NvdW50SWQiOiIiLCJpc3MiOiJlYS5taWNyb3NvZnRhenVyZS5jb20iLCJhdWQiOiJjbGllbnQuZWEubWljcm9zb2Z0YXp1cmUuY29tIiwiZXhwIjoxNTk0MTA4OTI0LCJuYmYiOjE1NzgzODQxMjR9.KoDdX0-H5HeNd75ShB2TupE4xDtwFmypw2cEFc8MbMI7rwufovO-3gXb8PB6X5myh_1TLb4bDvCgSJext5HqeKo2FEmA5_1a9S5htoiS8NkcA_QVBAB8dsXsKwVrrx3cupSt6NFmjIb6E2GRPhY39Sq381ME7nH0CyA67iFxm0KI6ruGJIxnImH4jI87pwURNdoZ2qggTKqnRykb6NOwBeYG0FfYrZdI2i5y_Hhu3j4KBB2IXs5ZVuVoUl-ba2BT6JLJy6z25xpWCCXblVKj0xgWUNTopVOwGm0GJWRiMRDtv-dGxX--7oZl1Wk0-_CtU5Zd3ji-GKFDhE3hDaGQSQ";
                    // Retrieve a list of available reports
                    string Url = string.Format(GetUsageListUrl, EnrollmentNumber);
                    //Directly download a monthly summary report,
                    // Directly download a monthly detail report,
                    Console.WriteLine("----------Directly download a monthly usage report--------------");
                    Url = string.Format(GetUsageByMonthUrl, EnrollmentNumber, "usagedetails", startdate, enddate);
                    string DetailUsageCSV = CallRestAPI(Url, AccessToken);
                    //Console.WriteLine(DetailUsageCSV);
                    WriteToFile(DetailUsageCSV, "usagedetails");
                    Console.WriteLine("Usage Detail Downloaded");
                    ImportToDatabase();
                    // MessageBox.Show("Data has been Retreived Successfully");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Select Start and End Date";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Start Date cannot be less than End Date";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
            }
        }

        public static string CallRestAPI(string url, string token)
        {
            WebRequest request = WebRequest.Create(url);
            request.Headers.Add("authorization", "bearer " + token);
            request.Headers.Add("api-version", "1.0");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }

        public static void WriteToFile(string data, string filename)
        {
            StringBuilder csvcontent = new StringBuilder();
            csvcontent.AppendLine(data);
            //Guid Guid = new Guid();
            string csvpath = Path.Combine(HttpContext.Current.Server.MapPath("~"),"temp.csv");
            
            //File.Create(csvpath);
            File.WriteAllText(csvpath, csvcontent.ToString());
        }

        public void ImportToDatabase()
        {
            DataTable dtable = new DataTable();
            dtable.Columns.AddRange(new DataColumn[40]
                {
                    new DataColumn(("AccountId"),typeof(string)),
                    new DataColumn(("AccountName"),typeof(string)),
                    new DataColumn(("AccountOwnerEmail"),typeof(string)),
                    new DataColumn(("AdditionalInfo"),typeof(string)),
                    new DataColumn(("ConsumedQuantity"),typeof(string)),
                    new DataColumn(("ConsumedService"),typeof(string)),
                    new DataColumn(("ConsumedServiceId"),typeof(string)),
                    new DataColumn(("Cost"),typeof(string)),
                    new DataColumn(("CostCenter"),typeof(string)),
                    new DataColumn(("Date"),typeof(string)),
                    new DataColumn(("DepartmentId"),typeof(string)),
                    new DataColumn(("DepartmentName"),typeof(string)),
                    new DataColumn(("InstanceId"),typeof(string)),
                    new DataColumn(("MeterCategory"),typeof(string)),
                    new DataColumn(("MeterId"),typeof(string)),
                    new DataColumn(("MeterName"),typeof(string)),
                    new DataColumn(("MeterRegion"),typeof(string)),
                    new DataColumn(("MeterSubCategory"),typeof(string)),
                    new DataColumn(("Product"),typeof(string)),
                    new DataColumn(("ProductId"),typeof(string)),
                    new DataColumn(("ResourceGroup"),typeof(string)),
                    new DataColumn(("ResourceLocation"),typeof(string)),
                    new DataColumn(("ResourceLocationId"),typeof(string)),
                    new DataColumn(("ResourceRate"),typeof(string)),
                    new DataColumn(("ServiceAdministratorId"),typeof(string)),
                    new DataColumn(("ServiceInfo1"),typeof(string)),
                    new DataColumn(("ServiceInfo2"),typeof(string)),
                    new DataColumn(("StoreServiceIdentifier"),typeof(string)),
                    new DataColumn(("SubscriptionGuid"),typeof(string)),
                    new DataColumn(("SubscriptionId"),typeof(string)),
                    new DataColumn(("SubscriptionName"),typeof(string)),
                    new DataColumn(("Tags"),typeof(string)),
                    new DataColumn(("UnitOfMeasure"),typeof(string)),
                    new DataColumn(("PartNumber"),typeof(string)),
                    new DataColumn(("ResourceGuid"),typeof(string)),
                    new DataColumn(("OfferId"),typeof(string)),
                    new DataColumn(("ChargesBilledSeparately"),typeof(string)),
                    new DataColumn(("Location"),typeof(string)),
                    new DataColumn(("ServiceName"),typeof(string)),
                    new DataColumn(("ServiceTier"),typeof(string)),
                });
            string csvpath = Path.Combine(HttpContext.Current.Server.MapPath("~"), "temp.csv");
            string data = File.ReadAllText(csvpath);
            int j = 1;
            int count = 0;
            //    string AdditionalInfo;
            foreach (string row in data.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    if (j > 3)
                    {
                        dtable.Rows.Add();
                        string[] temp = row.Split(',');
                        int i = 0;
                        if (temp.Length > 40)
                        {
                            string[] start = row.Split('{');
                            if (start.Length > 1)
                            {
                                string[] end = start[1].Split('}');
                                for (int l = 0; l <= start.Length; l++)
                                {
                                    if (l == 0)
                                    {
                                        foreach (string cell in start[0].Split(','))
                                        {
                                            string text = "\"";
                                            if (cell != text)
                                            {
                                                if (cell == "")
                                                    dtable.Rows[dtable.Rows.Count - 1][i] = null;
                                                else
                                                    dtable.Rows[dtable.Rows.Count - 1][i] = cell;
                                                i++;
                                            }
                                        }
                                    }
                                    else if (l == 1)
                                    {
                                        dtable.Rows[dtable.Rows.Count - 1][i] = end[0];
                                        i++;
                                    }
                                    else if (l == 2)
                                    {
                                        foreach (string cell in end[1].Split(','))
                                        {
                                            string text = "\"";
                                            if (cell != text)
                                            {
                                                if (cell == "")
                                                {
                                                    if (i == 31)
                                                        dtable.Rows[dtable.Rows.Count - 1][i] = getTag(dtable.Rows[dtable.Rows.Count - 1][12].ToString());
                                                    else
                                                        dtable.Rows[dtable.Rows.Count - 1][i] = null;
                                                }
                                                else
                                                {
                                                    if (i == 31)
                                                        dtable.Rows[dtable.Rows.Count - 1][i] = CorrectTagFormat(cell);
                                                    else
                                                        dtable.Rows[dtable.Rows.Count - 1][i] = cell;
                                                }
                                                i++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string[] check = start[2].Split(',');
                                        if (check.Length > 9)
                                        {
                                            string[] tag = start[2].Split('}');
                                            if (tag[0].Contains("BigBird"))
                                            {
                                                dtable.Rows[dtable.Rows.Count - 1][i] = "SD : Production";
                                                // string s = dtable.Rows[dtable.Rows.Count - 1][i].ToString();
                                            }
                                            else
                                                dtable.Rows[dtable.Rows.Count - 1][i] = "AN : Production";

                                            i++;



                                            foreach (string cell in tag[1].Split(','))
                                            {
                                                string text = "\"";
                                                if (cell != text)
                                                {
                                                    if (cell == "")
                                                        dtable.Rows[dtable.Rows.Count - 1][i] = null;
                                                    else
                                                        dtable.Rows[dtable.Rows.Count - 1][i] = cell;
                                                    i++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (string cell in start[2].Split(','))
                                            {
                                                string text = "\"";
                                                if (cell != text)
                                                {
                                                    if (cell == "")
                                                    {
                                                        if (i == 31)
                                                            dtable.Rows[dtable.Rows.Count - 1][i] = getTag(dtable.Rows[dtable.Rows.Count - 1][12].ToString());
                                                        else
                                                            dtable.Rows[dtable.Rows.Count - 1][i] = null;
                                                    }
                                                    else
                                                    {
                                                        if (i == 31)
                                                            dtable.Rows[dtable.Rows.Count - 1][i] = CorrectTagFormat(cell);
                                                        else
                                                            dtable.Rows[dtable.Rows.Count - 1][i] = cell;
                                                    }
                                                    i++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                                 count = count + 1;
                        }
                        else
                        {
                            foreach (string cell in row.Split(','))
                            {
                                if (cell == "")
                                {
                                    if (i == 31)
                                        dtable.Rows[dtable.Rows.Count - 1][i] = getTag(dtable.Rows[dtable.Rows.Count - 1][12].ToString());
                                    else
                                        dtable.Rows[dtable.Rows.Count - 1][i] = null;
                                }
                                else
                                {
                                    if (i == 31)
                                        dtable.Rows[dtable.Rows.Count - 1][i] = CorrectTagFormat(cell);
                                    else
                                        dtable.Rows[dtable.Rows.Count - 1][i] = cell;
                                }
                                i++;
                            }
                        }
                    }
                    else
                        j++;
                }
            }

            if (AddIntoDatabase(dtable))
            {
                // File.Delete("temp.csv");
                lblMsg.Visible = true;
                lblMsg.Text = "Data Added Successfully..!!!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                CurrentDataDates("start");
                CurrentDataDates("end");
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Data Addition Failed..!!!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
            }
        }

        public bool AddIntoDatabase(DataTable data)
        {
            try
            {
                string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["AzureDBConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    using (SqlBulkCopy sqlbkcopy = new SqlBulkCopy(conn))
                    {
                        sqlbkcopy.DestinationTableName = "dbo.ReportData";
                        conn.Open();
                        sqlbkcopy.WriteToServer(data);
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        void CurrentDataDates(string time)
        {
            List<object> data = new List<object>();
            string query;
           
            if(time == "start")
            {
                query = "select top(1) Date from dbo.ReportData where Date != 'NULL' group by Date order by Date Asc";
            }
            else 
            {
                query = "select top(1) Date from dbo.ReportData where Date != 'NULL' group by Date order by Date Desc";
            }
            try
            {
                DataTable dtDataItemsSets = ExecuteQuery(query);
                DataRow dt = dtDataItemsSets.Rows[0];
                string val = dt["Date"].ToString();
                if (time == "start")
                    lblFrom.Text = val;
                else
                    lblTo.Text = val;
            }
            catch (Exception ex)
            {
                lblFrom.Text = "No Data";
                lblTo.Text = "No Data";
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if(ClearDatabase())
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Data Cleared Successfully..!!!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Data Deletion Failed..!!!";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
            }
        }

        bool ClearDatabase()
        {
            try
            {
                string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["AzureDBConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    string query = "DELETE FROM dbo.ReportData";
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
            catch (Exception ex)
            {
                return false;
            }
        }

        void getTagsList()
        {
            using (StreamReader sr = new StreamReader(Path.Combine(HttpContext.Current.Server.MapPath("~"), "Tags.csv")))
            {
                dtCSV = new DataTable();
                dtCSV.Columns.AddRange(new DataColumn[2]
                {
                    new DataColumn(("InstanceId"),typeof(string)),
                    new DataColumn(("Tag"),typeof(string))
                });

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dtCSV.NewRow();
                    for (int i = 0; i < rows.Count(); i++)
                    {
                        dr[i] = rows[i];
                    }
                    dtCSV.Rows.Add(dr);
                }
            }
        }//end of getTagsList Function


        string getTag(string instanceId)
        {
            string tag = "";
            for (int i = 0; i < dtCSV.Rows.Count; i++)
            {
                string cid = dtCSV.Rows[i][0].ToString();
                if (String.Equals(cid, instanceId, StringComparison.OrdinalIgnoreCase))
                {
                    tag = dtCSV.Rows[i][1].ToString();
                }
            }

            if (tag != "")
                tag = CorrectTagFormat(tag);
            else
                tag = tag + "";

            return tag;
        }//end of getTag Function



        string CorrectTagFormat(string Tag)
        {
            var charsToRemove = new string[] { "@", ",", ".", ";", " ", "\"", "/", "{", "}" };
            foreach (var c in charsToRemove)
            {
                Tag = Tag.Replace(c, string.Empty);
            }
            string[] temp = Tag.Split(':');
            Tag = temp[0] + " " + ":" + " " + temp[1];

            return Tag;
        }//end or CorrectTagFormat Function

        public DataTable ExecuteQuery(string query)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AzureDBConnectionString"].ConnectionString);
            SqlDataAdapter dap = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            return ds.Tables[0];
        }
    }
}