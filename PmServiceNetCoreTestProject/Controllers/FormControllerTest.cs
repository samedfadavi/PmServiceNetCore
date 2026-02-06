using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmServiceNetCore.Tests.Controllers
{
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
    using PmServiceNetCode.DTOs;
    using Xunit;

public class FormsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public FormsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }


}

}
