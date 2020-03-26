using PLC.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Course.Api.Cores.Context
{
    public interface ICourseEncrypteContext
    {
        Task<IEnumerable<CourseModel>> CourseListEncrypteAsync(IEnumerable<CourseModel> courseModels);
    }
}
