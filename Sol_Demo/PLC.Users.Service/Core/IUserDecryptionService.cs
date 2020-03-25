using PLC.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Users.Service.Core
{
    public interface IUserDecryptionService
    {
        Task<UserModel> UserDecrypteEndPointAsync(UserModel userModel);
    }
}
