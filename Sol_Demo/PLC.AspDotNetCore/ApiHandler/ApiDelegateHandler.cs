using Microsoft.AspNetCore.Mvc;
using PLC.Models.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.AspDotNetCore.ApiHandler
{

    public sealed class ApiDelegateHandler : ControllerBase
    {
        public async Task<IActionResult> HandlerAsync<TModel>(TModel model,Func<Task<dynamic>> funcResponse,string badRequestMessage=null)
            where TModel : class
        {
            try
            {
                if (model == null) return base.BadRequest(new ErrorModel() { StatusCode = 400, Message = badRequestMessage ?? "Model should not be empty" });
                else
                {
                    var response =
                           await
                                funcResponse();

                    return base.Ok((Object)response);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
