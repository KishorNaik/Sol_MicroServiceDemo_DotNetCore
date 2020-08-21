using Newtonsoft.Json;
using PLC.Admin.Api.Cores.Business.Events;
using PLC.Admin.Api.Models;
using PLC.Admin.Cryptography.MessageBroker.Cores;
using PLC.EventHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Business.Events
{
    public sealed class AdminEncrypteEventHandler : IAdminEncrypteEventHandler
    {
        private readonly IAdminEncrypteMessageBroker adminEncrypteMessageBroker = null;

        public AdminEncrypteEventHandler(IAdminEncrypteMessageBroker adminEncrypteMessageBroker)
        {
            this.adminEncrypteMessageBroker = adminEncrypteMessageBroker;
        }

        async Task<AdminModel> IEventHandlers<AdminModel, AdminModel>.EventHandleAsync(AdminModel model)
        {
            try
            {
                var taskEncrypte = adminEncrypteMessageBroker?.PublishMessageAsync<AdminModel>(model);

                return JsonConvert.DeserializeObject<AdminModel>(await taskEncrypte);
            }
            catch
            {
                throw;
            }
        }
    }
}