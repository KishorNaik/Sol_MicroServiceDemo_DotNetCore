using Pathoschild.Http.Client;
using PLC.Mail.Service.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Mail.Service.Service
{
    public class MailService : IMailService
    {
        #region Declaration
        private readonly IClient client = null;
        #endregion

        #region Constructor
        public MailService(Func<String,IClient> funcClient)
        {
            client = funcClient("MailApi");
        }
        #endregion 

        #region Public Method
        async Task<string> IMailService.SendMailEndPoint()
        {
            try
            {
                var responseData =
                        await
                            client
                            ?.GetAsync(MailApiResource.SendMailEndPoint)
                            ?.As<String>();

                return responseData;
            }
            catch
            {
                throw;
            }
        }

        #endregion 
    }
}
