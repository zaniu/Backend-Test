using System.Net;
using BackendTest.Application.Requests.Person;
using FluentAssertions;

namespace BackendTest.Test.IntegrationTests;

public class PersonsControllerIntegrationTests : IntegrationTestBase
{
    // NOTE: These tests assume pre-populated data in Data.cs
    // Data should be initialized in the datasource before test execution
    // See Data.cs for the initial set of persons (IDs 1-10)


    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/persons");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // ensure the wrapper contains a list
        var wrapper = await ReadAsJsonAsync<ApiResponse<List<PersonDto>>>(response);
        wrapper.Should().NotBeNull();
        wrapper.Value.Should().NotBeNull().And.BeAssignableTo<List<PersonDto>>();
        wrapper.Value.Should().HaveCountGreaterOrEqualTo(2);
        wrapper.Value[0].Id.Should().Be(1);
        wrapper.Value[0].Firstname.Should().Be("John");
        wrapper.Value[1].Id.Should().Be(2);
        wrapper.Value[1].Firstname.Should().Be("Jane");
    }

    [Theory]
    [InlineData(1, "John")]
    [InlineData(2, "Jane")]
    public async Task GetById_WithExistingId_ReturnsPersonData(int personId, string expectedFirstName)
    {
        // Arrange
        // Data should be inserted into persons collection before test execution
        // Expected: Person with ID={personId} and firstname={expectedFirstName}
        
        // Act
        using var client = CreateClient();
        var response = await GetAsync(client, $"/persons/{personId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<PersonDto>>(response);
        var person = wrapper.Value;
        
        person.Should().NotBeNull("Person object should be deserialized");
        person!.Id.Should().Be(personId, $"Person ID should match {personId}");
        person.Firstname.Should().Be(expectedFirstName, $"Person first name should be {expectedFirstName}");
    }

    [Fact]
    public async Task Add_WithValidPerson_ReturnsCreatedWithPerson()
    {
        // Arrange
        using var client = CreateClient();
        var request = new AddPersonRequest(11, "Jane", "Smith", 1985);

        // Act
        var response = await PostAsync(client, "/persons", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var wrapper = await ReadAsJsonAsync<ApiResponse<PersonDto>>(response);
        var created = wrapper.Value;
        created.Should().NotBeNull();
        created!.Firstname.Should().Be("Jane");
        created.Lastname.Should().Be("Smith");
        created.Id.Should().Be(11);
        created.YearOfBirth.Should().Be(1985);

        var getResponse = await GetAsync(client, "/persons/11");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getWrapper = await ReadAsJsonAsync<ApiResponse<PersonDto>>(getResponse);
        getWrapper.Value.Id.Should().Be(11);
        getWrapper.Value.Firstname.Should().Be("Jane");
    }

    [Fact]
    public async Task Add_WithDuplicateId_ReturnsConflict()
    {
        // Arrange
        using var client = CreateClient();
        var request = new AddPersonRequest(1, "Jane", "Smith", 1985);

        // Act
        var response = await PostAsync(client, "/persons", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Add_WithFutureYearOfBirth_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();
        var request = new AddPersonRequest(1002, "Future", "Person", DateTime.UtcNow.Year + 1);

        // Act
        var response = await PostAsync(client, "/persons", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_WithValidPerson_ReturnsAcceptedWithPerson()
    {
        // Arrange
        // Data: Person with ID=1 should exist in persons collection
        var request = new UpdatePersonRequest("UpdatedFirstName", "UpdatedLastName", 1980);
        
        // Act
        using var client = CreateClient();
        var response = await PutAsync(client, "/persons/1", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Accepted, "Valid update should be accepted");

        var wrapper = await ReadAsJsonAsync<ApiResponse<PersonDto>>(response);
        var updated = wrapper.Value;
        updated.Should().NotBeNull();
        updated!.Firstname.Should().Be("UpdatedFirstName");
        updated.Lastname.Should().Be("UpdatedLastName");
        updated.YearOfBirth.Should().Be(1980);
    }

    [Fact]
    public async Task Update_WithFutureYearOfBirth_ReturnsBadRequest()
    {
        // Arrange
        // Data: Person with ID=1 should exist in persons collection
        var futureYear = DateTime.UtcNow.Year + 1;
        var request = new UpdatePersonRequest("TestName", "TestLastName", futureYear);
        
        // Act
        using var client = CreateClient();
        var response = await PutAsync(client, "/persons/1", request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest, "Future year of birth should cause validation error");
    }

    [Fact]
    public async Task Update_WithNonExistingPerson_ReturnsNotFound()
    {
        // Arrange
        var request = new UpdatePersonRequest("Unknown", "Person", 1980);

        // Act
        using var client = CreateClient();
        var response = await PutAsync(client, "/persons/999", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public async Task Delete_WithExistingId_ReturnsNoContent(int personId)
    {
        // Arrange
        // Data should be inserted into persons collection before test execution
        // Expected: Person with ID={personId} exists in the collection
        
        // Act
        using var client = CreateClient();
        var response = await DeleteAsync(client, $"/persons/{personId}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await GetAsync(client, $"/persons/{personId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/persons/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await DeleteAsync(client, "/persons/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
