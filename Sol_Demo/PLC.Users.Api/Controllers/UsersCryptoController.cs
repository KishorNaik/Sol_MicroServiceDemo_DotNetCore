using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLC.AspDotNetCore.ApiHandler;
using PLC.AspDotNetCore.Filters;
using PLC.Models.Cores;
using PLC.Models.Users;
using PLC.Users.Api.Cores.Context;

namespace PLC.Users.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/userscrypto")]
    [ApiController]
    [InternalApiKeyValidateFilter]
    public class UsersCryptoController : ControllerBase
    {
        #region Declaration
        private readonly ApiDelegateHandler apiDelegateHandler = null;
        #endregion

        #region Constructor
        public UsersCryptoController(ApiDelegateHandler apiDelegateHandler)
        {
            this.apiDelegateHandler = apiDelegateHandler;
        }
        #endregion 

        #region Public EndPint

        [HttpPost("userencrypte")]
        [AllowAnonymous]
        public async Task<IActionResult> UserEncryptionAsync(
            [FromBody] UserModel userModel,
            [FromServices] IUsersEncryptionContext usersEncryptionContext
            )
        {
            try
            {
                return
                    await
                        apiDelegateHandler
                        .HandlerAsync<UserModel>(
                            userModel,
                            async ()=> await usersEncryptionContext.UsersEncryptAsync(userModel),
                            (leResponse) => base.Ok((Object)leResponse),
                            "User Model should not be empty"
                            
                            );
            }
            catch
            {
                throw;
            }
        }


        [HttpPost("userdecrypte")]
        [AllowAnonymous]
        public async Task<IActionResult> UserDedryptionAsync(
            [FromBody] UserModel userModel,
            [FromServices] IUsersDecryptionContext usersDecryptionContext
            )
        {
            try
            {
                return
                     await
                        apiDelegateHandler
                        .HandlerAsync<UserModel>(
                            userModel,
                            async () => await usersDecryptionContext.UserDecryptAsync(userModel),
                            (leResponse) => base.Ok((Object)leResponse),
                            "User Model should not be empty"
                            );
            }
            catch
            {
                throw;
            }
        }


        #endregion 
    }
}