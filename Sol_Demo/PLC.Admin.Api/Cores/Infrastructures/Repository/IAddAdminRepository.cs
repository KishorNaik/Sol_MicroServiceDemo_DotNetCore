using PLC.Admin.Api.Models;
using PLC.EventStore.Core.Events;
using PLC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Cores.Infrastructures.Repository
{
    public interface IAddAdminRepository : IAddRepository<AdminModel, bool>, IEventStoreHandler<AdminModel>
    {
    }
}