using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLC.AspDotNetCore.ApiHandler;
using PLC.Models.Cores;
using PLC.Models.Users;
using PLC.Users.Api.Cores.Context;

namespace PLC.Users.Api.Controllers
{
    [Produces("application/Json")]
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        #region Declaration
        private readonly ApiDelegateHandler apiDelegateHandler = null;
        #endregion

        #region Constructor
        public UsersController(ApiDelegateHandler apiDelegateHandler)
        {
            this.apiDelegateHandler = apiDelegateHandler;
        }
        #endregion


        #region Public EndPoint
        [AllowAnonymous]
        [HttpPost("adduser")]
        public async Task<IActionResult> AddUsersAsync(
            [FromBody] UserModel userModel,
            [FromServices] IAddUserContext addUserContext
            )
        {
            try
            {
                return 
                    await 
                    apiDelegateHandler
                        .HandlerAsync<UserModel>(
                            userModel,
                            async ()=> await addUserContext?.AddUserAsync(userModel),
                            (leResponse)=> base.Ok((Object)leResponse),
                            "User Model Should not be empty"
                            );
            }
            catch
            {
                // Log
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> UserLoginAsync(
           [FromBody] UserModel userModel,
           [FromServices] ILoginCredentailsValidateContext loginCredentailsValidateContext
           )
        {
            try
            {
                return
                    await
                    apiDelegateHandler
                        .HandlerAsync<UserModel>(
                            userModel,
                            async () => await loginCredentailsValidateContext?.LoginValidateAsync(userModel),
                            (leResponse) => base.Ok((Object)leResponse),
                            "User Model Should not be empty"
                            );
            }
            catch
            {
                // Log
                throw;
            }
        }
        #endregion 

    }
}