using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PLC.Admin.Cryptography.Api.Models;
using PLC.AppSetting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLC.Admin.Cryptography.UnitTest
{
    [TestClass]
    public class AdminEncrypteUnitTest
    {
        [TestMethod]
        public void AdminEncryptionTestMethod()
        {
            var adminModel = new AdminModel()
            {
                FirstName = "Kishor",
                LastName = "Naik",
                EmailId = "kishor.naik011.net@gmail.com",
                Role = "Admin",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "kishornaik",
                    Password = "12345"
                }
            };

            var userModelJson = JsonConvert.SerializeObject(adminModel);
            Assert.IsNotNull(adminModel);
        }

        [TestMethod]
        public void AdminEncryptionListTestMethod()
        {
            var adminModel1 = new AdminModel()
            {
                FirstName = "Kishor",
                LastName = "Naik",
                EmailId = "kishor.naik011.net@gmail.com",
                Role = "Admin",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "kishornaik",
                    Password = "12345"
                }
            };

            var adminModel2 = new AdminModel()
            {
                FirstName = "Eshaan",
                LastName = "Naik",
                EmailId = "eshaan@gmail.com",
                Role = "Admin",
                AdminLogin = new AdminLoginModel()
                {
                    UserName = "eshaan07",
                    Password = "12345"
                }
            };

            List<AdminModel> listAdminModel = new List<AdminModel>();
            listAdminModel.Add(adminModel1);
            listAdminModel.Add(adminModel2);

            var userModelJson = JsonConvert.SerializeObject(listAdminModel);
            Assert.IsNotNull(userModelJson);
        }
    }
}