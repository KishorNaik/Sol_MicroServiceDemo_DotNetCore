using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DalSoft.Hosting.BackgroundQueue.DependencyInjection;

namespace PLC.AspDotNetCore.Services
{
    public static class BackgroundTaskExtensionService
    {
        public static void AddBackgroundTask(this IServiceCollection services)
        {
            services.AddBackgroundQueue(maxConcurrentCount: 1, millisecondsToWaitBeforePickingUpTask: 1000,
              onException: exception =>
              {

              });
        }
    }
}
