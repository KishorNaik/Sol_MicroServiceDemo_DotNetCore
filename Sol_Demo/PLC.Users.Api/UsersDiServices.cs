using Microsoft.Extensions.DependencyInjection;
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

            
        }
    }
}
