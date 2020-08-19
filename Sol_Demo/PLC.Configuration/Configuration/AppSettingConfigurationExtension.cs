using Microsoft.Extensions.Configuration;
using PLC.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Configuration.Configuration
{
    public static class AppSettingConfigurationExtension
    {
        public static bool? IsProduction(this IConfiguration configuration)
        {
            return configuration?.GetSection("ProductionModel").Get<ProductionModel>()?.IsProduction;
        }
    }
}