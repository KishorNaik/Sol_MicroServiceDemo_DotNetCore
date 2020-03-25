using PLC.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Users.Api.Cores.Context
{
    public interface IUsersEncryptionContext 
    {
        Task<UserModel> UsersEncryptAsync(UserModel userModel);
    }
}
