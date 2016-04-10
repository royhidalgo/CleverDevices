using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WcfService3.Sql
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        public System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new ConnectionWithTransactionManagement(new SqlConnection(connectionString));
        }
    }
}
