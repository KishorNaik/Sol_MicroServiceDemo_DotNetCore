using Microsoft.Extensions.DependencyInjection;
using PLC.Admin.Cryptography.Api.Applications.ApiCommands;
using PLC.Admin.Cryptography.Api.Cores.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Configurations.Services
{
    public static class CommandHandlerServiceExtension
    {
        public static void AddCommandHandler(this IServiceCollection services)
        {
            services.AddTransient<IAdminEncryptionApiCommandHandler, AdminEncrypteApiCommandHandler>();
            services.AddTransient<IAdminDecryptionApiCommandHandler, AdminDecryptionApiCommandHandler>();
            services.AddTransient<IAdminEncryptionListApiCommandHandler, AdminEncryptionListApiCommandHandler>();
        }
    }
}