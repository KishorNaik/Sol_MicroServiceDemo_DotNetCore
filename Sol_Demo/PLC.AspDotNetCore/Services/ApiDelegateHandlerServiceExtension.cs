using Microsoft.Extensions.DependencyInjection;
using PLC.AspDotNetCore.ApiHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.AspDotNetCore.Services
{
    public static class ApiDelegateHandlerServiceExtension
    {
        public static void AddApiDelegateHandler(this IServiceCollection services)
        {
            services.AddScoped<ApiDelegateHandler>();
        }
    }
}
