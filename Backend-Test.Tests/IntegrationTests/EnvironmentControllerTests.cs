using System.Net;
using BackendTest.Contracts;
using FluentAssertions;

namespace BackendTest.Test.IntegrationTests;

//Ideally these tests should inject values into the system configuration
//instead of using values configured in the appsettings.json of tested project.

public class EnvironmentControllerIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetIsProduction_ReturnsFalseWhenDebuggerAttached()
    {
        using var client = CreateClient();
        var response = await GetAsync(client, "/environment/production");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await ReadAsJsonAsync<Response<bool>>(response);
        content.Should().NotBeNull();
        content.Value.Should().BeFalse();
    }

    [Fact]
    public async Task GetApiVersion_ReturnsExpectedVersion()
    {
        using var client = CreateClient();
        var response = await GetAsync(client, "/environment/apiversion");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await ReadAsJsonAsync<Response<string>>(response);
        content.Should().NotBeNull();
        content.Value.Should().Be("2.3");
    }

    [Fact]
    public async Task GetUiVersion_ReturnsExpectedVersion()
    {
        using var client = CreateClient();
        var response = await GetAsync(client, "/environment/uiversion");
        
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var content = await ReadAsJsonAsync<Response<string>>(response);
        content.Should().NotBeNull();
        content.Value.Should().Be("4.7");
    }
}
