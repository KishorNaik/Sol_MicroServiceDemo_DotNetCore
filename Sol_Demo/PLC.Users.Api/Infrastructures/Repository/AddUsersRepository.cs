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
using PLC.Models.Cores;

namespace PLC.Users.Api.Infrastructures.Repository
{
    public sealed class AddUsersRepository : UsersAbstract, IAddUsersRepository
    {
        #region Declaration
        private readonly ISqlClientDbProviders sqlClientDbProviders = null;
        #endregion

        #region Constructor
        public AddUsersRepository(ISqlClientDbProviders sqlClientDbProviders)
        {
            this.sqlClientDbProviders = sqlClientDbProviders;
        }
        #endregion 

        #region Public Method
        async Task<dynamic> IAddUsersRepository.AddUserAsync(UserModel userModel)
        {
            try
            {
                var data =
                        await
                            base
                            .DapperFluent
                            ?.Value
                            ?.SqlOpenConnectionAsync(await sqlClientDbProviders.GetConnectionAsync())
                            ?.SqlParameterAsync(async () => await base.SetParameterAsync("Add-Users", userModel))
                            ?.SqlCommandAsync(async (leDbConnection, leDynamicParameter) =>
                            {
                                
                                    var data =
                                        (await
                                        leDbConnection
                                        ?.QueryAsync<MessageModel>("Users.uspSetUsers", param: leDynamicParameter, commandType: CommandType.StoredProcedure)
                                        )
                                        ?.FirstOrDefault();

                                    var response =
                                  (data?.Message?.Contains("Inserted") == true)
                                      ? (dynamic)true
                                      : (dynamic)new ErrorModel()
                                      {
                                          StatusCode = 200,
                                          Message = data.Message
                                      };

                                    return response;
                            })
                            ?.ResultAsync<dynamic>();

                return data;

            }
            catch
            {
                throw;
            }
        }

        #endregion 
    }
}
