using Microsoft.AspNetCore.DataProtection;
using PLC.AppSetting;
using PLC.Models.Users;
using PLC.Users.Api.Cores.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Users.Api.Business.Context
{
    public sealed class UsersDecryptionContext : IUsersDecryptionContext
    {

        #region Declaration
        private readonly IDataProtector dataProtector = null;
        #endregion

        #region Constructor
        public UsersDecryptionContext(IDataProtectionProvider provider)
        {
            this.dataProtector = provider.CreateProtector(AppResource.CryptoSecretKey);
        }
        #endregion

        #region Private Method
        private Task<UserModel> UserModelDecrypteAsync(UserModel userModel)
        {
            try
            {
                userModel.UserIdentity =(!string.IsNullOrEmpty(userModel?.UserIdentity)) ? dataProtector.Unprotect(userModel?.UserIdentity) : null;
                userModel.FirstName = (!string.IsNullOrEmpty(userModel?.FirstName)) ? dataProtector.Unprotect(userModel?.FirstName) : null;
                userModel.LastName = (!string.IsNullOrEmpty(userModel?.LastName)) ?  dataProtector.Unprotect(userModel?.LastName) : null;

                return Task.FromResult<UserModel>(userModel);
            }
            catch
            {
                throw;
            }
        }

        private Task<UserModel> UserCommunicationModelDecrypteAsync(UserModel userModel)
        {
            try
            {
                if (userModel?.UserCommunication != null)
                {
                    userModel.UserCommunication.MobileNo =(!string.IsNullOrEmpty(userModel?.UserCommunication?.MobileNo)) ? dataProtector.Unprotect(userModel?.UserCommunication?.MobileNo) : null;
                    userModel.UserCommunication.EmailId = (!string.IsNullOrEmpty(userModel?.UserCommunication?.EmailId)) ? dataProtector.Unprotect(userModel?.UserCommunication?.EmailId) :null;
                }

                return Task.FromResult<UserModel>(userModel);
            }
            catch
            {
                throw;
            }
        }

        private Task<UserModel> UserLoginModelDecrypteAsync(UserModel userModel)
        {
            try
            {
                if (userModel?.UserLogin != null)
                {
                    userModel.UserLogin.UserName =(!string.IsNullOrEmpty(userModel?.UserLogin?.UserName)) ? dataProtector.Unprotect(userModel?.UserLogin?.UserName) : null;
                    userModel.UserLogin.Password = (!string.IsNullOrEmpty(userModel?.UserLogin?.Password)) ? dataProtector.Unprotect(userModel?.UserLogin?.Password) : null;
                }

                return Task.FromResult<UserModel>(userModel);
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region Public Method
        async Task<UserModel> IUsersDecryptionContext.UserDecryptAsync(UserModel userModel)
        {
            try
            {
                await this.UserModelDecrypteAsync(userModel);

                await this.UserCommunicationModelDecrypteAsync(userModel);

                await this.UserLoginModelDecrypteAsync(userModel);

                return userModel;
            }
            catch
            {
                throw;
            }
        }

        #endregion 
    }
}
