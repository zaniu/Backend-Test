using System.Net;
using BackendTest.Application.Responses.Person;
using BackendTest.Contracts;
using BackendTest.Application.Requests.Person;
using FluentAssertions;

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
        var response = await GetAsync(client, "/persons");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // ensure the wrapper contains a list
        var wrapper = await ReadAsJsonAsync<Response<GetAllPersonsResponse>>(response);
        wrapper.Should().NotBeNull();
        wrapper.Value.Should().NotBeNull().And.BeAssignableTo<List<GetPersonByIdResponse>>();
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
        var response = await GetAsync(client, $"/persons/{personId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<Response<GetPersonByIdResponse>>(response);
        var person = wrapper.Value;
        
        person.Should().NotBeNull("Person object should be deserialized");
        person!.Id.Should().Be(personId, $"Person ID should match {personId}");
        person.Firstname.Should().Be(expectedFirstName, $"Person first name should be {expectedFirstName}");
    }

    [Fact]
    public async Task Add_WithValidPerson_ReturnsCreatedWithPerson()
    {
        using var client = CreateClient();
        var request = new AddPersonRequest(11, "Jane", "Smith", 1985);
        var response = await PostAsync(client, "/persons", request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var wrapper = await ReadAsJsonAsync<Response<AddPersonResponse>>(response);
        var created = wrapper.Value;
        created.Should().NotBeNull();
        created!.Firstname.Should().Be("Jane");
        created.Lastname.Should().Be("Smith");
        created.Id.Should().Be(11);
        created.YearOfBirth.Should().Be(1985);
    }

    [Fact]
    public async Task Update_WithValidPerson_ReturnsAcceptedWithPerson()
    {
        // Arrange:
        // Data: Person with ID=1 should exist in persons collection
        var request = new UpdatePersonRequest(1, "UpdatedFirstName", "UpdatedLastName", 1980);
        
        // Act:
        using var client = CreateClient();
        var response = await PutAsync(client, "/persons/1", request);
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.Accepted, "Valid update should be accepted");

        var wrapper = await ReadAsJsonAsync<Response<UpdatePersonResponse>>(response);
        var updated = wrapper.Value;
        updated.Should().NotBeNull();
        updated!.Firstname.Should().Be("UpdatedFirstName");
        updated.Lastname.Should().Be("UpdatedLastName");
        updated.YearOfBirth.Should().Be(1980);
    }

    [Fact]
    public async Task Update_WithFutureYearOfBirth_ThrowsException()
    {
        // Arrange:
        // Data: Person with ID=1 should exist in persons collection
        var futureYear = DateTime.UtcNow.Year + 1;
        var request = new UpdatePersonRequest(1, "TestName", "TestLastName", futureYear);
        
        // Act:
        using var client = CreateClient();
        var response = await PutAsync(client, "/persons/1", request);
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError, "Future year of birth should cause validation error");
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public async Task Delete_WithExistingId_ReturnsNoContent(int personId)
    {
        // Arrange:
        // Data should be inserted into persons collection before test execution
        // Expected: Person with ID={personId} exists in the collection
        
        // Act:
        using var client = CreateClient();
        var response = await DeleteAsync(client, $"/persons/{personId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
