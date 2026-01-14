using Moq;
using pmService.Controllers;
using pmService.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Security;
using System.Threading.Tasks;
using Xunit;

namespace PmServiceNetCode.Tests.Controllers
{
    public class DerakhtTajhizatControllerTests
    {
        [Fact]
        public async Task List_Iradat_ReturnsOkResult_WithExpectedData()
        {
            // Arrange
            var mockDb = new Mock<MaznetModel>();
            var mockData = new Mock<ClassData>();

            // داده‌ای که ExecuteSqlAsync برمی‌گرداند
            var expectedResult = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "ID", 1 },
                    { "Code", "C1" },
                    { "Name", "Item1" }
                }
            };

            mockData
                .Setup(d => d.ExecuteSqlAsync(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                .ReturnsAsync(expectedResult);

            var controller = new Derakht_TajhizatController(mockDb.Object, mockData.Object);

            // ورودی JArray معتبر
            var items = new JArray
            {
                new JObject
                {
                    { "Name_Jadval_Pm", "Tbl_Tajhiz_A" },
                    { "Name_Pm", "Name" },
                    { "Code_Pm", "Code" },
                    { "ID", 5 },
                    { "whereirad", "1,2,3" }
                }
            };

            // Act
            var result = await controller.List_Iradat(items);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualData = Assert.IsAssignableFrom<List<Dictionary<string, object>>>(okResult.Value);
            Assert.Equal(expectedResult, actualData);

            // بررسی اینکه ExecuteSqlAsync دقیقاً یک بار فراخوانی شده
            mockData.Verify(d => d.ExecuteSqlAsync(It.IsAny<string>(), It.IsAny<SqlParameter[]>()), Times.Once);
        }

        [Fact]
        public async Task List_Iradat_ThrowsSecurityException_OnInvalidTable()
        {
            var mockDb = new Mock<MaznetModel>();
            var mockData = new Mock<ClassData>();
            var controller = new Derakht_TajhizatController(mockDb.Object, mockData.Object);

            // جدول غیرمجاز
            var items = new JArray
            {
                new JObject
                {
                    { "Name_Jadval_Pm", "InvalidTable" },
                    { "Name_Pm", "Name" },
                    { "Code_Pm", "Code" },
                    { "ID", 5 }
                }
            };

            await Assert.ThrowsAsync<SecurityException>(() => controller.List_Iradat(items));
        }

        [Fact]
        public async Task List_Iradat_ThrowsSecurityException_OnInvalidColumn()
        {
            var mockDb = new Mock<MaznetModel>();
            var mockData = new Mock<ClassData>();
            var controller = new Derakht_TajhizatController(mockDb.Object, mockData.Object);

            // ستون غیرمجاز
            var items = new JArray
            {
                new JObject
                {
                    { "Name_Jadval_Pm", "Tbl_Tajhiz_A" },
                    { "Name_Pm", "InvalidColumn" },
                    { "Code_Pm", "Code" },
                    { "ID", 5 }
                }
            };

            await Assert.ThrowsAsync<SecurityException>(() => controller.List_Iradat(items));
        }
    }
}
