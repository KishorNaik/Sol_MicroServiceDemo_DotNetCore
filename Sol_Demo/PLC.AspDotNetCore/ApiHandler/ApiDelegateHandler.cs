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

        #region Public Method
        public async Task<IActionResult> HandlerAsync<TModel>(TModel model,Func<Task<dynamic>> funcResponse,Func<dynamic,IActionResult> funcResult, string badRequestMessage=null)
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

                    return funcResult(response);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IActionResult> HandlerAsync(Func<Task<dynamic>> funcResponse, Func<dynamic,IActionResult> funcResult)
        {
            try
            {
                    var response =
                           await
                                funcResponse();

                return funcResult(response);
            }
            catch
            {
                throw;
            }
        }
        #endregion 
    }
}
