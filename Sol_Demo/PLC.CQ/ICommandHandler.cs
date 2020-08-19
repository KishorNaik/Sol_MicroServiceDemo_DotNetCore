using System;
using System.Threading.Tasks;

namespace PLC.CQ
{
    public interface ICommandHandler<TModel, TResult> where TModel : class
    {
        Task<TResult> HandleAsync(TModel model);
    }

    public interface ICommandHandler<TResult>
    {
        Task<TResult> HandleAsync();
    }
}