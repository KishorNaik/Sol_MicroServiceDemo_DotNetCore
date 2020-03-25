﻿using Pathoschild.Http.Client;
using PLC.AppSetting;
using PLC.Models.Users;
using PLC.Users.Service.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Users.Service.Service
{
    public sealed class UserDecryptionService : IUserDecryptionService
    {

        #region Declaration
        private readonly IClient client = null;
        #endregion

        #region Constructor
        public UserDecryptionService()
        {
            string baseUrl =
                    (Convert.ToBoolean(AppResource.IsProduction) == true)
                    ? UsersApiResource.UserCryptoBaseUrlLive
                    : UsersApiResource.UserCryptoBaseUrlUat;

            client = new FluentClient(baseUrl);
        }
        #endregion

        #region Public Method
        
  

        async Task<UserModel> IUserDecryptionService.UserDecrypteEndPointAsync(UserModel userModel)
        {
            try
            {
                var data =
                      await
                      client
                      ?.PostAsync<UserModel>(UsersApiResource.UserEncrypteEndPoint, userModel)
                      ?.WithHeader("InternalApiKey", AppResource.InternalApiKey)
                      ?.As<UserModel>();

                return data;
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }
        }

        #endregion 
    }
}
