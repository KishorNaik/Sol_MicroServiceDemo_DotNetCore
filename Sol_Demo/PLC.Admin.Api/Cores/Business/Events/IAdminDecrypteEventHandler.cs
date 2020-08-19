using PLC.Admin.Api.Models;
using PLC.EventHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Cores.Business.Events
{
    public interface IAdminDecrypteEventHandler : IEventHandlers<AdminModel, AdminModel>
    {
    }
}