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
    public sealed class AdminEncrypteListEventHandler : IAdminEncrypteListEventHandler
    {
        private readonly IAdminEncrypteListMessageBroker adminEncrypteListMessageBroker = null;

        public AdminEncrypteListEventHandler(IAdminEncrypteListMessageBroker adminEncrypteListMessageBroker)
        {
            this.adminEncrypteListMessageBroker = adminEncrypteListMessageBroker;
        }

        async Task<List<AdminModel>> IEventHandlers<List<AdminModel>, List<AdminModel>>.EventHandleAsync(List<AdminModel> model)
        {
            try
            {
                var taskEncrypteList = adminEncrypteListMessageBroker?.PublishMessageAsync<List<AdminModel>>(model);

                return JsonConvert.DeserializeObject<List<AdminModel>>((await taskEncrypteList));
            }
            catch
            {
                throw;
            }
        }
    }
}