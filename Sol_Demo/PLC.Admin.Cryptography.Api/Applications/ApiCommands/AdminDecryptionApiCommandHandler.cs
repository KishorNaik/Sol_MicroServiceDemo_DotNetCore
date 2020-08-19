using Microsoft.AspNetCore.Mvc;
using PLC.Admin.Cryptography.Api.Controllers;
using PLC.Admin.Cryptography.Api.Cores.Commands;
using PLC.Admin.Cryptography.Api.Cores.Contexts;
using PLC.Admin.Cryptography.Api.Models;
using PLC.ApiCommand;
using PLC.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Applications.ApiCommands
{
    public sealed class AdminDecryptionApiCommandHandler : IAdminDecryptionApiCommandHandler
    {
        private readonly IAdminDecryptionContext adminDecryptionContext = null;

        public AdminDecryptionApiCommandHandler(IAdminDecryptionContext adminDecryptionContext)
        {
            this.adminDecryptionContext = adminDecryptionContext;
        }

        async Task<IActionResult> IApiCommand<AdminCryptoController, AdminModel>.ExecuteAsync(AdminCryptoController controller, AdminModel model)
        {
            try
            {
                if (model == null) return controller?.BadRequest(new ErrorModel() { StatusCode = 400, Message = "Admin Model Should not be empty" });
                else
                {
                    var data = await this.adminDecryptionContext?.AdminDecryptionAsync(model);

                    return controller?.Ok(data);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}