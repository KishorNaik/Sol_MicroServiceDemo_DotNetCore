using PLC.Admin.Cryptography.Api.Controllers;
using PLC.Admin.Cryptography.Api.Models;
using PLC.ApiCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Cores.Commands
{
    public interface IAdminEncryptionListApiCommandHandler : IApiCommand<AdminCryptoController, List<AdminModel>>
    {
    }
}