using System.Net;
using BackendTest.Application.Requests.Person;
using BackendTest.Application.Requests.Product;
using BackendTest.Application.Requests.Purchase;
using FluentAssertions;

namespace BackendTest.Test.IntegrationTests;

public class PersonsControllerIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetAll_ReturnsOkWithCreatedPersons()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1001, "John", "Doe", 1980));
        await PostAsync(client, "/persons", new AddPersonRequest(1002, "Jane", "Doe", 1985));

        // Act
        var response = await GetAsync(client, "/persons");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<List<PersonDto>>>(response);
        wrapper.Should().NotBeNull();
        wrapper.Value.Should().Contain(person => person.Id == 1001 && person.Firstname == "John");
        wrapper.Value.Should().Contain(person => person.Id == 1002 && person.Firstname == "Jane");
    }

    [Fact]
    public async Task GetById_WithExistingId_ReturnsPersonData()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1003, "Alice", "Smith", 1990));

        // Act
        var response = await GetAsync(client, "/persons/1003");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<PersonDto>>(response);
        var person = wrapper.Value;

        person.Should().NotBeNull();
        person.Id.Should().Be(1003);
        person.Firstname.Should().Be("Alice");
        person.Lastname.Should().Be("Smith");
    }

    [Fact]
    public async Task Add_WithValidPerson_ReturnsCreatedWithPerson()
    {
        // Arrange
        using var client = CreateClient();
        var request = new AddPersonRequest(1004, "Jane", "Smith", 1985);

        // Act
        var response = await PostAsync(client, "/persons", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var wrapper = await ReadAsJsonAsync<ApiResponse<PersonDto>>(response);
        var created = wrapper.Value;
        created.Should().NotBeNull();
        created.Id.Should().Be(1004);
        created.Firstname.Should().Be("Jane");
        created.Lastname.Should().Be("Smith");
        created.YearOfBirth.Should().Be(1985);

        var getResponse = await GetAsync(client, "/persons/1004");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Add_WithDuplicateId_ReturnsConflict()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1005, "First", "Person", 1980));

        // Act
        var response = await PostAsync(client, "/persons", new AddPersonRequest(1005, "Second", "Person", 1982));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Add_WithFutureYearOfBirth_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();
        var request = new AddPersonRequest(1006, "Future", "Person", DateTime.UtcNow.Year + 1);

        // Act
        var response = await PostAsync(client, "/persons", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_WithValidPerson_ReturnsAcceptedWithPerson()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1007, "Original", "Name", 1980));
        var request = new UpdatePersonRequest("UpdatedFirstName", "UpdatedLastName", 1981);

        // Act
        var response = await PutAsync(client, "/persons/1007", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var wrapper = await ReadAsJsonAsync<ApiResponse<PersonDto>>(response);
        var updated = wrapper.Value;
        updated.Should().NotBeNull();
        updated.Id.Should().Be(1007);
        updated.Firstname.Should().Be("UpdatedFirstName");
        updated.Lastname.Should().Be("UpdatedLastName");
        updated.YearOfBirth.Should().Be(1981);
    }

    [Fact]
    public async Task Update_WithFutureYearOfBirth_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1008, "Test", "Person", 1980));
        var request = new UpdatePersonRequest("TestName", "TestLastName", DateTime.UtcNow.Year + 1);

        // Act
        var response = await PutAsync(client, "/persons/1008", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_WithNonExistingPerson_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();
        var request = new UpdatePersonRequest("Unknown", "Person", 1980);

        // Act
        var response = await PutAsync(client, "/persons/1999", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithExistingIdWithoutPurchases_ReturnsNoContent()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1009, "Delete", "Me", 1988));

        // Act
        var response = await DeleteAsync(client, "/persons/1009");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await GetAsync(client, "/persons/1009");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithExistingIdWithPurchases_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1010, "Buyer", "One", 1988));
        await PostAsync(client, "/products", new AddProductRequest(2001, "Item", "Test", 1m));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3001, 1010, [new PurchaseProductItemRequest(2001, 1)]));

        // Act
        var response = await DeleteAsync(client, "/persons/1010");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetById_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/persons/9999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await DeleteAsync(client, "/persons/9999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
