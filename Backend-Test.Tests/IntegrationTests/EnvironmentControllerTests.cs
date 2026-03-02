using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BackendTest.Test.IntegrationTests;

public class EnvironmentControllerIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetIsProduction_ReturnsFalseWhenDebuggerAttached()
    {
        using var client = CreateClient();
        var response = await GetAsync(client, "/environment/isproduction");
        
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var isProduction = await ReadAsJsonAsync<bool>(response);
        isProduction.Should().BeTrue();
    }

    [Fact]
    public async Task GetApiVersion_ReturnsExpectedVersion()
    {
        using var client = CreateClient();
        var response = await GetAsync(client, "/environment/apiversion");
        
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var content = await ReadAsStringAsync(response);
        content.Should()
            .NotBeNullOrEmpty("API version should be returned")
            .And.Contain("2.3", "API version should contain version number 2.3");
    }

    [Fact]
    public async Task GetUiVersion_ReturnsExpectedVersion()
    {
        using var client = CreateClient();
        var response = await GetAsync(client, "/environment/uiversion");
        
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var content = await ReadAsStringAsync(response);
        content.Should()
            .NotBeNullOrEmpty("UI version should be returned")
            .And.Contain("4.7", "UI version should contain version number 4.7");
    }
}
