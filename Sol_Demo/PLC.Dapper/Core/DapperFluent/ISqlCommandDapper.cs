using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Dapper.Core.DapperFluent
{
    public interface ISqlCommandDapper
    {
        Task<T> ResultAsync<T>();
    }
}
