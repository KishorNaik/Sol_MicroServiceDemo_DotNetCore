using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLC.Admin.Api.Cores.Applications.ApiCommands;
using PLC.Admin.Api.Models;
using PLC.Configuration.Filters;

namespace PLC.Admin.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/admin")]
    [ApiController]
    [InternalApiKeyValidateFilter]
    public class AdminController : ControllerBase
    {
        [HttpPost("add")]
        public Task<IActionResult> AddAsync(
            [FromBody] AdminModel adminModel,
            [FromServices] IAddAdminApiCommandHandler addAdminApiCommandHandler
            ) => addAdminApiCommandHandler?.ExecuteAsync(this, adminModel);

        [HttpPost("getall")]
        public Task<IActionResult> GetAllDataAsync(
            [FromServices] IGetAllAdminApiCommandHandler getAllAdminApiCommandHandler
            ) => getAllAdminApiCommandHandler?.ExecuteAsync(this);

        [HttpGet("test")]
        public IActionResult Test()
        {
            return base.Ok("Test");
        }
    }
}