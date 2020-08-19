using Microsoft.Extensions.DependencyInjection;
using PLC.Admin.Api.Applications.ApiCommands;
using PLC.Admin.Api.Cores.Applications.ApiCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Configurations.Services
{
    public static class ApiCommandServiceExtension
    {
        public static void AddApiCommandConfig(this IServiceCollection services)
        {
            services.AddTransient<IAddAdminApiCommandHandler, AddAdminApiCommandHandler>();
            services.AddTransient<IGetAllAdminApiCommandHandler, GetAllAdminApiCommandHandler>();
        }
    }
}