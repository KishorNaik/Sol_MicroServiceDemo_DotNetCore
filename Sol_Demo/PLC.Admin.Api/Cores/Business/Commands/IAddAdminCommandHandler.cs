using PLC.Admin.Api.Models;
using PLC.CQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Cores.Business.Commands
{
    public interface IAddAdminCommandHandler : ICommandHandler<AdminModel, bool>
    {
    }
}