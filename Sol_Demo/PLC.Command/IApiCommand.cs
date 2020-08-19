using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLC.ApiCommand
{
    public interface IApiCommand<TController> where TController : ControllerBase
    {
        Task<IActionResult> ExecuteAsync(TController controller);
    }

    public interface IApiCommand<TController, TModel>
        where TController : ControllerBase
        where TModel : class
    {
        Task<IActionResult> ExecuteAsync(TController controller, TModel model);
    }
}