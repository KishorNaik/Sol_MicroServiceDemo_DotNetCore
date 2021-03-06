using DalSoft.Hosting.BackgroundQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PLC.Admin.Api.Business.Commands;
using PLC.Admin.Api.Cores.Business.Commands;
using PLC.Admin.Api.Cores.Business.Events;
using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.Admin.Api.Models;
using PLC.CQ;
using PLC.EventHandler;
using PLC.EventStore.Core;
using PLC.EventStore.Core.Repository;
using PLC.EventStore.Models;
using PLC.Repository;
using System;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace PLC.Admin.UnitTest
{
    [TestClass]
    public class AddAdminUnitTest
    {
        private Mock<IAddAdminRepository> adminRepositoryMock = null;
        private Mock<IAdminDecrypteEventHandler> adminDecrypteEventHandlerMock = null;
        private BackgroundQueue backgroundQueueMock = null;
        private Mock<IEventStoreRepository> eventStoreRepositoryMock = null;

        private IAddAdminCommandHandler addAdminCommandHandler = null;

        public AddAdminUnitTest()
        {
            adminRepositoryMock = new Mock<IAddAdminRepository>();
            adminDecrypteEventHandlerMock = new Mock<IAdminDecrypteEventHandler>();
            backgroundQueueMock = new BackgroundQueue((e) => { }, 1, 1000);
            eventStoreRepositoryMock = new Mock<IEventStoreRepository>();

            addAdminCommandHandler = new AddAdminCommandHandler(adminRepositoryMock.Object, adminDecrypteEventHandlerMock.Object, backgroundQueueMock, eventStoreRepositoryMock.Object);
        }

        [TestMethod]
        public async Task AddAdminFailed()
        {
            var adminModel = new AdminModel()
            {
                AdminIdentity = "1212-12-13",
                FirstName = "Eshaan",
                LastName = "Naik",
                EmailId = "eshaan@gmail.com",
                Role = "Admin",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "eshaan01",
                    Password = "12345",
                    Salt = "xxx",
                    Hash = "xxx"
                }
            };

            //var adminModel = new Mock<AdminModel>();

            adminRepositoryMock
               .Setup((r) => r.AddAsync(adminModel))
               .ReturnsAsync(false);

            adminDecrypteEventHandlerMock
                .Setup((e) => e.EventHandleAsync(adminModel))
                .ReturnsAsync(
                   adminModel
                );

            var data = await addAdminCommandHandler.HandleAsync(adminModel);

            Assert.IsFalse(data);
        }

        [TestMethod]
        public async Task AddAdminSuccess()
        {
            var adminModel = new AdminModel()
            {
                AdminIdentity = "1212-12-13",
                FirstName = "Eshaan",
                LastName = "Naik",
                EmailId = "eshaan@gmail.com",
                Role = "Admin",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "eshaan01",
                    Password = "12345",
                    Salt = "xxx",
                    Hash = "xxx"
                }
            };

            //adminRepositoryMock
            //   .Setup((r) => r.AddAsync(adminModel))
            //   .ReturnsAsync(true);

            //adminDecrypteEventHandlerMock
            //    .Setup((e) => e.EventHandleAsync(adminModel))
            //    .ReturnsAsync(
            //       adminModel
            //    );

            adminRepositoryMock
              .Setup((r) => r.AddAsync(It.IsAny<AdminModel>()))
              .ReturnsAsync(true);

            adminDecrypteEventHandlerMock
                .Setup((e) => e.EventHandleAsync(It.IsAny<AdminModel>()))
                .ReturnsAsync(
                   adminModel
                );

            eventStoreRepositoryMock
                .Setup((e) => e.SaveAsync(It.IsAny<EventModel>()));

            var data = await addAdminCommandHandler.HandleAsync(adminModel);

            Assert.IsTrue(data);
        }
    }
}