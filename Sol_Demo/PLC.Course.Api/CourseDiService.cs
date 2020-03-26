using Microsoft.Extensions.DependencyInjection;
using PLC.Course.Api.Business.Context;
using PLC.Course.Api.Cores.Context;
using PLC.Course.Api.Cores.Repository;
using PLC.Course.Api.Infrastructures.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Course.Api
{
    public static  class CourseDiService
    {
        public static void AddCourseDiService(this IServiceCollection services)
        {
            services.AddTransient<IGetCourseListContext, GetCourseListContext>();
            services.AddTransient<ICourseEncrypteContext, CourseEncrypteContext>();

            services.AddTransient<IGetCourseListRepository, GetCourseListRepository>();
        }
    }
}
