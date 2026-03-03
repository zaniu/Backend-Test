using System.Net;
using BackendTest.Application.Responses.Purchase;
using BackendTest.Contracts;
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
        using var client = CreateClient();
        var response = await GetAsync(client, "/purchases");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var wrapper = await ReadAsJsonAsync<Response<GetAllPurchasesResponse>>(response);
        wrapper.Should().NotBeNull();
        wrapper.Value.Should().NotBeNull().And.BeAssignableTo<List<GetPurchaseByCustomerIdResponse>>();
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
        var wrapper = await ReadAsJsonAsync<Response<GetPurchaseByCustomerIdResponse>>(response);
        var purchase = wrapper.Value;
        
        purchase.Should().NotBeNull("Purchase object should be deserialized");
        purchase!.CustomerId.Should().Be(customerId, $"Returned purchase should belong to customer {customerId}");
        purchase.ProductsIds.Should().NotBeNull("Purchase should have product IDs");
        purchase.ProductsIds!.Should().NotBeEmpty("Purchase should contain at least one product");
    }
}
