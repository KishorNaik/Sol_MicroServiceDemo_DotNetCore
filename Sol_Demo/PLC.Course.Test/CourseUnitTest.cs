using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pathoschild.Http.Client;
using PLC.AppSetting;
using PLC.Course.Service;
using PLC.Course.Service.Core;
using PLC.Course.Service.Service;
using System;
using System.Threading.Tasks;

namespace PLC.Course.Test
{
    [TestClass]
    public class CourseUnitTest
    {
        [TestMethod]
        public async  Task GetCourseListEndPoint()
        {
            IGetCourseListService getCourseListService = new GetCourseListService((leKey) =>
              {
                  FluentClient fluentClient = null ;  
                  if(leKey== "CourseListApi")
                  {
                      string baseUrl = (Convert.ToBoolean(AppResource.IsProduction) == true)
                                            ? CourseApiResource.CourseBaseUrlProduction
                                            : CourseApiResource.CourseBaseUrlUat;

                      fluentClient = new FluentClient(baseUrl);
                  }

                  return fluentClient;
              });

            var response = await getCourseListService.GetCourseListEndPointAsync();

            Assert.IsNotNull(response);


        }
    }
}
