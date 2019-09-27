using Microsoft.SqlServer.Management.IntegrationServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Jobs.Deployment
{
    public class Program
    {
        static void Main(string[] args)
        {
            string targetFolderName = "dailyClosedJobs";
            string projectName = "dailyClosedJobs.dtsx";
            string projectFilePath = @"" + ConfigurationManager.AppSettings["FolderPath"];

            try
            {
                // Create a connection to the server// Create the Integration Services object
                string conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conStr);
                using (var con = sqlConnection)
                {
                    IntegrationServices integrationServices = new IntegrationServices(con);

                    // Get the Integration Services catalog
                    Catalog catalog = integrationServices.Catalogs["SSISDB"];

                    // Create the target folder
                    CatalogFolder folder = new CatalogFolder(catalog,
                        targetFolderName, "Folder description");

                    Console.WriteLine("Deploying " + projectName + " project.");

                    byte[] projectFile = File.ReadAllBytes(projectFilePath);
                    folder.DeployProject(projectName, projectFile);

                    Console.WriteLine("Done.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
