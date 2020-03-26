using PLC.Models.Users;
using PLC.Users.Api.Cores.Context;
using PLC.Users.Api.Cores.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Users.Api.Business.Context
{
    public sealed class LoginCredentailsValidateContext : ILoginCredentailsValidateContext
    {
        #region Declaration
        private readonly ILoginCredentailsValidateRepository loginCredentailsValidateRepository;
        private readonly IUsersDecryptionContext usersDecryptionContext = null;
        private readonly IUsersEncryptionContext usersEncryptionContext = null;
        #endregion 

        #region Constructor
        public LoginCredentailsValidateContext(
            ILoginCredentailsValidateRepository loginCredentailsValidateRepository,
            IUsersDecryptionContext usersDecryptionContext,
            IUsersEncryptionContext usersEncryptionContext
            )
        {
            this.loginCredentailsValidateRepository = loginCredentailsValidateRepository;
            this.usersDecryptionContext = usersDecryptionContext;
            this.usersEncryptionContext = usersEncryptionContext;
        }
        #endregion


        #region Public Method
        async Task<dynamic> ILoginCredentailsValidateContext.LoginValidateAsync(UserModel userModel)
        {
            dynamic encrypteUserData = null;
            try
            {
                userModel = await usersDecryptionContext.UserDecryptAsync(userModel);

                var response = await loginCredentailsValidateRepository.LoginValidateAsync(userModel);

                if(response is UserModel)
                {
                    encrypteUserData = await usersEncryptionContext?.UsersEncryptAsync(userModel);
                }
                else
                {
                    encrypteUserData = response;
                }

                return encrypteUserData;
            }
            catch
            {
                throw;
            }
        }
        #endregion 
    }
}
