using Microsoft.Extensions.DependencyInjection;
using PLC.EventStore.Core.Repository;
using PLC.EventStore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.EventStore.Configurations
{
    public static class EventStoreServiceExtension
    {
        public static void AddEventStore(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();
        }
    }
}