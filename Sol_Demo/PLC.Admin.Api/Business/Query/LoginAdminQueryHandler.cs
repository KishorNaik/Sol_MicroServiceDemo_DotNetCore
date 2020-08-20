using AuthJwt.Generates;
using HashPassword;
using PLC.Admin.Api.Cores.Business.Events;
using PLC.Admin.Api.Cores.Business.Query;
using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.Admin.Api.Models;
using PLC.AppSetting;
using PLC.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Business.Query
{
    public sealed class LoginAdminQueryHandler : ILoginAdminQueryHandler
    {
        private readonly IAdminDecrypteEventHandler adminDecrypteEventHandler = null;
        private readonly ILoginAdminRepository loginAdminRepository = null;
        private readonly IGenerateJwtToken generateJwtToken = null;

        public LoginAdminQueryHandler(
            IAdminDecrypteEventHandler adminDecrypteEventHandler,
            ILoginAdminRepository loginAdminRepository,
            IGenerateJwtToken generateJwtToken
            )
        {
            this.adminDecrypteEventHandler = adminDecrypteEventHandler;
            this.loginAdminRepository = loginAdminRepository;
            this.generateJwtToken = generateJwtToken;
        }

        private ErrorModel ErrorMessage
        {
            get => new ErrorModel() { StatusCode = 200, Message = "User Name & Password does not matched" };
        }

        private async Task<AdminModel> AddJwtTokenAsync(AdminModel adminModel)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, Convert.ToString(adminModel.AdminIdentity))); // Id Base
                claims.Add(new Claim(ClaimTypes.Role, adminModel?.Role)); // Role Base

                // Generate Token
                adminModel.AdminLogin.JwtToken = await generateJwtToken.CreateJwtTokenAsync(AppResource.JwtSecretKey, claims.ToArray(), DateTime.Now.AddDays(1));

                return adminModel;
            }
            catch
            {
                throw;
            }
        }

        private async Task<dynamic> IsValidLoginAsync(bool isFlag, AdminModel adminModel)
        {
            try
            {
                return
                    (isFlag == true)
                    ? (dynamic)await this.AddJwtTokenAsync(adminModel)
                    : (dynamic)ErrorMessage;
            }
            catch
            {
                throw;
            }
        }

        async Task<dynamic> ILoginAdminQueryHandler.LoginAsync(AdminModel adminModel)
        {
            try
            {
                adminModel = await adminDecrypteEventHandler?.EventHandleAsync(adminModel);

                var adminData = await loginAdminRepository?.LoginAsync(adminModel);

                if (adminData != null)
                {
                    var flag = await Hash.ValidateAsync(adminModel?.AdminLogin?.Password, adminData?.AdminLogin?.Salt, adminData?.AdminLogin?.Hash, ByteRange.byte256);

                    return await this.IsValidLoginAsync(flag, adminData);
                }
                else
                {
                    return ErrorMessage;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}