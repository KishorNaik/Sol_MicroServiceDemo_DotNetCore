using Dapper;
using PLC.Dapper.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PLC.Dapper.Core.DapperFluent;

namespace PLC.Dapper.Helpers
{
    public abstract class DapperBaseAbstract<TModel> where TModel :class
    {
        #region Property

        private Lazy<IDapperBuilder> _dapperBuilder = null;

        protected Lazy<IDapperBuilder> DapperFluent
        {
            get
            {
                return
                    _dapperBuilder
                        ??
                            (
                                _dapperBuilder = new Lazy<IDapperBuilder>(() => new DapperBuilder())
                            );
            }
        }

        #endregion Property

        #region Private Methods

        private Task<DynamicParameters> ParameterAsync(string command)
        {
            try
            {
                    DynamicParameters dynamicParameters = new DynamicParameters();

                    dynamicParameters
                    .Add("@Command", command, DbType.String, ParameterDirection.Input);

                    return Task.FromResult<DynamicParameters>( dynamicParameters);
               
            }
            catch
            {
                throw;
            }
        }

        #endregion Private Methods

        #region Protected Method
        protected async Task<dynamic> ReadMessageFromUspAsync(SqlMapper.GridReader queryData)
        {
            try
            {

                var dapperCommandFromProc
                    =
                    (
                        await
                        queryData
                        .ReadAsync<dynamic>()
                    )
                    .AsEnumerable()
                    .Select((leDynamicCommand) => new
                    {
                        Command = leDynamicCommand.Command
                    })
                    .ToList()
                    .FirstOrDefault()
                    .Command;
                return dapperCommandFromProc;
            }
            catch
            {
                throw;
            }
        }

        protected  Task<dynamic> ReadMessageFromUspAsync(IEnumerable<dynamic> listDynamic)
        {
            dynamic data = null;
            try
            {

                    if (listDynamic != null)
                    {
                        data =
                            listDynamic
                            .AsEnumerable()
                            .Select((leCommand) => new
                            {
                                Command = leCommand.Command
                            })
                            .ToList()
                            .FirstOrDefault()
                            .Command;
                    }

                    return Task.FromResult<dynamic>(data);

            }
            catch
            {
                throw;
            }
        }

        protected virtual async Task<DynamicParameters> SetParameterAsync(String command, TModel model = null)
        {
            try
            {
                return await ParameterAsync(command);
            }
            catch
            {
                throw;
            }
        }

        protected async virtual Task<DynamicParameters> GetParameterAsync(string command, TModel model = null)
        {
            try
            {
                return await ParameterAsync(command);
            }
            finally
            { }
        }
        #endregion 

    }
}
