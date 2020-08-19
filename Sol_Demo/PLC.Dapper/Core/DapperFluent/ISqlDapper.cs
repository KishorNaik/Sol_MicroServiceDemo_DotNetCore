using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PLC.Dapper.Core.DapperFluent
{
    public interface ISqlDapper
    {
        ISqlConnectionDapper SqlOpenConnection(IDbConnection dbConnection);
    }
}