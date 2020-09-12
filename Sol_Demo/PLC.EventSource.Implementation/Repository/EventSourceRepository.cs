﻿using PLC.EventSource.Implementation.Core;
using PLC.EventSource.Implementation.Models;
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

namespace PLC.EventSource.Implementation.Repository
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
                        dynamicParameter.Add("@Data", eventModel?.Data, DbType.String, ParameterDirection.Input);
                        dynamicParameter.Add("@CreatedDate", eventModel?.CreatedDate, DbType.String, ParameterDirection.Input);

                        return dynamicParameter;
                    })
                    ?.SqlCommand(async (dbConnection, dynamicParameter) =>
                    {
                        var command =
                                new StringBuilder()
                                .Append("INSERT INTO dbo.EventStore ")
                                .Append("(TransactionId,EventName,Data,CreatedDate) ")
                                .Append("VALUES ")
                                .Append("(@TransactionId,@EventName,@Data,@CreatedDate)")
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