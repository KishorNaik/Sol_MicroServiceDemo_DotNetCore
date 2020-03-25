using PLC.AppSetting;
using PLC.Dapper.Core;
using PLC.Dapper.Core.DbProviders;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Dapper.Helpers
{
    public class SqlClientDbProviders : ISqlClientDbProviders
    {

        private readonly String connectionString = null;

        public SqlClientDbProviders(String connectionString)
        {
            this.connectionString = connectionString;
        }


        Task<SqlConnection> IDbProviders<SqlConnection>.GetConnectionAsync()
        {
            try
            {
                return Task.FromResult<SqlConnection>(new SqlConnection(connectionString));
            }
            catch
            {
                throw;
            }
        }
    }
}
