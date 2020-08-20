using PLC.Admin.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Cores.Business.Query
{
    public interface ILoginAdminQueryHandler
    {
        Task<dynamic> LoginAsync(AdminModel adminModel);
    }
}