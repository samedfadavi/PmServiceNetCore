using Moq;
using PmServiceNetCode.Interfaces;
using PmServiceNetCode.Models;
using PmServiceNetCode.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmServiceNetCore.Tests.Controllers
{
    public class FormServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsForms()
        {
            // Arrange
            var mockRepo = new Mock<IFormRepository>();
            mockRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Form>
                {
                new Form { IdForm = 1, Onvan = "فرم تست", Description = "توضیح" }
                });

            var service = new FormService(mockRepo.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("فرم تست", result[0].Onvan);
        }
        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenFormExists()
        {
            var mockRepo = new Mock<IFormRepository>();

            mockRepo.Setup(r => r.SoftDeleteAsync(1))
                .ReturnsAsync(true);

            var service = new FormService(mockRepo.Object);

            var result = await service.DeleteAsync(1);

            Assert.True(result);
        }

    }

}
