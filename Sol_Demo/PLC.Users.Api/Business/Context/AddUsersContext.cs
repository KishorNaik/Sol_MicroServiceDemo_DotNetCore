﻿using HashPassword;
using PLC.Models.Users;
using PLC.Users.Api.Cores.Context;
using PLC.Users.Api.Cores.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Users.Api.Business.Context
{
    public sealed class AddUsersContext : IAddUserContext
    {
        #region Declaration
        private readonly IAddUsersRepository addUsersRepository = null;
        private readonly IUsersDecryptionContext usersDecryptionContext = null;
        #endregion

        #region Constructor
        public AddUsersContext(IAddUsersRepository addUsersRepository, IUsersDecryptionContext usersDecryptionContext)
        {
            this.addUsersRepository = addUsersRepository;
            this.usersDecryptionContext = usersDecryptionContext;
        }
        #endregion

        #region Private Method
        private async Task PasswordHashAsync(UserModel userModel)
        {
            try
            {
                // Password Hasing
                userModel.UserLogin.Salt = await Salt.CreateAsync(ByteRange.byte256);
                userModel.UserLogin.Hash = await Hash.CreateAsync(userModel.UserLogin.Password, userModel?.UserLogin?.Salt, ByteRange.byte256);
            }
            catch
            {
                throw;
            }

        }
        #endregion 

        #region Public Metod
        async Task<dynamic> IAddUserContext.AddUserAsync(UserModel userModel)
        {
            try
            {
                // Decrypte userModel
                userModel =
                       await
                       this
                       .usersDecryptionContext.UserDecryptAsync(userModel);

                await PasswordHashAsync(userModel);

                // Add User Data
                var response =
                        await
                        addUsersRepository
                        ?.AddUserAsync(userModel);

                return response;
            }
            catch
            {
                throw;
            }
        }

        

        #endregion
    }
}
