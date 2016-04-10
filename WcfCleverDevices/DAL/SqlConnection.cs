using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WcfService3.Sql;

namespace WcfService3.DAL
{
    public class SqlConnection : ISqlConnection
    {
        string conn;

        public SqlConnection()
        {
            conn = WebConfigurationManager.AppSettings["ConnectionString"];
        }

        public IDbConnection Connect()
        {
            SqlConnectionFactory sql = new SqlConnectionFactory();            
            return sql.CreateConnection(conn);
        }
    }
}