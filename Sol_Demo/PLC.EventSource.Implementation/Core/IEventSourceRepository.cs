using PLC.EventSource.Implementation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.EventSource.Implementation.Core
{
    public interface IEventStoreRepository : ICreateEventStore<EventModel>, IReadEventSource<EventModel>
    {
    }
}