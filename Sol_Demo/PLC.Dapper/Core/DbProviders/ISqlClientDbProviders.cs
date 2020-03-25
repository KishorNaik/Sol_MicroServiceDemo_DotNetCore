using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PLC.Dapper.Core.DbProviders
{
    public interface ISqlClientDbProviders : IDbProviders<SqlConnection>
    {
    }
}
