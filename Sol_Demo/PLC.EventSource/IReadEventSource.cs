using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.EventSource
{
    public interface IReadEventSource<TEventModel> where TEventModel : class
    {
        Task<TEventModel> ReadAsync(string transactionId);
    }
}