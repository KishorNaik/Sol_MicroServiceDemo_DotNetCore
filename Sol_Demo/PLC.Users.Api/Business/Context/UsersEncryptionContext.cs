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
    public class UsersEncryptionContext : IUsersEncryptionContext
    {

        #region Declaration
        private readonly IDataProtector dataProtector = null;
        #endregion

        #region Constructor
        public UsersEncryptionContext(IDataProtectionProvider provider)
        {
            this.dataProtector = provider.CreateProtector(AppResource.CryptoSecretKey);
        }
        #endregion

        #region Private Method
        private Task<UserModel> UserModelEncrypteAsync(UserModel userModel)
        {
            try
            {
                userModel.UserIdentity =(!string.IsNullOrEmpty(userModel?.UserIdentity)) ? dataProtector?.Protect(userModel?.UserIdentity) : null;
                userModel.FirstName =(!string.IsNullOrEmpty(userModel?.FirstName)) ? dataProtector?.Protect(userModel?.FirstName) : null;
                userModel.LastName = (!string.IsNullOrEmpty(userModel?.LastName)) ? dataProtector?.Protect(userModel?.LastName) : null;

                return Task.FromResult<UserModel>(userModel);
            }
            catch
            {
                throw;
            }
        }

        private Task<UserModel> UserCommunicationModelEncrypteAsync(UserModel userModel)
        {
            try
            {
                if (userModel?.UserCommunication != null)
                {
                    userModel.UserCommunication.MobileNo = (!string.IsNullOrEmpty(userModel?.UserCommunication?.MobileNo)) ? dataProtector.Protect(userModel?.UserCommunication?.MobileNo) : null;
                    userModel.UserCommunication.EmailId =(!string.IsNullOrEmpty(userModel?.UserCommunication?.EmailId)) ? dataProtector.Protect(userModel?.UserCommunication?.EmailId) : null;
                }

                return Task.FromResult<UserModel>(userModel);
            }
            catch
            {
                throw;
            }
        }

        private Task<UserModel> UserLoginModelEncrypteAsync(UserModel userModel)
        {
            try
            {
                if (userModel?.UserLogin != null)
                {
                    userModel.UserLogin.UserName = (!string.IsNullOrEmpty(userModel?.UserLogin?.UserName))  ? dataProtector.Protect(userModel?.UserLogin?.UserName) : null;
                    userModel.UserLogin.Password =(!string.IsNullOrEmpty(userModel?.UserLogin?.Password)) ? dataProtector.Protect(userModel?.UserLogin?.Password) : null;
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
        async Task<UserModel> IUsersEncryptionContext.UsersEncryptAsync(UserModel userModel)
        {
            try
            {
                await this.UserModelEncrypteAsync(userModel);

                await this.UserCommunicationModelEncrypteAsync(userModel);

                await this.UserLoginModelEncrypteAsync(userModel);

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
