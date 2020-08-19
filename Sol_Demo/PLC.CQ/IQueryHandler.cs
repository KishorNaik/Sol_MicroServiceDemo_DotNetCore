using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.CQ
{
    public interface IQueryHandler<TModel, TResult>
    {
        Task<TResult> HandleAsync(TModel model);
    }

    public interface IQueryHandler<TResult>
    {
        Task<TResult> HandleAsync();
    }
}