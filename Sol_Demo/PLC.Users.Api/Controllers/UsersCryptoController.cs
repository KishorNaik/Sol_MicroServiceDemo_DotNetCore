using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                if (userModel == null) return base.BadRequest(new MessageModel() { Message = "User Model Should not be empty" });
                else
                {
                    var response =
                            await
                               usersEncryptionContext
                               ?.UsersEncryptAsync(userModel);

                    return base.Ok((Object)response);
                }
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
                if (userModel == null) return base.BadRequest(new MessageModel() { Message = "User Model Should not be empty" });
                else
                {
                    var response =
                            await
                               usersDecryptionContext
                               ?.UserDecryptAsync(userModel);

                    return base.Ok((Object)response);
                }
            }
            catch
            {
                throw;
            }
        }


        #endregion 
    }
}