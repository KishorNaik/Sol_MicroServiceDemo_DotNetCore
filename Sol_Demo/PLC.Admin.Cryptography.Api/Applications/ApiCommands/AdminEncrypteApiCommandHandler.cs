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
    public sealed class AdminEncrypteApiCommandHandler : IAdminEncryptionApiCommandHandler
    {
        #region Declaration

        private readonly IAdminEncryptionContext adminEncryptionContext = null;

        #endregion Declaration

        #region Constructor

        public AdminEncrypteApiCommandHandler(IAdminEncryptionContext adminEncryptionContext)
        {
            this.adminEncryptionContext = adminEncryptionContext;
        }

        #endregion Constructor

        async Task<IActionResult> IApiCommand<AdminCryptoController, AdminModel>.ExecuteAsync(AdminCryptoController controller, AdminModel model)

        {
            try
            {
                if (model == null) return controller?.BadRequest(new ErrorModel() { StatusCode = 400, Message = "Admin Model should not not be empty" });
                else
                {
                    var data = this.adminEncryptionContext.AdminEncryptionAsync(model).ConfigureAwait(false);

                    return controller?.Ok(await data);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}