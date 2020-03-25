using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                if (userModel == null) return base.BadRequest(new MessageModel() { Message = "User Model Should not Empty" });
                else
                {
                    var response =
                           await
                                addUserContext
                                ?.AddUserAsync(userModel);
                   
                    return base.Ok((Object)response);
                }
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