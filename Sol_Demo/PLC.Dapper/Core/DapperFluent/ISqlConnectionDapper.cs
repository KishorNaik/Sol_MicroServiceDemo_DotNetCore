﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Dapper.Core.DapperFluent
{
    public interface ISqlConnectionDapper
    {

        ISqlParameterDapper SqlParameterAsync(Func<Task<DynamicParameters>> funcSqlParameters);

        ISqlParameterDapper SqlParameter(Func<DynamicParameters> funcSqlParameter);
    }
}
