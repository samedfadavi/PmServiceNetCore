using Moq;
using PmServiceNetCode.DTOs;
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
    public class FormServiceCreateTests
    {
        [Fact]
        public async Task CreateAsync_Creates_Form()
        {
            var mockRepo = new Mock<IFormRepository>();

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Form>()))
                .ReturnsAsync((Form f) =>
                {
                    f.IdForm = 1;
                    return f;
                });

            var service = new FormService(mockRepo.Object);

            var dto = new CreateFormDto
            {
                Onvan = "فرم جدید",
                Description = "توضیح"
            };

            var result = await service.CreateAsync(dto);

            Assert.Equal(1, result.IdForm);
            Assert.Equal("فرم جدید", result.Onvan);
        }

    }
}
