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

        private IDbConnection _dbConnection = null;
        private DynamicParameters _dynamicParameters = null;
        private dynamic _result = null;

        #endregion Declaration

        #region Public Method

        ISqlConnectionDapper ISqlDapper.SqlOpenConnection(IDbConnection dbConnection)
        {
            try
            {
                dbConnection?.OpenConnection();

                this._dbConnection = dbConnection;
            }
            catch
            {
                throw;
            }

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

        ISqlParameterDapper ISqlConnectionDapper.SqlParameter(Func<Task<DynamicParameters>> funcSqlParameter)
        {
            try
            {
                _dynamicParameters = funcSqlParameter().GetAwaiter().GetResult();
            }
            catch
            {
                throw;
            }

            return this;
        }

        ISqlCommandDapper ISqlParameterDapper.SqlCommand(Func<IDbConnection, DynamicParameters, dynamic> funcCommand)
        {
            try
            {
                _result = funcCommand(_dbConnection, _dynamicParameters);
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbConnection?.CloseConnection();
            }

            return this;
        }

        ISqlCommandDapper ISqlParameterDapper.SqlCommand<T>(Func<IDbConnection, DynamicParameters, T> funcCommand)
        {
            try
            {
                _result = funcCommand(_dbConnection, _dynamicParameters);
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbConnection?.CloseConnection();
            }

            return this;
        }

        public ISqlCommandDapper SqlCommand(Func<IDbConnection, DynamicParameters, Task<dynamic>> funcCommand)
        {
            try
            {
                _result = funcCommand(_dbConnection, _dynamicParameters).GetAwaiter().GetResult();
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbConnection?.CloseConnection();
            }

            return this;
        }

        public ISqlCommandDapper SqlCommand<T>(Func<IDbConnection, DynamicParameters, Task<T>> funcCommand)
        {
            try
            {
                _result = funcCommand(_dbConnection, _dynamicParameters).GetAwaiter().GetResult();
            }
            catch
            {
                throw;
            }
            finally
            {
                _dbConnection?.CloseConnection();
            }

            return this;
        }

        Task<T> ISqlCommandDapper.ResultAsync<T>()
        {
            return Task.Run(() =>
            {
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

        #endregion Public Method
    }
}