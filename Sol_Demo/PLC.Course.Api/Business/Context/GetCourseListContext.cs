using PLC.Course.Api.Cores.Context;
using PLC.Course.Api.Cores.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Course.Api.Business.Context
{
    public sealed class GetCourseListContext : IGetCourseListContext
    {

        #region Declaration
        private readonly IGetCourseListRepository getCourseListRepository = null;
        private readonly ICourseEncrypteContext courseEncrypteContext = null;
        #endregion

        #region Constructor
        public GetCourseListContext(IGetCourseListRepository getCourseListRepository,ICourseEncrypteContext courseEncrypteContext)
        {
            this.getCourseListRepository = getCourseListRepository;
            this.courseEncrypteContext = courseEncrypteContext;
        }
        #endregion 

        #region Public Method
        async Task<dynamic> IGetCourseListContext.GetCourseListAsync()
        {
            try
            {
                var data =
                        await
                            getCourseListRepository
                            ?.GetCourseListAsync();

                var encrypteCourseList = await this.courseEncrypteContext.CourseListEncrypteAsync(data);

                return encrypteCourseList;
            }
            catch
            {
                throw;
            }
        }

        #endregion 
    }
}
