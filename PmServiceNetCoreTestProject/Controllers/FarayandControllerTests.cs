using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using PmServiceNetCode.Controllers;
using PmServiceNetCode.DTOs;
using PmServiceNetCode.Interfaces;
using PmServiceNetCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PmServiceNetCore.Tests.Controllers
{
    public class FarayandIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public FarayandIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // فقط API را صدا می‌زنیم، هیچ کاری با DbContext در این تست نداریم
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
