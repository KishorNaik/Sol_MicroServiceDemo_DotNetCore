using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.Admin.Api.Infrastructures.Abstracts;
using PLC.Admin.Api.Models;
using PLC.Dapper.Core.DbProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PLC.Admin.Api.Infrastructures.ResultSets;

namespace PLC.Admin.Api.Infrastructures.Repository
{
    public sealed class LoginAdminRepository : AdminRepositoryAbstract, ILoginAdminRepository
    {
        private readonly ISqlClientDbProviders sqlClientDbProviders = null;

        public LoginAdminRepository(ISqlClientDbProviders sqlClientDbProviders)
        {
            this.sqlClientDbProviders = sqlClientDbProviders;
        }

        async Task<AdminModel> ILoginAdminRepository.LoginAsync(AdminModel adminModel)
        {
            try
            {
                var taskConnection = sqlClientDbProviders?.GetConnectionAsync();
                var taskParameter = base.GetParameterAsync("Admin-Login", adminModel);

                var taskData =
                        base
                        .DapperFluent
                        ?.Value
                        ?.SqlOpenConnection(await taskConnection)
                        ?.SqlParameter(async () => await taskParameter)
                        ?.SqlCommand(async (dbConnection, parameter) =>
                        {
                            var data =
                                (await
                                    dbConnection
                                    ?.QueryAsync<GetAdminResultSet>("Admin.uspGetAdmin", param: parameter, commandType: CommandType.StoredProcedure)
                                )
                                ?.Select((getadminResultSet) => new AdminModel()
                                {
                                    AdminIdentity = getadminResultSet?.AdminIdentity,
                                    FirstName = getadminResultSet?.FirstName,
                                    LastName = getadminResultSet?.LastName,
                                    EmailId = getadminResultSet?.EmailId,
                                    Role = getadminResultSet?.Role,
                                    AdminLogin = new AdminLoginModel()
                                    {
                                        UserName = getadminResultSet?.UserName,
                                        Hash = getadminResultSet?.Hash,
                                        Salt = getadminResultSet?.Salt
                                    }
                                })
                                ?.FirstOrDefault();

                            return data;
                        })
                        ?.ResultAsync<AdminModel>();

                return (await taskData);
            }
            catch
            {
                throw;
            }
        }
    }
}