using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.AspDotNetCore.Services
{
    public static class DataProtectionServiceExtension
    {
        public static void AddCrypto(this IServiceCollection services)
        {
            try
            {
                services.
                    AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"bin\debug\configuration"))
                .ProtectKeysWithDpapi()
                .SetDefaultKeyLifetime(TimeSpan.FromDays(10));
            }
            catch
            {
                throw;
            }
        }
    }
}
