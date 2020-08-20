using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PLC.Admin.Api.Business.Query;
using PLC.Admin.Api.Cores.Business.Events;
using PLC.Admin.Api.Cores.Business.Query;
using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.Admin.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Admin.UnitTest
{
    [TestClass]
    public class GetAdminUnitTest
    {
        private Mock<IGetAllAdminRepository> getAllAdminRepositoryMock = null;
        private Mock<IAdminEncrypteListEventHandler> getEncrypteListEventHandlerMock = null;

        private IGetAllAdminQuery getAllAdminQuery = null;

        public GetAdminUnitTest()
        {
            getAllAdminRepositoryMock = new Mock<IGetAllAdminRepository>();
            getEncrypteListEventHandlerMock = new Mock<IAdminEncrypteListEventHandler>();
            getAllAdminQuery = new GetAllAdminQuery(getAllAdminRepositoryMock.Object, getEncrypteListEventHandlerMock.Object);
        }

        [TestMethod]
        public async Task NullGetAdminData()
        {
            getAllAdminRepositoryMock
                ?.Setup((r) => r.GetAllAsync())
                ?.ReturnsAsync((IReadOnlyList<AdminModel>)null);

            var data = await getAllAdminQuery?.HandleAsync();
            Assert.AreEqual<String>("No data found", ((dynamic)data).Message);
        }

        [TestMethod]
        public async Task ZeroCountGetAdminData()
        {
            var listData = new List<AdminModel>();

            getAllAdminRepositoryMock
                ?.Setup((r) => r.GetAllAsync())
                ?.ReturnsAsync(listData.ToList());

            var data = await getAllAdminQuery?.HandleAsync();
            Assert.AreEqual<String>("No data found", ((dynamic)data).Message);
        }

        [TestMethod]
        public async Task GetAdminData()
        {
            var listData = new List<AdminModel>();
            listData.Add(new AdminModel()
            {
                AdminIdentity = "xxx",
                FirstName = "Kishor",
                LastName = "Naik",
                EmailId = "Test@gmail.com",
                Role = "Admin",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "kishor11",
                    Password = "12345",
                    Salt = "xxx",
                    Hash = "xxx"
                }
            });
            listData.Add(new AdminModel()
            {
                AdminIdentity = "xxx11",
                FirstName = "Eshaan",
                LastName = "Naik1",
                EmailId = "eshaan@gmail.com",
                Role = "Admin1",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "eshaan11",
                    Password = "123451",
                    Salt = "xxx1",
                    Hash = "xxx1"
                }
            });

            IReadOnlyList<AdminModel> adminModels = listData?.ToList();

            getAllAdminRepositoryMock
                ?.Setup((r) => r.GetAllAsync())
                ?.ReturnsAsync(adminModels);

            getEncrypteListEventHandlerMock
                .Setup(x => x.EventHandleAsync(It.IsAny<List<AdminModel>>()))
                ?.ReturnsAsync(adminModels.ToList());

            var data = await getAllAdminQuery?.HandleAsync();

            Assert.IsNotNull(data);
        }

        [TestMethod]
        public async Task GetAdminDataNull()
        {
            var listData = new List<AdminModel>();
            listData.Add(new AdminModel()
            {
                AdminIdentity = "xxx",
                FirstName = "Kishor",
                LastName = "Naik",
                EmailId = "Test@gmail.com",
                Role = "Admin",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "kishor11",
                    Password = "12345",
                    Salt = "xxx",
                    Hash = "xxx"
                }
            });
            listData.Add(new AdminModel()
            {
                AdminIdentity = "xxx11",
                FirstName = "Eshaan",
                LastName = "Naik1",
                EmailId = "eshaan@gmail.com",
                Role = "Admin1",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "eshaan11",
                    Password = "123451",
                    Salt = "xxx1",
                    Hash = "xxx1"
                }
            });

            IReadOnlyList<AdminModel> adminModels = listData?.ToList();

            getAllAdminRepositoryMock
                ?.Setup((r) => r.GetAllAsync())
                ?.ReturnsAsync(adminModels);

            getEncrypteListEventHandlerMock
                .Setup(x => x.EventHandleAsync(It.IsAny<List<AdminModel>>()))
                .ReturnsAsync(null as List<AdminModel>);

            var data = await getAllAdminQuery?.HandleAsync();

            Assert.IsNull(data);
        }

        [TestMethod]
        public async Task GetAdminEncryptionFailed()
        {
            var listData = new List<AdminModel>();
            listData.Add(new AdminModel()
            {
                AdminIdentity = "xxx",
                FirstName = "Kishor",
                LastName = "Naik",
                EmailId = "Test@gmail.com",
                Role = "Admin",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "kishor11",
                    Password = "12345",
                    Salt = "xxx",
                    Hash = "xxx"
                }
            });
            listData.Add(new AdminModel()
            {
                AdminIdentity = "xxx11",
                FirstName = "Eshaan",
                LastName = "Naik1",
                EmailId = "eshaan@gmail.com",
                Role = "Admin1",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "eshaan11",
                    Password = "123451",
                    Salt = "xxx1",
                    Hash = "xxx1"
                }
            });

            IReadOnlyList<AdminModel> adminModels = listData?.ToList();

            getAllAdminRepositoryMock
                ?.Setup((r) => r.GetAllAsync())
                ?.ReturnsAsync(adminModels);

            getEncrypteListEventHandlerMock
                .Setup(x => x.EventHandleAsync(It.IsAny<List<AdminModel>>()))
                .Throws<Exception>();

            try
            {
                var data = await getAllAdminQuery?.HandleAsync();
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}