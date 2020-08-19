using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLC.Admin.Cryptography.Api.Cores.Commands;
using PLC.Admin.Cryptography.Api.Models;
using PLC.Configuration.Filters;

namespace PLC.Admin.Cryptography.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/admincrypto")]
    [ApiController]
    [InternalApiKeyValidateFilter]
    public class AdminCryptoController : ControllerBase
    {
        [HttpPost("adminencrypte")]
        public Task<IActionResult> AdminEncryptionAsync(
            [FromServices] IAdminEncryptionApiCommandHandler adminEncryptionCommandHandler,
            [FromBody] AdminModel adminModel
            ) => adminEncryptionCommandHandler?.ExecuteAsync(this, adminModel);

        [HttpPost("admindecrypte")]
        public Task<IActionResult> AdminDecrypteAsync(
            [FromServices] IAdminDecryptionApiCommandHandler adminDecryptionCommandHandler,
            [FromBody] AdminModel adminModel
            ) => adminDecryptionCommandHandler?.ExecuteAsync(this, adminModel);

        [HttpPost("adminlistencrypte")]
        public Task<IActionResult> AdminListEncrypteAsync(
            [FromServices] IAdminEncryptionListApiCommandHandler adminEncryptionListApiCommandHandler,
            [FromBody] List<AdminModel> adminModels
            ) => adminEncryptionListApiCommandHandler?.ExecuteAsync(this, adminModels);
    }
}