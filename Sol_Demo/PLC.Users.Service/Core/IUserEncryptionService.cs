using PLC.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Users.Service.Core
{
    public interface IUserEncryptionService
    {
        Task<UserModel> UserEncrypteEndPointAsync(UserModel userModel);
    }
}
