using Microsoft.Extensions.DependencyInjection;
using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.Admin.Api.Infrastructures.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Configurations.Services
{
    public static class RepositoryServiceExtension
    {
        public static void AddRepositoryConfig(this IServiceCollection services)
        {
            services?.AddTransient<IAddAdminRepository, AddAdminRepository>();
            services.AddTransient<IGetAllAdminRepository, GetAllAdminRepository>();
        }
    }
}