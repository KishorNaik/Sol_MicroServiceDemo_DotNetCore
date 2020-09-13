using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.Admin.Api.Infrastructures.Abstracts;
using PLC.Admin.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PLC.Admin.Api.Infrastructures.ResultSets;
using PLC.SqlClientProvider.Cores;

namespace PLC.Admin.Api.Infrastructures.Repository
{
    public sealed class GetAllAdminRepository : AdminRepositoryAbstract, IGetAllAdminRepository
    {
        private readonly ISqlClientDbProviders sqlClientDbProviders = null;

        public GetAllAdminRepository(ISqlClientDbProviders sqlClientDbProviders)
        {
            this.sqlClientDbProviders = sqlClientDbProviders;
        }

        async Task<IReadOnlyList<AdminModel>> IGetAllAdminRepository.GetAllAsync()
        {
            try
            {
                var taskConnection = this.sqlClientDbProviders?.GetConnectionAsync();
                var taskParameter = base.GetParameterAsync("Get-Admin");

                var taskData =
                        DapperFluent
                        ?.Value
                        ?.SqlOpenConnection(await taskConnection)
                        ?.SqlParameter(async () => await taskParameter)
                        ?.SqlCommand(async (dbConnection, dynamicParameter) =>
                        {
                            var rawData =
                                    dbConnection
                                    ?.QueryAsync<GetAdminResultSet>("Admin.uspGetAdmin", param: dynamicParameter, commandType: CommandType.StoredProcedure);

                            return
                                (await rawData)
                                ?.Select((getAdminResultSet) => new AdminModel()
                                {
                                    AdminIdentity = getAdminResultSet?.AdminIdentity,
                                    FirstName = getAdminResultSet?.FirstName,
                                    LastName = getAdminResultSet?.LastName,
                                    EmailId = getAdminResultSet?.EmailId,
                                    Role = getAdminResultSet?.Role,
                                    AdminLogin = new AdminLoginModel()
                                    {
                                        UserName = getAdminResultSet?.UserName
                                    }
                                })
                                ?.ToList();
                        })
                        ?.ResultAsync<IReadOnlyList<AdminModel>>();

                return (await taskData);
            }
            catch
            {
                throw;
            }
        }
    }
}