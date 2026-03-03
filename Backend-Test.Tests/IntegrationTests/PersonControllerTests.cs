using System.Net;
using FluentAssertions;
using PersonApi.Models;

namespace BackendTest.Test.IntegrationTests;

public class PersonControllerIntegrationTests : IntegrationTestBase
{
    // NOTE: These tests assume pre-populated data in Data.cs
    // Data should be initialized in the datasource before test execution
    // See Data.cs for the initial set of persons (IDs 1-10)


    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        using var client = CreateClient();
        var response = await GetAsync(client, "/persons/persons");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(1, "John")]
    [InlineData(2, "Jane")]
    public async Task GetById_WithExistingId_ReturnsPersonData(int personId, string expectedFirstName)
    {
        // Arrange:
        // Data should be inserted into persons collection before test execution
        // Expected: Person with ID={personId} and firstname={expectedFirstName}
        
        // Act:
        using var client = CreateClient();
        var response = await GetAsync(client, $"/persons/persons/{personId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var person = await ReadAsJsonAsync<Person>(response);
        
        person.Should().NotBeNull("Person object should be deserialized");
        person!.Id.Should().Be(personId, $"Person ID should match {personId}");
        person.Firstname.Should().Be(expectedFirstName, $"Person first name should be {expectedFirstName}");
    }

    [Fact]
    public async Task Add_WithValidPerson_ReturnsAccepted()
    {
        using var client = CreateClient();
        var person = new Person(1, "Jane", "Smith", 1985);
        var response = await PostAsync(client, "/persons/persons", person);
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }

    [Fact]
    public async Task Update_WithValidPerson_ReturnsAccepted()
    {
        // Arrange:
        // Data: Person with ID=1 should exist in persons collection
        var person = new Person(1, "UpdatedFirstName", "UpdatedLastName", 1980);
        
        // Act:
        using var client = CreateClient();
        var response = await PutAsync(client, "/persons/persons/1", person);
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.Accepted, "Valid update should be accepted");
    }

    [Fact]
    public async Task Update_WithFutureYearOfBirth_ThrowsException()
    {
        // Arrange:
        // Data: Person with ID=1 should exist in persons collection
        var futureYear = DateTime.UtcNow.Year + 1;
        var person = new Person(1, "TestName", "TestLastName", futureYear);
        
        // Act:
        using var client = CreateClient();
        var response = await PutAsync(client, "/persons/persons/1", person);
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError, "Future year of birth should cause validation error");
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public async Task Delete_WithExistingId_ReturnsAccepted(int personId)
    {
        // Arrange:
        // Data should be inserted into persons collection before test execution
        // Expected: Person with ID={personId} exists in the collection
        
        // Act:
        using var client = CreateClient();
        var response = await DeleteAsync(client, $"/persons/persons/{personId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }
}
