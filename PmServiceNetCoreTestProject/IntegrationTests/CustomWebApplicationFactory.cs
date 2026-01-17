using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmServiceNetCore.Tests.IntegrationTests
{
    public class CustomWebApplicationFactory
        : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                // اول remove تمام config های قبلی
                config.Sources.Clear();

                // اضافه کردن environment-specific یا environment variable
                config.AddEnvironmentVariables();

                // یا اگر میخوای فایل appsettings مخصوص تست داشته باشی:
                config.AddJsonFile("appsettings.Test.json", optional: true);
            });
        }
    }
}
