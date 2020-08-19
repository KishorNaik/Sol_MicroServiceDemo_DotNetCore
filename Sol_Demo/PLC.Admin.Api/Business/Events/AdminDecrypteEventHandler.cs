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
    public sealed class AdminDecrypteEventHandler : IAdminDecrypteEventHandler
    {
        private readonly IAdminDecrypteMessageBroker adminDecrypteMessageBroker = null;

        public AdminDecrypteEventHandler(IAdminDecrypteMessageBroker adminDecrypteMessageBroker)
        {
            this.adminDecrypteMessageBroker = adminDecrypteMessageBroker;
        }

        async Task<AdminModel> IEventHandlers<AdminModel, AdminModel>.EventHandleAsync(AdminModel model)
        {
            try
            {
                var taskDecrypte = adminDecrypteMessageBroker?.PublishMessageAsync<AdminModel>(model);

                return JsonConvert.DeserializeObject<AdminModel>((await taskDecrypte));
            }
            catch
            {
                throw;
            }
        }
    }
}