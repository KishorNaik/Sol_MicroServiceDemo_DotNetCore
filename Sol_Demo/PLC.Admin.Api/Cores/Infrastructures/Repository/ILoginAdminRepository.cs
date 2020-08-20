﻿using PLC.Admin.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Cores.Infrastructures.Repository
{
    public interface ILoginAdminRepository
    {
        Task<AdminModel> LoginAsync(AdminModel adminModel);
    }
}