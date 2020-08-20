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
    public class AdminDecrypteMessageBroker : IAdminDecrypteMessageBroker
    {
        private readonly IClient client = null;

        public AdminDecrypteMessageBroker(IClient client)
        {
            this.client = client;
        }

        Task<String> IMessageBroker.PublishMessageAsync<TMessage>(TMessage message)
        {
            try
            {
                return
                       client
                       ?.PostAsync<TMessage>("admindecrypte", message)
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