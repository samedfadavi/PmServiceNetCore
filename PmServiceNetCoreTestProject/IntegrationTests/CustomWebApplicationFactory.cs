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
            // مشخص کردن مسیر پروژه API واقعی
            builder.UseContentRoot(Path.Combine(Directory.GetCurrentDirectory(), "..", "PmServiceNetCode"));

            builder.ConfigureAppConfiguration((context, config) =>
            {
                // بارگذاری فایل appsettings.json واقعی
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            });
        }
    }
}
