using Microsoft.AspNetCore.Mvc;
using PLC.Admin.Cryptography.Api.Controllers;
using PLC.Admin.Cryptography.Api.Cores.Commands;
using PLC.Admin.Cryptography.Api.Cores.Contexts;
using PLC.Admin.Cryptography.Api.Models;
using PLC.ApiCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Applications.ApiCommands
{
    public sealed class AdminEncryptionListApiCommandHandler : IAdminEncryptionListApiCommandHandler
    {
        private readonly IAdminEncryptionListContext adminEncryptionListContext = null;

        public AdminEncryptionListApiCommandHandler(IAdminEncryptionListContext adminEncryptionListContext)
        {
            this.adminEncryptionListContext = adminEncryptionListContext;
        }

        async Task<IActionResult> IApiCommand<AdminCryptoController, List<AdminModel>>.ExecuteAsync(AdminCryptoController controller, List<AdminModel> adminModels)
        {
            try
            {
                var taskEncryptionList = this.adminEncryptionListContext?.AdminEncryptionListAsync(adminModels);
                return controller?.Ok(await taskEncryptionList);
            }
            catch
            {
                throw;
            }
        }
    }
}