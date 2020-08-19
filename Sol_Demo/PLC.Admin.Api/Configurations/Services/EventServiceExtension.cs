using Microsoft.Extensions.DependencyInjection;
using PLC.Admin.Api.Business.Events;
using PLC.Admin.Api.Cores.Business.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Configurations.Services
{
    public static class EventServiceExtension
    {
        public static void AddEventConfig(this IServiceCollection services)
        {
            services?.AddTransient<IAdminDecrypteEventHandler, AdminDecrypteEventHandler>();
            services?.AddTransient<IAdminEncrypteListEventHandler, AdminEncrypteListEventHandler>();
        }
    }
}