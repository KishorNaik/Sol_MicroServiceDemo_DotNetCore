using PLC.Dapper.Core.DbProviders;
using PLC.Models.Users;
using PLC.Users.Api.Cores.Repository;
using PLC.Users.Api.Infrastructures.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using HashPassword;
using AuthJwt.Generates;
using System.Security.Claims;
using PLC.AppSetting;
using PLC.Models.Cores;

namespace PLC.Users.Api.Infrastructures.Repository
{
    public sealed class LoginCredentailsValidateRepository : UsersAbstract, ILoginCredentailsValidateRepository
    {
        #region Declaration
        private readonly ISqlClientDbProviders sqlClientDbProviders = null;
        private readonly IGenerateJwtToken generateJwtToken = null;
        #endregion

        #region Constructor
        public LoginCredentailsValidateRepository(ISqlClientDbProviders sqlClientDbProviders, IGenerateJwtToken generateJwtToken)
        {
            this.sqlClientDbProviders = sqlClientDbProviders;
            this.generateJwtToken = generateJwtToken;
        }
        #endregion

        #region Private Method
        private async Task<UserModel> GetUsersDataAsync(IDbConnection leDbConnection, DynamicParameters leDynamicParameter)
        {
            try
            {
                return
                (await
                    leDbConnection
                    ?.QueryAsync<UserModel, UserLoginModel, UserModel>(
                        "Users.uspGetUsers",
                        (leUserModel, leUserLoginModel) => new UserModel()
                        {
                            UserIdentity = leUserModel?.UserIdentity,
                            FirstName = leUserModel?.FirstName,
                            LastName = leUserModel?.LastName,
                            UserLogin = new UserLoginModel()
                            {
                                UserName = leUserLoginModel?.UserName,
                                Hash = leUserLoginModel?.Hash,
                                Salt = leUserLoginModel?.Salt
                            }
                        },
                         param: leDynamicParameter,
                        commandType: CommandType.StoredProcedure, splitOn: "Split1")
                )
                ?.FirstOrDefault();
            }
            catch
            {
                throw;
            }
            
        }

        private async Task<dynamic> GenerateJwtTokenAsync(UserModel userModel,bool flag)
        {
            dynamic data = null;
            try
            {
                if (flag == true)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, Convert.ToString(userModel.UserIdentity)));

                    userModel.UserLogin.Token = await generateJwtToken.CreateJwtTokenAsync(AppResource.JwtSecretKey, claims.ToArray());

                    userModel.UserLogin.Password = null;

                    data = userModel;
                }
                else
                {
                    data = new ErrorModel()
                    {
                        StatusCode = 200,
                        Message = "User Name & Password Does not match."
                    };
                }

                return data;
            }
            catch
            {
                throw;
            }
        }

        
        #endregion

        async Task<dynamic> ILoginCredentailsValidateRepository.LoginValidateAsync(UserModel userModel)
        {
            dynamic responseData = null;
            try
            {
                return
                    await
                        base
                        .DapperFluent
                        ?.Value
                        ?.SqlOpenConnectionAsync(await sqlClientDbProviders.GetConnectionAsync())
                        ?.SqlParameterAsync(async () => await base.GetParameterAsync("Login-Credentails", userModel))
                        ?.SqlCommandAsync(async (leDbConnection, leDynamicParameter) =>
                        {
                            var userModelResult=await GetUsersDataAsync(leDbConnection, leDynamicParameter);

                            var flag= await Hash.ValidateAsync(userModel?.UserLogin?.Password, userModelResult?.UserLogin?.Salt, userModelResult?.UserLogin?.Hash, ByteRange.byte256);

                            responseData = await this.GenerateJwtTokenAsync(userModelResult, flag);

                            return responseData;

                        })
                        ?.ResultAsync<dynamic>();
                       
            }
            catch
            {
                throw;
            }
        }

       
    }
}
