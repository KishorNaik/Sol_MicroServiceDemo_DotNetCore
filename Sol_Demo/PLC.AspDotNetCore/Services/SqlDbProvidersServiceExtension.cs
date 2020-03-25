using Microsoft.Extensions.DependencyInjection;
using PLC.AppSetting;
using PLC.Dapper.Core;
using PLC.Dapper.Core.DbProviders;
using PLC.Dapper.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.AspDotNetCore.Services
{
    public static class SqlDbProvidersServiceExtension
    {
        public static void AddSqlClientDbProvider(this IServiceCollection services,string uatConnectionString, string productionConnectionString)
        {
            try
            {
                services.AddTransient<ISqlClientDbProviders, SqlClientDbProviders>((leServiceProvide) =>
                {
                    return new SqlClientDbProviders((Convert.ToBoolean(AppResource.IsProduction) == true) ? productionConnectionString : uatConnectionString);
                });
            }
            catch
            {
                throw;
            }
        }
    }
}
