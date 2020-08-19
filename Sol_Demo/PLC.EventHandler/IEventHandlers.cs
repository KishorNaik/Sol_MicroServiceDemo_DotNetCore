using System;
using System.Threading.Tasks;

namespace PLC.EventHandler
{
    public interface IEventHandlers<TModel, TResult> where TModel : class
    {
        Task<TResult> EventHandleAsync(TModel model);
    }
}