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
    public sealed class AdminDecryptionContext : IAdminDecryptionContext
    {
        private readonly IDataProtector dataProtector = null;

        public AdminDecryptionContext(IDataProtectionProvider provider)
        {
            dataProtector = provider.CreateProtector(AppResource.CryptoSecretKey);
        }

        private Task AdminInfoDecrypteAsync(AdminModel adminModel)
        {
            try
            {
                return Task.Run(() =>
                {
                    if (adminModel != null)
                    {
                        adminModel.AdminIdentity = (!string.IsNullOrEmpty(adminModel?.AdminIdentity)) ? dataProtector.Unprotect(adminModel.AdminIdentity) : null;
                        adminModel.FirstName = (!string.IsNullOrEmpty(adminModel?.FirstName)) ? dataProtector.Unprotect(adminModel.FirstName) : null;
                        adminModel.LastName = (!string.IsNullOrEmpty(adminModel?.LastName)) ? dataProtector.Unprotect(adminModel.LastName) : null;
                        adminModel.EmailId = (!string.IsNullOrEmpty(adminModel?.EmailId)) ? dataProtector.Unprotect(adminModel.EmailId) : null;
                        adminModel.Role = (!string.IsNullOrEmpty(adminModel?.Role)) ? dataProtector.Unprotect(adminModel.Role) : null;
                    }
                });
            }
            catch
            {
                throw;
            }
        }

        private Task AdminLoginDecrypteAsync(AdminModel adminModel)
        {
            try
            {
                return Task.Run(() =>
                {
                    if (adminModel?.AdminLogin != null)
                    {
                        adminModel.AdminLogin.UserName = (!string.IsNullOrEmpty(adminModel?.AdminLogin?.UserName)) ? dataProtector.Unprotect(adminModel?.AdminLogin?.UserName) : null;
                        adminModel.AdminLogin.Password = (!string.IsNullOrEmpty(adminModel?.AdminLogin?.Password)) ? dataProtector.Unprotect(adminModel?.AdminLogin?.Password) : null;
                        adminModel.AdminLogin.Salt = (!string.IsNullOrEmpty(adminModel?.AdminLogin?.Salt)) ? dataProtector.Unprotect(adminModel?.AdminLogin?.Salt) : null;
                        adminModel.AdminLogin.Hash = (!string.IsNullOrEmpty(adminModel?.AdminLogin?.Salt)) ? dataProtector.Unprotect(adminModel?.AdminLogin?.Hash) : null;
                    }
                });
            }
            catch
            {
                throw;
            }
        }

        async Task<AdminModel> IAdminDecryptionContext.AdminDecryptionAsync(AdminModel adminModel)
        {
            try
            {
                await Task.WhenAll(this.AdminInfoDecrypteAsync(adminModel), this.AdminLoginDecrypteAsync(adminModel));
                return adminModel;
            }
            catch
            {
                throw;
            }
        }
    }
}