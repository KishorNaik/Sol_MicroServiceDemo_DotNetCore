using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.Admin.Api.Infrastructures.Abstracts;
using PLC.Admin.Api.Models;
using PLC.Dapper.Core.DbProviders;
using PLC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PLC.UtilityModel;

namespace PLC.Admin.Api.Infrastructures.Repository
{
    public sealed class AddAdminRepository : AdminRepositoryAbstract, IAddAdminRepository
    {
        private readonly ISqlClientDbProviders sqlClientDbProviders = null;

        public AddAdminRepository(ISqlClientDbProviders sqlClientDbProviders)
        {
            this.sqlClientDbProviders = sqlClientDbProviders;
        }

        async Task<bool> IAddRepository<AdminModel, bool>.AddAsync(AdminModel model)
        {
            try
            {
                var taskConnection = sqlClientDbProviders.GetConnectionAsync();
                var taskParameter = base.SetParameterAsync("Add-Admin", model);

                var taskAdd =
                        base
                        .DapperFluent
                        ?.Value
                        ?.SqlOpenConnection(await taskConnection)
                        ?.SqlParameter(async () => await taskParameter)
                        ?.SqlCommand(async (dbConnection, dynamicParameter) =>
                        {
                            var queryData =
                                    (await
                                        dbConnection
                                        ?.QueryFirstAsync<MessageModel>("Admin.uspSetAdmin", param: dynamicParameter, commandType: CommandType.StoredProcedure)
                                    )
                                    ?.Message;

                            return (queryData.Contains("Insert")) ? true : false;
                        })
                        ?.ResultAsync<bool>();

                return (await taskAdd);
            }
            catch
            {
                throw;
            }
        }
    }
}