using System;
using System.Threading.Tasks;

namespace PLC.Repository
{
    public interface IAddRepository<TModel, TResult> where TModel : class
    {
        Task<TResult> AddAsync(TModel model);
    }
}