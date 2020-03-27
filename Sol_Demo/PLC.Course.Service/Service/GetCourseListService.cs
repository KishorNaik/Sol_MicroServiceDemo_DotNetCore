using Newtonsoft.Json;
using Pathoschild.Http.Client;
using PLC.Course.Service.Core;
using PLC.Models.Cores;
using PLC.Models.Course;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.Course.Service.Service
{
    public sealed class GetCourseListService : IGetCourseListService
    {

        #region Declaration
        private readonly IClient client = null;
        #endregion

        #region Constructor
        public GetCourseListService(Func<String,IClient> funcClient)
        {
            this.client = funcClient("CourseListApi");
        }
        #endregion 

        async Task<dynamic> IGetCourseListService.GetCourseListEndPointAsync()
        {
            dynamic responseData = null;
            try
            {
                var data =
                        await
                        client
                        ?.PostAsync(CourseApiResource.GetCourseEndPoint)
                        ?.AsString();

                if (!data.Contains("StatusCode"))
                {
                    responseData = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(data);
                }
                
            }
            catch
            {
                throw;
            }

            return responseData;
        }
    }
}
