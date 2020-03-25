using PLC.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Users.Api.Cores.Repository
{
    public interface IAddUsersRepository
    {
        Task<dynamic> AddUserAsync(UserModel userModel);
    }
}
