using System.Net;
using FluentAssertions;

namespace BackendTest.Test.IntegrationTests;

//Ideally these tests should inject values into the system configuration
//instead of using values configured in the appsettings.json of tested project.

public class EnvironmentControllerIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetIsProduction_ReturnsFalseWhenDebuggerAttached()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/environment/production");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await ReadAsJsonAsync<ApiResponse<bool>>(response);
        content.Should().NotBeNull();
        content.Value.Should().BeFalse();
    }

    [Fact]
    public async Task GetApiVersion_ReturnsExpectedVersion()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/environment/apiversion");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await ReadAsJsonAsync<ApiResponse<string>>(response);
        content.Should().NotBeNull();
        content.Value.Should().Be("2.3");
    }

    [Fact]
    public async Task GetUiVersion_ReturnsExpectedVersion()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/environment/uiversion");
        
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var content = await ReadAsJsonAsync<ApiResponse<string>>(response);
        content.Should().NotBeNull();
        content.Value.Should().Be("4.7");
    }
}
