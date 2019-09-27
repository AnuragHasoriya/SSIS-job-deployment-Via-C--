using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Jobs.Deployment
{
    public class SqlHelper
    {
        public static SqlConnection GetConnection()
        {
            if (connectionDBName == null)
                return new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            else
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                string user = builder.UserID;
                string pass = builder.Password;
                string credentials = user != "" && pass != "" ? (";User=" + user + "; Password = " + pass) : ";Integrated Security=true;";
                return new SqlConnection("Data Source=" + builder.DataSource + ";MultipleActiveResultSets=True;Initial Catalog=" + connectionDBName + credentials);
            }
        }
        public static object connectionDBName { get; set; }
    }
}
