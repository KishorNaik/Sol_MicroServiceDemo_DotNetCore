using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace PLC.Configuration.Services
{
    public static class JsonServiceExtension
    {
        public static void AddJson(this IServiceCollection services, bool isPascalCase)
        {
            services
                 .AddControllers()
                 .AddJsonOptions((leSetUpAction) =>
                 {
                     if (isPascalCase == true)
                     {
                         // Pascal Casing
                         leSetUpAction.JsonSerializerOptions.PropertyNamingPolicy = null;
                     }

                     // Ignore Json Property Null Value from Response
                     leSetUpAction.JsonSerializerOptions.IgnoreNullValues = true;
                 });
        }
    }
}