using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnterpriseAutomation.lumino.appcode;
using System.Data;
using System.Data.SqlClient;

namespace EnterpriseAutomation.lumino
{
    public partial class Main : System.Web.UI.Page
    {
        static string name;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            UserLogin login = new UserLogin();
            if (login.CheckUser(txtUsername.Value.ToString(), txtPass.Value.ToString()))
            {
                name = txtUsername.Value.ToString();
                Response.Redirect("Dashboard.aspx");

            }
            else
            {
                lblError.Visible = true;
                txtPass.Value = "";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblError.ClientID + "').style.display='none'\",2000)</script>");
            }
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
        }

        public bool IsUserReadOnly()
        {
            string query;
            bool value = false;
            query = "Select ReadOnly from dbo.Login Where Email = '"+name+"'";
            try
            {
                DataTable dtDataItemsSets = ExecuteQuery(query);
                DataRow dt = dtDataItemsSets.Rows[0];
                string val = dt["ReadOnly"].ToString();
                value = Convert.ToBoolean(val);
            }
            catch
            {

            }
            return value;
        }

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