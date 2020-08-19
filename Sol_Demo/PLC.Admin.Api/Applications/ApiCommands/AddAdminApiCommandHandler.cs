using Microsoft.AspNetCore.Mvc;
using PLC.Admin.Api.Controllers;
using PLC.Admin.Api.Cores.Applications.ApiCommands;
using PLC.Admin.Api.Cores.Business.Commands;
using PLC.Admin.Api.Models;
using PLC.ApiCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Applications.ApiCommands
{
    public sealed class AddAdminApiCommandHandler : IAddAdminApiCommandHandler
    {
        private readonly IAddAdminCommandHandler addAdminCommandHandler = null;

        public AddAdminApiCommandHandler(IAddAdminCommandHandler addAdminCommandHandler)
        {
            this.addAdminCommandHandler = addAdminCommandHandler;
        }

        async Task<IActionResult> IApiCommand<AdminController, AdminModel>.ExecuteAsync(AdminController controller, AdminModel model)
        {
            try
            {
                var taskCommand = addAdminCommandHandler?.HandleAsync(model);
                return controller?.Ok((Object)await taskCommand);
            }
            catch
            {
                throw;
            }
        }
    }
}