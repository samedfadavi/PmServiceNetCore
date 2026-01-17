using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmServiceNetCore.Tests.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // استفاده از مسیر پیش‌فرض پروژه تست، بدون تغییر ContentRoot
            builder.ConfigureAppConfiguration((context, config) =>
            {
                // بارگذاری appsettings.json پروژه تست
                config.AddJsonFile("appsettings.json", optional: true);
            });

            return base.CreateHost(builder);
        }
    }
}
