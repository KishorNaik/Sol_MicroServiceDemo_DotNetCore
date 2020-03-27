using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Course.Service.Core
{
    public interface IGetCourseListService
    {
        Task<dynamic> GetCourseListEndPointAsync();
    }
}
