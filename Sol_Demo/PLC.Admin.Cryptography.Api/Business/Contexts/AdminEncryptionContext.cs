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
    public sealed class AdminEncryptionContext : IAdminEncryptionContext
    {
        #region Declaration

        private readonly IDataProtector dataProtector = null;

        #endregion Declaration

        #region Constructor

        public AdminEncryptionContext(IDataProtectionProvider provider)
        {
            dataProtector = provider.CreateProtector(AppResource.CryptoSecretKey);
        }

        #endregion Constructor

        #region Private Method

        private Task AdminInfoEncrypteAsync(AdminModel adminModel)
        {
            try
            {
                return Task.Run(() =>
                {
                    if (adminModel != null)
                    {
                        adminModel.AdminIdentity = (!string.IsNullOrEmpty(adminModel?.AdminIdentity)) ? dataProtector.Protect(adminModel.AdminIdentity) : null;
                        adminModel.FirstName = (!string.IsNullOrEmpty(adminModel?.FirstName)) ? dataProtector.Protect(adminModel.FirstName) : null;
                        adminModel.LastName = (!string.IsNullOrEmpty(adminModel?.LastName)) ? dataProtector.Protect(adminModel.LastName) : null;
                        adminModel.EmailId = (!string.IsNullOrEmpty(adminModel?.EmailId)) ? dataProtector.Protect(adminModel.EmailId) : null;
                        adminModel.Role = (!string.IsNullOrEmpty(adminModel?.Role)) ? dataProtector.Protect(adminModel.Role) : null;
                    }
                });
            }
            catch
            {
                throw;
            }
        }

        private Task AdminLoginEncrypteAsync(AdminModel adminModel)
        {
            try
            {
                return Task.Run(() =>
                {
                    if (adminModel?.AdminLogin != null)
                    {
                        adminModel.AdminLogin.UserName = (!string.IsNullOrEmpty(adminModel?.AdminLogin?.UserName)) ? dataProtector.Protect(adminModel?.AdminLogin?.UserName) : null;
                        adminModel.AdminLogin.Password = (!string.IsNullOrEmpty(adminModel?.AdminLogin?.Password)) ? dataProtector.Protect(adminModel?.AdminLogin?.Password) : null;
                        adminModel.AdminLogin.Salt = (!string.IsNullOrEmpty(adminModel?.AdminLogin?.Salt)) ? dataProtector.Protect(adminModel?.AdminLogin?.Salt) : null;
                        adminModel.AdminLogin.Hash = (!string.IsNullOrEmpty(adminModel?.AdminLogin?.Salt)) ? dataProtector.Protect(adminModel?.AdminLogin?.Hash) : null;
                    }
                });
            }
            catch
            {
                throw;
            }
        }

        #endregion Private Method

        async Task<AdminModel> IAdminEncryptionContext.AdminEncryptionAsync(AdminModel adminModel)
        {
            try
            {
                await Task.WhenAll(this.AdminInfoEncrypteAsync(adminModel), this.AdminLoginEncrypteAsync(adminModel));

                return adminModel;
            }
            catch
            {
                throw;
            }
        }
    }
}