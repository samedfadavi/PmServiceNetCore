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
    public class FarayandControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOk_WithData()
        {
            var mockRepo = new Mock<IFarayandRepository>();
            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<TblFarayand>
        {
            new TblFarayand { ID = 1, Onvan = "Test" }
        });

            var controller = new FarayandController(mockRepo.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Correct type assertion
            var data = Assert.IsType<List<FarayandDto>>(okResult.Value);

            Assert.Single(data);
            Assert.Equal(1, data[0].ID);
            Assert.Equal("Test", data[0].Onvan);
        }
    }
}
