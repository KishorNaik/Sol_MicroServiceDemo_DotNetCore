using Pathoschild.Http.Client;
using PLC.Admin.Cryptography.MessageBroker.Cores;
using PLC.AppSetting;
using PLC.MessageBroker;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.MessageBroker.Publish
{
    public sealed class AdminEncrypteListMessageBroker : IAdminEncrypteListMessageBroker
    {
        private readonly IClient client = null;

        public AdminEncrypteListMessageBroker(IClient client)
        {
            this.client = client;
        }

        Task<string> IMessageBroker.PublishMessageAsync<TMessage>(TMessage message)
        {
            try
            {
                return
                      client
                      ?.PostAsync<TMessage>("adminlistencrypte", message)
                      ?.WithHeader("InternalApiKey", AppResource.InternalApiKey)
                      ?.AsString();
            }
            catch
            {
                throw;
            }
        }
    }
}