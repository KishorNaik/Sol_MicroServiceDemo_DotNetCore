using PLC.Admin.Api.Controllers;
using PLC.Admin.Api.Models;
using PLC.ApiCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Cores.Applications.ApiCommands
{
    public interface ILoginAdminApiCommandHandler : IApiCommand<AdminController, AdminModel>
    {
    }
}