using Pathoschild.Http.Client;
using PLC.AppSetting;
using PLC.Models.Users;
using PLC.Users.Service.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Users.Service.Service
{
    public sealed class UserEncryptionService : IUserEncryptionService
    {

        #region Declaration
        private readonly IClient client = null;
        #endregion

        #region Constructor
        public UserEncryptionService()
        {
            string baseUrl =
                    (Convert.ToBoolean(AppResource.IsProduction) == true)
                    ? UsersApiResource.UserCryptoBaseUrlLive
                    : UsersApiResource.UserCryptoBaseUrlUat;

            client = new FluentClient(baseUrl);
        }
        #endregion

        #region Public Method
        Task<UserModel> IUserEncryptionService.UserEncrypteEndPointAsync(UserModel userModel)
        {
            try
            {
                var data =
                      client
                      ?.PostAsync<UserModel>(UsersApiResource.UserEncrypteEndPoint, userModel)
                      ?.WithHeader("InternalApiKey",AppResource.InternalApiKey)
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
