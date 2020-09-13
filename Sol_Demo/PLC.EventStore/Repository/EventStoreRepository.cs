using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PLC.Dapper;
using PLC.Dapper.Helpers;
using System.Data;
using PLC.Dapper.Core.DbProviders;
using System.Runtime.CompilerServices;
using PLC.EventStore.Models;
using PLC.EventStore.Core.Repository;
using PLC.EventSource;

namespace PLC.EventStore.Repository
{
    public sealed class EventStoreRepository : DapperBaseAbstract<EventModel>, IEventStoreRepository
    {
        private readonly ISqlClientDbProviders sqlClientDbProviders = null;

        public EventStoreRepository(ISqlClientDbProviders sqlClientDbProviders)
        {
            this.sqlClientDbProviders = sqlClientDbProviders;
        }

        async Task<EventModel> IReadEventSource<EventModel>.ReadAsync(string transactionId)
        {
            try
            {
                var taskConnection = sqlClientDbProviders.GetConnectionAsync();

                return
                    await
                    base
                    .DapperFluent
                    ?.Value
                    ?.SqlOpenConnection(await taskConnection)
                    ?.SqlParameter(() =>
                    {
                        var dynamicParameter = new DynamicParameters();
                        dynamicParameter.Add("@TransactionId", transactionId, DbType.String, ParameterDirection.Input);

                        return dynamicParameter;
                    })
                    ?.SqlCommand(async (dbConnection, dynamicParameter) =>
                    {
                        var command = "SELECT * FROM dbo.EventStore WHERE TransactionId=@TransactionId";

                        var data =
                            await
                            dbConnection
                            ?.QueryFirstAsync<EventModel>(command, dynamicParameter, commandType: CommandType.Text);

                        return data;
                    })
                    ?.ResultAsync<EventModel>();
            }
            catch
            {
                throw;
            }
        }

        async Task ICreateEventStore<EventModel>.SaveAsync(EventModel eventModel)
        {
            try
            {
                var taskConnection = sqlClientDbProviders.GetConnectionAsync();

                await
                    base
                    .DapperFluent
                    ?.Value
                    ?.SqlOpenConnection(await taskConnection)
                    ?.SqlParameter(() =>
                    {
                        var dynamicParameter = new DynamicParameters();
                        dynamicParameter.Add("@TransactionId", eventModel?.TransactionId, DbType.String, ParameterDirection.Input);
                        dynamicParameter.Add("@EventName", eventModel?.EventName, DbType.String, ParameterDirection.Input);
                        dynamicParameter.Add("@OldData", eventModel?.OldData, DbType.String, ParameterDirection.Input);
                        dynamicParameter.Add("@NewData", eventModel?.NewData, DbType.String, ParameterDirection.Input);
                        dynamicParameter.Add("@CreatedDate", Convert.ToDateTime(eventModel?.CreatedDate), DbType.DateTime, ParameterDirection.Input);

                        return dynamicParameter;
                    })
                    ?.SqlCommand(async (dbConnection, dynamicParameter) =>
                    {
                        var command =
                                new StringBuilder()
                                .Append("INSERT INTO dbo.EventStore ")
                                .Append("(TransactionId,EventName,OldData,NewData,CreatedDate) ")
                                .Append("VALUES ")
                                .Append("(@TransactionId,@EventName,@OldData,@NewData,@CreatedDate)")
                                .ToString();

                        await
                            dbConnection
                            .ExecuteAsync(command, dynamicParameter, commandType: CommandType.Text);

                        return true;
                    })

                    ?.ResultAsync<bool>();
            }
            catch
            {
                throw;
            }
        }
    }
}