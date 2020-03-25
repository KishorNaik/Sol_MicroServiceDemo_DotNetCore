using PLC.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Users.Api.Cores.Context
{
    public interface IUsersDecryptionContext
    {
        Task<UserModel> UserDecryptAsync(UserModel userModel);
    }
}
