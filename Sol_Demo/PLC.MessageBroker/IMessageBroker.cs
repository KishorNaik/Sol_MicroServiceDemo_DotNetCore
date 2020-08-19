using System;
using System.Threading.Tasks;

namespace PLC.MessageBroker
{
    public interface IMessageBroker
    {
        Task<String> PublishMessageAsync<TMessage>(TMessage message);
    }
}