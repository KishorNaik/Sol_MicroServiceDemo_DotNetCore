using Microsoft.VisualStudio.TestTools.UnitTesting;
using PLC.Models.Users;
using PLC.Users.Service.Core;
using PLC.Users.Service.Service;
using System.Threading.Tasks;

namespace PLC.Users.UnitTest
{
    [TestClass]
    public class UserCryptoUnitTest
    {
        [TestMethod]
        public async Task UserEncryptionEndPointTest()
        {
            IUserEncryptionService userEncryptionService = new UserEncryptionService();

            var userModel = new UserModel()
            {
                FirstName = "Kishor",
                LastName = "Naik",
                UserCommunication = new UserCommunicationModel()
                {
                    EmailId = "kishor.naik011.net@gmail.com",
                    MobileNo = "9167791119"
                },
                UserLogin = new UserLoginModel()
                {
                    UserName = "kishor11",
                    Password = "123"
                }
            };

            var encrypteModel = await userEncryptionService?.UserEncrypteEndPointAsync(userModel);

            Assert.IsNotNull(encrypteModel);

        }
    }
}
