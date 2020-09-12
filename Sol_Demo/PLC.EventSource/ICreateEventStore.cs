using System;
using System.Threading.Tasks;

namespace PLC.EventSource
{
    public interface ICreateEventStore<TEventModel> where TEventModel : class
    {
        Task SaveAsync(TEventModel eventModel);
    }
}