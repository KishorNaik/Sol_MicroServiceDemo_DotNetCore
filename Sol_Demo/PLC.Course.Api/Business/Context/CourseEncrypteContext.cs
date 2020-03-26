using Microsoft.AspNetCore.DataProtection;
using PLC.AppSetting;
using PLC.Course.Api.Cores.Context;
using PLC.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Course.Api.Business.Context
{
    public sealed class CourseEncrypteContext : ICourseEncrypteContext
    {
        #region Declaration
        private readonly IDataProtector dataProtector = null;
        #endregion

        #region Constructor
        public CourseEncrypteContext(IDataProtectionProvider provider)
        {
            this.dataProtector= provider.CreateProtector(AppResource.CryptoSecretKey);
        }
        #endregion 

        Task<IEnumerable<CourseModel>> ICourseEncrypteContext.CourseListEncrypteAsync(IEnumerable<CourseModel> courseModels)
        {
            try
            {
                if (courseModels?.Count() >= 1)
                {
                    var listCourseModelList = new List<CourseModel>();

                    courseModels
                        ?.ToList()
                        ?.ForEach((leCourseAction) =>
                        {
                            listCourseModelList.Add(new CourseModel()
                            {
                                CourseIdentity = (!string.IsNullOrEmpty(leCourseAction?.CourseIdentity) ? this.dataProtector.Protect(leCourseAction?.CourseIdentity) : null),
                                CourseName = (!string.IsNullOrEmpty(leCourseAction?.CourseName) ? this.dataProtector.Protect(leCourseAction.CourseName) : null)
                            });
                        });

                    return Task.FromResult<IEnumerable<CourseModel>>(listCourseModelList);
                }

                return Task.FromResult<IEnumerable<CourseModel>>(null);
            }
            catch
            {
                throw;
            }
        }
    }
}
