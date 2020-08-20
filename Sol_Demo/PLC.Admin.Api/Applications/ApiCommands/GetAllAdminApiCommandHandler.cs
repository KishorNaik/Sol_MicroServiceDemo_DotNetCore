using Microsoft.AspNetCore.Mvc;
using PLC.Admin.Api.Controllers;
using PLC.Admin.Api.Cores.Applications.ApiCommands;
using PLC.Admin.Api.Cores.Business.Query;
using PLC.ApiCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Applications.ApiCommands
{
    public sealed class GetAllAdminApiCommandHandler : IGetAllAdminApiCommandHandler
    {
        private readonly IGetAllAdminQueryHandler getAllAdminQuery = null;

        public GetAllAdminApiCommandHandler(IGetAllAdminQueryHandler getAllAdminQuery)
        {
            this.getAllAdminQuery = getAllAdminQuery;
        }

        async Task<IActionResult> IApiCommand<AdminController>.ExecuteAsync(AdminController controller)
        {
            try
            {
                var taskAdminData = this.getAllAdminQuery?.HandleAsync();
                return controller?.Ok(await taskAdminData);
            }
            catch
            {
                throw;
            }
        }
    }
}