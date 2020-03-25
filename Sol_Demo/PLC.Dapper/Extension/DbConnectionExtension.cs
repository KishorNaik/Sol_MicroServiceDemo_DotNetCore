using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Dapper.Extension
{
    public static class DbConnectionExtension
    {
        public static Task OpenConnectionAsync(this IDbConnection dbConnection)
        {

            return Task.Run(() =>
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
            });
            
        }

        public static Task CloseConnectionAsync(this IDbConnection dbConnection)
        {

             return Task.Run(() => {

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
            });

        }
    }
}
