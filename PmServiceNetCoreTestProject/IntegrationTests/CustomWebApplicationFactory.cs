using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pmService.Models;
using System;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    private SqliteConnection _connection = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseContentRoot(Directory.GetCurrentDirectory());
        builder.ConfigureServices(services =>
        {
            // DbContext اصلی رو حذف می‌کنیم
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<MaznetModel>));

            if (descriptor != null)
                services.Remove(descriptor);

            // SQLite in-memory
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<MaznetModel>(options =>
            {
                options.UseSqlite(_connection);
            });

            // ساخت Schema
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MaznetModel>();
            db.Database.EnsureCreated();
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection?.Dispose();
    }
}
