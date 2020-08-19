using Microsoft.Extensions.DependencyInjection;
using PLC.AppSetting;
using PLC.Dapper.Core;
using PLC.Dapper.Core.DbProviders;
using PLC.Dapper.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.Configuration.Services
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