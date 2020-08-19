using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Repository
{
    public interface IDeleteRepository<TModel, TResult> where TModel : class
    {
        Task<TResult> DeleteAsync(TModel model);
    }
}