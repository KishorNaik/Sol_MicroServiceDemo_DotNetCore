using PLC.Models.Users;
using PLC.Users.Api.Cores.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Users.Api.Cores.Context
{
    public interface ILoginCredentailsValidateContext
    {
        Task<dynamic> LoginValidateAsync(UserModel userModel);
    }
}
