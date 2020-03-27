using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PLC.Mail.Api.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        [HttpGet("mailsend")]
        public IActionResult MailSend()
        {
            return base.Ok((Object)"Mail Send Demo");
        }
    }
}