using Microsoft.AspNetCore.DataProtection;
using PLC.Admin.Cryptography.Api.Cores.Contexts;
using PLC.Admin.Cryptography.Api.Models;
using PLC.AppSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.Api.Business.Contexts
{
    public sealed class AdminEncryptionListContext : IAdminEncryptionListContext
    {
        private readonly IAdminEncryptionContext adminEncryptionContext = null;

        public AdminEncryptionListContext(IAdminEncryptionContext adminEncryptionContext)
        {
            this.adminEncryptionContext = adminEncryptionContext;
        }

        async Task<List<AdminModel>> IAdminEncryptionListContext.AdminEncryptionListAsync(List<AdminModel> adminModels)
        {
            try
            {
                List<Task<AdminModel>> listTask = new List<Task<AdminModel>>();

                foreach (var admin in adminModels)
                {
                    listTask.Add(this.adminEncryptionContext?.AdminEncryptionAsync(admin));
                }

                var adminListData = (await Task.WhenAll(listTask)).ToList();
                return adminListData;
            }
            catch
            {
                throw;
            }
        }
    }
}