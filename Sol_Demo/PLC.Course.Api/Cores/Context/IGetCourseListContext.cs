using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Course.Api.Cores.Context
{
    public interface IGetCourseListContext
    {
        Task<dynamic> GetCourseListAsync();
    }
}
