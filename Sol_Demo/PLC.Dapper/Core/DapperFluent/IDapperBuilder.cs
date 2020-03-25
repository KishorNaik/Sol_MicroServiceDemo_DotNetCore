using System;
using System.Collections.Generic;
using System.Text;

namespace PLC.Dapper.Core.DapperFluent
{
    public interface IDapperBuilder : ISqlDapper, ISqlConnectionDapper, ISqlParameterDapper, ISqlCommandDapper
    {
    }
}
