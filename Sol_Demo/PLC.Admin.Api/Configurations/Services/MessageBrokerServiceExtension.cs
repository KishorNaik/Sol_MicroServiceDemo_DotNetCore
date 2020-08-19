using Microsoft.Extensions.DependencyInjection;
using Pathoschild.Http.Client;
using PLC.Admin.Cryptography.MessageBroker.Cores;
using PLC.Admin.Cryptography.MessageBroker.Publish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Configurations.Services
{
    public static class MessageBrokerServiceExtension
    {
        public static void AddMessageBrokerConfig(this IServiceCollection services, bool? isProduction)
        {
            string baseUrl = (isProduction == true) ? "http://localhost:5642/api/admincrypto" : "http://localhost:5642/api/admincrypto";

            services.AddTransient<IAdminDecrypteMessageBroker, AdminDecrypteMessageBroker>((config) =>
            {
                return new AdminDecrypteMessageBroker(new FluentClient(baseUrl));
            });

            services.AddTransient<IAdminEncrypteMessageBroker, AdminEncrypteMessageBroker>((config) =>
            {
                return new AdminEncrypteMessageBroker(new FluentClient(baseUrl));
            });

            services.AddTransient<IAdminEncrypteListMessageBroker, AdminEncrypteListMessageBroker>((config) =>
            {
                return new AdminEncrypteListMessageBroker(new FluentClient(baseUrl));
            });
        }
    }
}