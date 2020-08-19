using Microsoft.Extensions.DependencyInjection;
using PLC.Admin.Cryptography.Api.Business.Contexts;
using PLC.Admin.Cryptography.Api.Cores.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Configurations.Services
{
    public static class ContextServiceExtension
    {
        public static void AddContexts(this IServiceCollection services)
        {
            services.AddTransient<IAdminEncryptionContext, AdminEncryptionContext>();
            services.AddTransient<IAdminDecryptionContext, AdminDecryptionContext>();
            services.AddTransient<IAdminEncryptionListContext, AdminEncryptionListContext>();
        }
    }
}