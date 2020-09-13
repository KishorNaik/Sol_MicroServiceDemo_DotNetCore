using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.EventStore.Core.Events
{
    public interface IEventStoreHandler<TNewData> where TNewData : class
    {
        event EventHandler<TNewData> EventStoreHandler;
    }

    public interface IEventStoreHandler<TOldData, TNewData> where TOldData : class where TNewData : class
    {
        event Action<Object, TOldData, TNewData> EventStoreHandler;
    }
}