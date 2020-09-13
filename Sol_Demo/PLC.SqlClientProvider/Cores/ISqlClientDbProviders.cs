using PLC.Dapper.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PLC.SqlClientProvider.Cores
{
    public interface ISqlClientDbProviders : IDbProviders<SqlConnection>
    {
    }
}