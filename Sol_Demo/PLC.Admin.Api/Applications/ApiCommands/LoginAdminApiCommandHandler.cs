using Microsoft.AspNetCore.Mvc;
using PLC.Admin.Api.Controllers;
using PLC.Admin.Api.Cores.Applications.ApiCommands;
using PLC.Admin.Api.Cores.Business.Query;
using PLC.Admin.Api.Models;
using PLC.ApiCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Applications.ApiCommands
{
    public sealed class LoginAdminApiCommandHandler : ILoginAdminApiCommandHandler
    {
        private readonly ILoginAdminQueryHandler loginAdminQueryHandler = null;

        public LoginAdminApiCommandHandler(ILoginAdminQueryHandler loginAdminQueryHandler)
        {
            this.loginAdminQueryHandler = loginAdminQueryHandler;
        }

        async Task<IActionResult> IApiCommand<AdminController, AdminModel>.ExecuteAsync(AdminController controller, AdminModel model)
        {
            try
            {
                var taskData = loginAdminQueryHandler?.LoginAsync(model);
                return controller?.Ok((Object)await taskData);
            }
            catch
            {
                throw;
            }
        }
    }
}