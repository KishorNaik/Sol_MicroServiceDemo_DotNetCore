using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Dapper.Core.DapperFluent
{
    public interface ISqlParameterDapper
    {
        ISqlCommandDapper SqlCommand(Func<IDbConnection, DynamicParameters, dynamic> funcCommand);

        ISqlCommandDapper SqlCommand<T>(Func<IDbConnection, DynamicParameters, T> funcCommand);

        ISqlCommandDapper SqlCommand(Func<IDbConnection, DynamicParameters, Task<dynamic>> funcCommand);

        ISqlCommandDapper SqlCommand<T>(Func<IDbConnection, DynamicParameters, Task<T>> funcCommand);
    }
}