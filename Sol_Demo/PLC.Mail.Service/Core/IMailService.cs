using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Mail.Service.Core
{
    public interface IMailService
    {
        Task<String> SendMailEndPoint();
    }
}
