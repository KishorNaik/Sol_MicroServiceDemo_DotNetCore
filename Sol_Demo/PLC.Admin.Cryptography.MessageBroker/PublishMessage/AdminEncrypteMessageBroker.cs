using Pathoschild.Http.Client;
using PLC.Admin.Cryptography.MessageBroker.Cores;
using PLC.AppSetting;
using PLC.MessageBroker;
using System;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.MessageBroker.Publish
{
    public sealed class AdminEncrypteMessageBroker : IAdminEncrypteMessageBroker
    {
        private readonly IClient client = null;

        public AdminEncrypteMessageBroker(IClient client)
        {
            this.client = client;
        }

        Task<String> IMessageBroker.PublishMessageAsync<TMessage>(TMessage message)
        {
            try
            {
                return
                       client
                       ?.PostAsync<TMessage>("adminencrypte", message)
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