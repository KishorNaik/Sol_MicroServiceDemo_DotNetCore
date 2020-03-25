using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PLC.Dapper.Core;
using PLC.Dapper.Core.DapperFluent;
using PLC.Dapper.Extension;

namespace PLC.Dapper.Helpers
{
    public class DapperBuilder : IDapperBuilder
    {

        #region Declaration
        private  IDbConnection _dbConnection = null;
        private DynamicParameters _dynamicParameters = null;
        private dynamic _result = null;
        #endregion

        #region Public Method

        ISqlConnectionDapper ISqlDapper.SqlOpenConnectionAsync(IDbConnection dbConnection)
        {
           
                Task.Run(async () => {

                    try
                    {
                        await dbConnection?.OpenConnectionAsync();

                        this._dbConnection = dbConnection;
                    }
                    catch
                    {
                        throw;
                    }

                }).Wait();

                return this;
           
        }

        ISqlParameterDapper ISqlConnectionDapper.SqlParameter(Func<DynamicParameters> funcSqlParameter)
        {
            try
            {
                _dynamicParameters = funcSqlParameter();
            }
            catch
            {
                throw;
            }

            return this;
        }

        ISqlParameterDapper ISqlConnectionDapper.SqlParameterAsync(Func<Task<DynamicParameters>> funcSqlParameters)
        {
          
                Task.Run(async () => {

                    try
                    {
                        _dynamicParameters = await funcSqlParameters();
                    }
                    catch
                    {
                        throw;
                    }

                }).Wait();
          


            return this;
        }

        ISqlCommandDapper ISqlParameterDapper.SqlCommand(Func<IDbConnection, DynamicParameters, dynamic> funcCommand)
        {
            
                try
                {
                    _result =  funcCommand(_dbConnection, _dynamicParameters);

                     _dbConnection?.CloseConnectionAsync().Wait();
                }
                catch
                {
                    throw;
                }

         
            return this;

        }

        ISqlCommandDapper ISqlParameterDapper.SqlCommand<T>(Func<IDbConnection, DynamicParameters, T> funcCommand)
        {
            try
            {
                _result = funcCommand(_dbConnection, _dynamicParameters);

                _dbConnection?.CloseConnectionAsync().Wait();
            }
            catch
            {
                throw;
            }


            return this;
        }

        ISqlCommandDapper ISqlParameterDapper.SqlCommandAsync(Func<IDbConnection, DynamicParameters, Task<dynamic>> funcCommand)
        {
            
                Task.Run(async () => {

                    try
                    {
                        _result = await funcCommand(_dbConnection, _dynamicParameters);

                        await _dbConnection?.CloseConnectionAsync();
                    }
                    catch
                    {
                        throw;
                    }

                }).Wait();

                return this;
           
            
        }

        ISqlCommandDapper ISqlParameterDapper.SqlCommandAsync<T>(Func<IDbConnection, DynamicParameters, Task<T>> funcCommand)
        {
            Task.Run(async () => {

                try
                {
                    _result = await funcCommand(_dbConnection, _dynamicParameters);

                    await _dbConnection?.CloseConnectionAsync();
                }
                catch
                {
                    throw;
                }

            }).Wait();

            return this;

        }

        

        async Task<T> ISqlCommandDapper.ResultAsync<T>()
        {
         
                return await Task.Run(() => {

                    try
                    {
                        return (T)(dynamic)_result;
                    }
                    catch
                    {
                        throw;
                    }

                });
          
        }
        #endregion
    }
}
