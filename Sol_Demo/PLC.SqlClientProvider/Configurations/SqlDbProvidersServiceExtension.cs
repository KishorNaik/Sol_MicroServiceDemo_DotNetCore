using Microsoft.Extensions.DependencyInjection;
using PLC.Dapper.Helpers;
using PLC.SqlClientProvider.Clients;
using PLC.SqlClientProvider.Cores;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.SqlClientProvider.Configurations
{
    public static class SqlDbProvidersServiceExtension
    {
        public static void AddSqlClientDbProvider(this IServiceCollection services, string uatConnectionString, string productionConnectionString, bool? isProduction)
        {
            try
            {
                services.AddTransient<ISqlClientDbProviders, SqlClientDbProviders>((leServiceProvide) =>
                {
                    return new SqlClientDbProviders((isProduction == true) ? productionConnectionString : uatConnectionString);
                });
            }
            catch
            {
                throw;
            }
        }
    }
}