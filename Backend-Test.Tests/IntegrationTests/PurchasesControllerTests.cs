using System.Net;
using BackendTest.Application.Requests.Purchase;
using FluentAssertions;

namespace BackendTest.Test.IntegrationTests;

public class PurchasesControllerIntegrationTests : IntegrationTestBase
{
    // NOTE: These tests assume pre-populated data in Data.cs
    // Data should be initialized in the datasource before test execution
    // See Data.cs for the initial set of purchases with customer IDs and product IDs

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/purchases");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var wrapper = await ReadAsJsonAsync<ApiResponse<List<PurchaseDto>>>(response);
        wrapper.Should().NotBeNull();
        wrapper.Value.Should().NotBeNull().And.BeAssignableTo<List<PurchaseDto>>();
        wrapper.Value.Should().HaveCountGreaterOrEqualTo(2);
        wrapper.Value[0].Id.Should().Be(1);
        wrapper.Value[0].CustomerId.Should().Be(1);
        wrapper.Value[1].Id.Should().Be(2);
        wrapper.Value[1].CustomerId.Should().Be(1);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task GetByCustomerId_WithExistingCustomerId_ReturnsPurchaseData(int customerId)
    {
        // Arrange:
        // Data should be inserted into purchases collection before test execution
        // Expected: Purchase records with CustomerId={customerId} exist in the collection
        
        // Act:
        using var client = CreateClient();
        var response = await GetAsync(client, $"/purchases/customer/{customerId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.OK, $"Should return purchase data for existing customer {customerId}");
        var wrapper = await ReadAsJsonAsync<ApiResponse<PurchaseDto>>(response);
        var purchase = wrapper.Value;
        
        purchase.Should().NotBeNull("Purchase object should be deserialized");
        purchase!.CustomerId.Should().Be(customerId, $"Returned purchase should belong to customer {customerId}");
        purchase.ProductsIds.Should().NotBeNull("Purchase should have product IDs");
        purchase.ProductsIds!.Should().NotBeEmpty("Purchase should contain at least one product");
    }

    [Fact]
    public async Task GetByCustomerId_WithNonExistingCustomerId_ReturnsInternalServerError()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/purchases/customer/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task Add_WithValidPurchase_ReturnsCreated()
    {
        // Arrange
        using var client = CreateClient();
        var request = new AddPurchaseRequest(100, 999, [1, 2]);

        // Act
        var response = await PostAsync(client, "/purchases", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var wrapper = await ReadAsJsonAsync<ApiResponse<PurchaseDto>>(response);
        var created = wrapper.Value;
        created.Should().NotBeNull();
        created!.Id.Should().Be(100);
        created.CustomerId.Should().Be(999);
        created.ProductsIds.Should().Contain([1, 2]);

        var getResponse = await GetAsync(client, "/purchases/customer/999");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getWrapper = await ReadAsJsonAsync<ApiResponse<PurchaseDto>>(getResponse);
        getWrapper.Value.Id.Should().Be(100);
        getWrapper.Value.CustomerId.Should().Be(999);
    }

    [Fact]
    public async Task Add_WithDuplicateId_ReturnsInternalServerError()
    {
        // Arrange
        using var client = CreateClient();
        var request = new AddPurchaseRequest(1, 1, [1]);

        // Act
        var response = await PostAsync(client, "/purchases", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task Delete_WithExistingId_ReturnsNoContent()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await DeleteAsync(client, "/purchases/1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsInternalServerError()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await DeleteAsync(client, "/purchases/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task DeleteFromCustomer_WithExistingCustomerId_ReturnsNoContent()
    {
        // Arrange
        using var client = CreateClient();
        var addResponse = await PostAsync(client, "/purchases", new AddPurchaseRequest(101, 998, [1]));
        addResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        // Act
        var response = await DeleteAsync(client, "/purchases/customer/998");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await GetAsync(client, "/purchases/customer/998");
        getResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task DeleteFromCustomer_WithNonExistingCustomerId_ReturnsInternalServerError()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await DeleteAsync(client, "/purchases/customer/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task GetPurchaseReportById_ReturnsInternalServerError()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/purchases/1/report");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}
