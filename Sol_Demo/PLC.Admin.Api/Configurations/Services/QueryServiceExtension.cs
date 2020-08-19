using Microsoft.Extensions.DependencyInjection;
using PLC.Admin.Api.Business.Query;
using PLC.Admin.Api.Cores.Business.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Configurations.Services
{
    public static class QueryServiceExtension
    {
        public static void AddQueryConfig(this IServiceCollection services)
        {
            services?.AddTransient<IGetAllAdminQuery, GetAllAdminQuery>();
        }
    }
}