using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pmService.Models;

using PmServiceNetCode.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace PmServiceNetCore.Tests.IntegrationTests
{
    public class FarayandIntegrationTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public FarayandIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_CallsApiAndReturnsData()
        {
            // 1️⃣ فراخوانی endpoint واقعی
            var response = await _client.GetAsync("/api/farayand");

            // 2️⃣ بررسی کد پاسخ
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // 3️⃣ خواندن داده واقعی که API برگردانده
            var data = await response.Content.ReadFromJsonAsync<List<FarayandDto>>();

            Assert.NotNull(data);
            // اینجا optional: اگر می‌خوای چک کنی حداقل یک رکورد دارد
            // Assert.True(data.Count > 0);
        }
    }
}
