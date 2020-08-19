using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Dapper.Extension
{
    public static class DbConnectionExtension
    {
        public static void OpenConnection(this IDbConnection dbConnection)
        {
            try
            {
                if (dbConnection?.State == ConnectionState.Closed || dbConnection?.State == ConnectionState.Broken)
                {
                    dbConnection?.Open();
                }
            }
            catch
            {
                throw;
            }
        }

        public static void CloseConnection(this IDbConnection dbConnection)
        {
            try
            {
                if (dbConnection?.State == ConnectionState.Open)
                {
                    dbConnection?.Close();
                    dbConnection.Dispose();
                    dbConnection = null;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}