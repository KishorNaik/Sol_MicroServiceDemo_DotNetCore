using PLC.EventSource;
using PLC.EventStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.EventStore.Core.Repository
{
    public interface IEventStoreRepository : ICreateEventStore<EventModel>, IReadEventSource<EventModel>
    {
    }
}