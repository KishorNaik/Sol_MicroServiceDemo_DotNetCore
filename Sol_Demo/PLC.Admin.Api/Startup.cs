using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthJwt.Services;
using JwtAuth.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PLC.Admin.Api.Configurations.Services;
using PLC.AppSetting;
using PLC.Configuration.Configuration;
using PLC.Configuration.Middlewares;
using PLC.Configuration.Services;
using PLC.EventStore.Configurations;

namespace PLC.Admin.Api
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
            services.AddControllers((config) =>
            {
                config.CacheProfiles.Add("Default-Cache", new CacheProfile()
                {
                    Duration = 30,
                    NoStore = false,
                    Location = ResponseCacheLocation.Any
                });
            });

            // Add Json Service
            services.AddJson(true);

            // Is Production Api
            bool? isProduction = Configuration.IsProduction();

            // Add Db Providers
            services.AddSqlClientDbProvider(DatabaseResource.PLC_Admin_UAT, DatabaseResource.PLC_Admin_Production, isProduction);

            // Add GZip Response Compression
            services.AddGzipResponseCompression();

            // Jwt Token
            services.AddJwtToken(AppResource.JwtSecretKey);

            // Background Task
            services.AddBackgroundTask();

            services.AddResponseCaching();

            services.AddEventStore();

            services.AddApiCommandConfig();

            services.AddMessageBrokerConfig(isProduction);

            services.AddEventConfig();

            services.AddCommandConfig();

            services.AddQueryConfig();

            services.AddRepositoryConfig();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Use Jwt Token Middleware
            app.UseJwtToken();

            app.UseAuthorization();

            // Use Response Compression
            app.UseResponseCompression();

            // Use Custom Exception Handler
            app.UseException();

            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}