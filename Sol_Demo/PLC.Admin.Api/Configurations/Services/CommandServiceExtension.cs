using Microsoft.Extensions.DependencyInjection;
using PLC.Admin.Api.Business.Commands;
using PLC.Admin.Api.Cores.Business.Commands;
using PLC.Admin.Cryptography.MessageBroker.Cores;
using PLC.Admin.Cryptography.MessageBroker.Publish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Configurations.Services
{
    public static class CommandServiceExtension
    {
        public static void AddCommandConfig(this IServiceCollection services)
        {
            services.AddTransient<IAddAdminCommandHandler, AddAdminCommandHandler>();
        }
    }
}