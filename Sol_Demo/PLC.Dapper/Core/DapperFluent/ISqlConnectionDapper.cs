using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Dapper.Core.DapperFluent
{
    public interface ISqlConnectionDapper
    {
        ISqlParameterDapper SqlParameter(Func<DynamicParameters> funcSqlParameter);

        ISqlParameterDapper SqlParameter(Func<Task<DynamicParameters>> funcSqlParameter);
    }
}