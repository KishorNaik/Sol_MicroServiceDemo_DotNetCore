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
        public UserEncryptionService(Func<string,IClient> funcClient)
        {
            //    string baseUrl =
            //            (Convert.ToBoolean(AppResource.IsProduction) == true)
            //            ? UsersApiResource.UserCryptoBaseUrlLive
            //            : UsersApiResource.UserCryptoBaseUrlUat;

            //    client = new FluentClient(baseUrl);

            client = funcClient("UserEncrypteApi");
        }
        #endregion

        #region Public Method
        async Task<UserModel> IUserEncryptionService.UserEncrypteEndPointAsync(UserModel userModel)
        {
            try
            {
                var data =
                       await
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
