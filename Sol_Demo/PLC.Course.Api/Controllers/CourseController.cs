using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLC.AspDotNetCore.ApiHandler;
using PLC.Course.Api.Cores.Context;
using PLC.Models.Cores;

namespace PLC.Course.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        #region Declaration
        private readonly ApiDelegateHandler apiDelegateHandler = null;
        #endregion

        #region Constructor
        public CourseController(ApiDelegateHandler apiDelegateHandler)
        {
            this.apiDelegateHandler = apiDelegateHandler;
        }
        #endregion

        #region Public EndPoint
        [HttpPost("getcourselist")]
        public async Task<IActionResult> GetCourseListAsync([FromServices] IGetCourseListContext getCourseListContext)
        {
            try
            {
                return
                    await
                        this
                        .apiDelegateHandler
                        ?.HandlerAsync(
                                async () => await getCourseListContext?.GetCourseListAsync(),
                                (leResponse)=>
                                {
                                    if (leResponse == null) return base.Ok(new ErrorModel()
                                    {
                                        StatusCode = 200,
                                        Message = "Course List is Empty"
                                    });
                                    else
                                    {
                                        return base.Ok((Object)leResponse);
                                    }
                                }
                        
                        );
            }
            catch
            {
                throw;
            }
        }
        #endregion 
    }
}