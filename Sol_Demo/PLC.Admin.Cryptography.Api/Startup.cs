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
using PLC.Admin.Cryptography.Api.Configurations.Services;
using PLC.Configuration.Middlewares;
using PLC.Configuration.Services;

namespace PLC.Admin.Cryptography.Api
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

            // Add Crypto Service
            services.AddCrypto();

            // Add GZip Response Compression
            services.AddGzipResponseCompression();

            // Add Context
            services.AddContexts();

            // Add Command Handler
            services.AddCommandHandler();
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