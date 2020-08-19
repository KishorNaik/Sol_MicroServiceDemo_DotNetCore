using PLC.Admin.Cryptography.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Cores.Contexts
{
    public interface IAdminDecryptionContext
    {
        Task<AdminModel> AdminDecryptionAsync(AdminModel adminModel);
    }
}