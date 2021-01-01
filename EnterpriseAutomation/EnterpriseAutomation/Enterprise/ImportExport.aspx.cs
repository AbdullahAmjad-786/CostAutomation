using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Storage.Fluent;
using System.Threading.Tasks;

namespace EnterpriseAutomation.Enterprise
{
    public partial class ImportExport : System.Web.UI.Page
    {
        IAzure azure;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.GetPostBackEventReference(this, string.Empty);
        }

        protected void Page_Update(object sender, EventArgs e)
        {
           
        }

        protected void hfSubscription_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < subscriptionValue.Items.Count; i++)
            {
                if (subscriptionValue.Items[i].Value == hfSubscription.Value)
                    subscriptionValue.SelectedIndex = i;
            }
            //productName.InnerText = hfSubscription.Value;
            Console.WriteLine("Value Changed to:  " + hfSubscription.Value);
            if (hfSubscription.Value != "")
            {
                SetConnection();
                getServersList();
            }
            else
            {
                serverValue.Items.Clear();
                databaseValue.Items.Clear();
            }
        }

        protected void SetConnection()
        {
            try
            {
                string subscriptionId = "";

                if (hfSubscription.Value == "Almusnet")
                    subscriptionId = "cac601c4-4a4f-4aa3-939e-2923e23b1cd6";
                else if (hfSubscription.Value == "Vicenna")
                    subscriptionId = "565af59d-9511-40cd-811d-ce91ce902eb5";
                else if (hfSubscription.Value == "TMX")
                    subscriptionId = "3c3b8913-a968-4f0c-8d13-d386611bda0b";

                //=================================================================
                // Authenticate
                // var credentials = SdkContext.AzureCredentialsFactory.FromFile(Environment.GetEnvironmentVariable("AZURE_AUTH_LOCATION"));
                var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal("881879b4-3847-4255-be6a-ecafb36cefb2", "U852tQ7VwhtDfVY@mCe.E/JSIfxqc]Vl", "31f19eca-2e1f-48ee-9936-358d20cafcde", AzureEnvironment.AzureGlobalCloud);


                azure = Azure
                    .Configure()
                    .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                    .Authenticate(credentials)
                    .WithSubscription(subscriptionId);

                // Print selected subscription
                Console.WriteLine("Selected subscription: " + azure.SubscriptionId);

                //RunSample(azure);
                //ExportDb(azure);
                //getServersList(azure);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
                // Utilities.Log(e.ToString())
            }
        }

        protected void getServersList()
        {
            try
            {
                databaseValue.Items.Clear();
                serverValue.Items.Clear();
                var sqlServer = azure.SqlServers.List();
                //var databases = sqlServer.ElementAt(1).Databases.List();
                serverValue.Items.Add("Select - Server");
                for (int i=0;i<sqlServer.Count();i++)
                {
                    serverValue.Items.Add(sqlServer.ElementAt(i).Name.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //Utilities.Log(e);
            }
        }

        protected void hfServers_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void hfCurrentServer_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < subscriptionValue.Items.Count; i++)
            {
                if (subscriptionValue.Items[i].Value == hfSubscription.Value)
                    subscriptionValue.SelectedIndex = i;
            }

            string server_name = hfCurrentServer.Value.ToString().Replace("\"", string.Empty);
            int o = serverValue.Items.Count;
            for (int i = 0; i < serverValue.Items.Count; i++)
            {
                if (serverValue.Items[i].Value == hfCurrentServer.Value)
                    serverValue.SelectedIndex = i;
            }

            if (hfCurrentServer.Value != "Select - Server")
            {
                SetConnection();
                getDatabasesList(server_name);
            }
            else
                databaseValue.Items.Clear();
        }

        protected void getDatabasesList(string serverName)
        {
            try
            {
                databaseValue.Items.Clear();
                var sqlServer = azure.SqlServers.List();
                int index = 0;
                for (int j = 0; j < sqlServer.Count(); j++)
                {
                    if (serverName == sqlServer.ElementAt(j).Name.ToString())
                    {
                        index = j;
                    }
                }

                var database = sqlServer.ElementAt(index).Databases.List();
                databaseValue.Items.Add("Select - Database");
                for (int i = 0; i < database.Count(); i++)
                {
                    if(database.ElementAt(i).Name.ToString() != "master")
                        databaseValue.Items.Add(database.ElementAt(i).Name.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //Utilities.Log(e);
            }
        }

        protected async void btnExport_Click(object sender, EventArgs e)
        {
            SetConnection();
            await ExportDbAsync();
        }

        public async Task ExportDbAsync()
        {
            try
            {
                var sqlServer = azure.SqlServers.GetByResourceGroup("AN-BI", "an-bi");
                //   Utilities.PrintSqlServer(sqlServer);


                var dbFromSample = sqlServer.Databases
                    .Get("an-rpt-test-db");
                // Utilities.PrintDatabase(dbFromSample);

                // ============================================================
                // Export a database from a SQL server created above to a new storage account within the same resource group.
                Console.WriteLine("Exporting a database from a SQL server created above to a new storage account within the same resource group.");

                var storageAccount = azure.StorageAccounts.GetByResourceGroup("AN-PREPROD", "ancloudops");
                if (storageAccount == null)
                {
                    Console.WriteLine("Storage Account Not Found");
                }
                else
                {
                   // ISqlDatabaseImportExportResponse exportDB;
                   /*Task.Run(async ()=>
                       {
                             exportDB = await dbFromSample.ExportTo(storageAccount, "rlmc", "an-rpt-test-db-10.bacpac")
                            .WithSqlAdministratorLoginAndPassword("anbiadmin", "AxBidTxMOxq13")
                            .ExecuteAsync();
                           //return exportDB;
                       });*/

                   ISqlDatabaseImportExportResponse exportedDB = await StartExport(dbFromSample, storageAccount);
                   Console.WriteLine(exportedDB.Status);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //Utilities.Log(e);
            }
        }

        public Task<ISqlDatabaseImportExportResponse> StartExport(ISqlDatabase db, IStorageAccount storage)
        {
             return Task.Run(async () =>
             {
                 ISqlDatabaseImportExportResponse exportedDB = await db.ExportTo(storage, "rlmc", "an-rpt-test-db-11.bacpac")
                .WithSqlAdministratorLoginAndPassword("anbiadmin", "AxBidTxMOxq13")
                .ExecuteAsync();
                 return exportedDB;
             });
        }
    }
}