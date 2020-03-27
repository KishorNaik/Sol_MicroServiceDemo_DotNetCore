using Microsoft.Extensions.DependencyInjection;
using Pathoschild.Http.Client;
using PLC.AppSetting;
using PLC.Mail.Service;
using PLC.Mail.Service.Core;
using PLC.Mail.Service.Service;
using PLC.Users.Api.Business.Context;
using PLC.Users.Api.Cores.Context;
using PLC.Users.Api.Cores.Repository;
using PLC.Users.Api.Infrastructures.Repository;
using PLC.Users.Service.Core;
using PLC.Users.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Users.Api
{
    public static class UsersDiServices
    {
        public static void AddUsersDi(this IServiceCollection services)
        {
            services.AddTransient<IUsersEncryptionContext, UsersEncryptionContext>();
            services.AddTransient<IUsersDecryptionContext, UsersDecryptionContext>();

            services.AddTransient<IAddUserContext, AddUsersContext>();
            services.AddTransient<IAddUsersRepository, AddUsersRepository>();

            services.AddTransient<ILoginCredentailsValidateRepository, LoginCredentailsValidateRepository>();

            // Api Service Call
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<Func<String, IClient>>(
                (
                    leServiceProvider=>
                        leKey =>
                        {
                            FluentClient fluentClient = null;
                            String baseUrl = null;

                            if (leKey == "MailApi")
                            {
                                baseUrl =
                                    (Convert.ToBoolean(AppResource.IsProduction) == true)
                                        ? MailApiResource.MailBaseUrlLive
                                        : MailApiResource.MailBaseUrlUat;

                                fluentClient = new FluentClient(baseUrl);
                            }

                            return fluentClient;
                        }

                       
                ));
        }
    }
}
