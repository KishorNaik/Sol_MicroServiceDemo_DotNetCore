using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PLC.AppSetting;
using PLC.AspDotNetCore.Middlewares;
using PLC.AspDotNetCore.Services;

namespace PLC.Course.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add Json Service
            services.AddJson(true);

            // Add Db Providers
            services.AddSqlClientDbProvider(DatabaseResource.PLC_Course_Uat, DatabaseResource.PLC_Course_Production);

            // Add Api Delegate Handler
            services.AddApiDelegateHandler();

            // Add Crypto Service
            services.AddCrypto();

            // Add GZip Response Compression
            services.AddGzipResponseCompression();

            // Add Course Di Service
            services.AddCourseDiService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            // Use Response Compression
            app.UseResponseCompression();

            // Use Custom Exception Handler
            app.UseException();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
