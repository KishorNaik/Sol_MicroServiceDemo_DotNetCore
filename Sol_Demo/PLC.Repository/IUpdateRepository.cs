using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Repository
{
    public interface IUpdateRepository<TModel, TResult> where TModel : class
    {
        Task<TResult> UpdateAsync(TModel model);
    }
}