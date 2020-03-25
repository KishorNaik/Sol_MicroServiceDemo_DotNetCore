using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PLC.AppSetting;
using PLC.Models.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.AspDotNetCore.Filters
{
    public class InternalApiKeyValidateFilterAttribute : Attribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var requestHeader = context.HttpContext.Request.Headers;

            if (requestHeader.ContainsKey("InternalApiKey"))
            {
                if (requestHeader.TryGetValue("InternalApiKey", out var headerValues))
                {
                    string token = headerValues.First();

                    if (AppResource.InternalApiKey != token)
                    {
                        context.Result = new UnauthorizedObjectResult(new ErrorModel()
                        {
                            StatusCode = 401,
                            Message = "Unauthorized Access"
                        });
                    }

                }
            }
        }
    }
}
