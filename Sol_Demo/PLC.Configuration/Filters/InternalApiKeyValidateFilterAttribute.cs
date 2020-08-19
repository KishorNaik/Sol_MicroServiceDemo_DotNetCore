using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PLC.AppSetting;
using PLC.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Configuration.Filters
{
    public class InternalApiKeyValidateFilterAttribute : Attribute, IActionFilter
    {
        private IActionResult UnAuthorizedResultSet
        {
            get => new UnauthorizedObjectResult(new ErrorModel()
            {
                StatusCode = 401,
                Message = "Unauthorized Access"
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
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
                        context.Result = this.UnAuthorizedResultSet;
                    }
                }
            }
            else
            {
                context.Result = this.UnAuthorizedResultSet;
            }
        }
    }
}