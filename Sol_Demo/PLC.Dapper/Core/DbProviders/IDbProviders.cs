using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Dapper.Core
{
    public interface IDbProviders<TConnection> where TConnection : class
    {
        Task<TConnection> GetConnectionAsync();
    }
}
