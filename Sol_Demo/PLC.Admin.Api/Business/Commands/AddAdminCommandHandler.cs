using DalSoft.Hosting.BackgroundQueue;
using HashPassword;
using Newtonsoft.Json;
using PLC.Admin.Api.Cores.Business.Commands;
using PLC.Admin.Api.Cores.Business.Events;
using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.Admin.Api.Models;
using PLC.CQ;
using PLC.EventStore.Core;
using PLC.EventStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Business.Commands
{
    public sealed class AddAdminCommandHandler : IAddAdminCommandHandler
    {
        private readonly IAddAdminRepository addAdminRepository = null;
        private readonly IAdminDecrypteEventHandler adminDecrypteEventHandler = null;

        private readonly BackgroundQueue backgroundQueue = null;
        private readonly IEventStoreRepository eventStoreRepository = null;

        public AddAdminCommandHandler(
            IAddAdminRepository addAdminRepository,
            IAdminDecrypteEventHandler adminDecrypteEventHandler,
            BackgroundQueue backgroundQueue,
            IEventStoreRepository eventStoreRepository
            )
        {
            this.addAdminRepository = addAdminRepository;
            this.adminDecrypteEventHandler = adminDecrypteEventHandler;
            this.backgroundQueue = backgroundQueue;
            this.eventStoreRepository = eventStoreRepository;
        }

        //public AddAdminCommandHandler(IAddAdminRepository addAdminRepository)
        //{
        //    this.addAdminRepository = addAdminRepository;
        //}

        private async Task PasswordHashAsync(AdminModel adminModel)
        {
            try
            {
                adminModel.AdminLogin.Salt = await Salt.CreateAsync(ByteRange.byte256);
                adminModel.AdminLogin.Hash = await Hash.CreateAsync(adminModel?.AdminLogin?.Password, adminModel?.AdminLogin?.Salt, ByteRange.byte256);
            }
            catch
            {
                throw;
            }
        }

        async Task<bool> ICommandHandler<AdminModel, bool>.HandleAsync(AdminModel model)
        {
            try
            {
                string transactionId = Guid.NewGuid().ToString("N");

                model = await adminDecrypteEventHandler.EventHandleAsync(model);

                await PasswordHashAsync(model);

                var taskAddRepository = await this.addAdminRepository.AddAsync(model);

                // Record Add Command to Event Store
                backgroundQueue.Enqueue((cancellationToken) => this.eventStoreRepository?.SaveAsync(new EventModel()
                {
                    TransactionId = transactionId,
                    EventName = "AddAdminCommand",
                    Data = JsonConvert.SerializeObject(model),
                    CreatedDate = DateTime.Now.ToString()
                }));

                return taskAddRepository;
            }
            catch
            {
                throw;
            }
        }
    }
}