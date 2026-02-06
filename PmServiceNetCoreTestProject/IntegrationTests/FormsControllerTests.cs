using Microsoft.Extensions.DependencyInjection;
using pmService.Models;
using PmServiceNetCode.DTOs;
using PmServiceNetCode.Models;
using System;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
public class FormsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    public FormsControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task GetAll_ReturnsOkAndList()
    {
        var response = await _client.GetAsync("/api/forms");
        response.EnsureSuccessStatusCode();

        var forms = await response.Content.ReadFromJsonAsync<List<FormDto>>();
        Assert.NotNull(forms);
    }

    [Fact]
    public async Task CreateAndDelete_Form_Works()
    {
        // Create
        var createDto = new CreateFormDto
        {
            Onvan = "Integration Test",
            Description = "Testing POST"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/forms", createDto);
        createResponse.EnsureSuccessStatusCode();

        var createdForm = await createResponse.Content.ReadFromJsonAsync<FormDto>();
        Assert.NotNull(createdForm);
        Assert.Equal("Integration Test", createdForm!.Onvan);

        // Delete (Soft)
        var deleteResponse = await _client.DeleteAsync($"/api/forms/{createdForm.IdForm}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify Deleted
        var getResponse = await _client.GetAsync($"/api/forms/{createdForm.IdForm}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
    [Fact]
    public async Task GetForms_ShouldReturnListOfForms()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<MaznetModel>();

            db.Forms.AddRange(
                new Form { Onvan = "Form A", Description = "Desc A" },
                new Form { Onvan = "Form B", Description = "Desc B" }
            );

            await db.SaveChangesAsync();
        }

        // Act
        var response = await _client.GetAsync("/api/forms");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content
            .ReadFromJsonAsync<List<FormDto>>();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result!.Select(x => x.Onvan)
              .Should()
              .Contain(new[] { "Form A", "Form B" });
    }
    [Fact]
    public async Task Create_Then_Delete_Form_Works()
    {
        // Create
        var createDto = new CreateFormDto
        {
            Onvan = "SQLite Test",
            Description = "Integration"
        };

        var createResponse =
            await _client.PostAsJsonAsync("/api/forms", createDto);

        createResponse.EnsureSuccessStatusCode();

        var created =
            await createResponse.Content.ReadFromJsonAsync<FormDto>();

        Assert.NotNull(created);

        // Delete (Soft)
        var deleteResponse =
            await _client.DeleteAsync($"/api/forms/{created!.IdForm}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify hidden
        var getResponse =
            await _client.GetAsync($"/api/forms/{created.IdForm}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}
