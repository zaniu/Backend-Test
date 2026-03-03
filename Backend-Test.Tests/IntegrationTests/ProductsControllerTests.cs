using System.Net;
using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using BackendTest.Contracts;
using FluentAssertions;

namespace BackendTest.Test.IntegrationTests;

// NOTE: These tests assume pre-populated data in Data.cs
// Data should be initialized in the datasource before test execution
// See Data.cs for the initial set of products (IDs 1-10)

public class ProductsControllerIntegrationTests : IntegrationTestBase
{

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        using var client = CreateClient();
        var response = await GetAsync(client, "/products");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var wrapper = await ReadAsJsonAsync<Response<GetAllProductsResponse>>(response);
        wrapper.Should().NotBeNull();
        wrapper.Value.Should().NotBeNull().And.BeAssignableTo<List<GetProductByIdResponse>>();
    }

    [Theory]
    [InlineData(1, "Pipe Wrench", 0)]
    [InlineData(2, "Electric Drill", 0)]
    public async Task GetById_WithExistingId_ReturnsProductData(int productId, string expectedProductName, decimal expectedPrice)
    {
        using var client = CreateClient();
        
        // Act:
        var response = await GetAsync(client, $"/products/{productId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<Response<GetProductByIdResponse>>(response);
        var product = wrapper.Value;
        
        product.Should().NotBeNull("Product object should be deserialized");
        product!.Id.Should().Be(productId, $"Product ID should match {productId}");
        product.Name.Should().Be(expectedProductName, $"Product name should be {expectedProductName}");
        product.Price.Should().Be(expectedPrice, $"Product price should be {expectedPrice}");
    }

    [Fact]
    public async Task Add_WithValidProduct_ReturnsCreated()
    {
        using var client = CreateClient();
        var request = new AddProductRequest(99, "New Product", "Home", 19.99m);
        var response = await PostAsync(client, "/products", request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var wrapper = await ReadAsJsonAsync<Response<AddProductResponse>>(response);
        var created = wrapper.Value;
        created.Should().NotBeNull();
        created!.Id.Should().Be(99);
        created.Name.Should().Be("New Product");
        created.Type.Should().Be("Home");
        created.Price.Should().Be(19.99m);
    }

    [Fact]
    public async Task Update_WithValidProduct_ReturnsAcceptedWithProduct()
    {
        using var client = CreateClient();
        var request = new UpdateProductRequest(1, "Updated Product", "Updated Type", 29.99m);

        var response = await PutAsync(client, "/products/1", request);

        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        var wrapper = await ReadAsJsonAsync<Response<UpdateProductResponse>>(response);
        var updated = wrapper.Value;
        updated.Should().NotBeNull();
        updated!.Id.Should().Be(1);
        updated.Name.Should().Be("Updated Product");
        updated.Type.Should().Be("Updated Type");
        updated.Price.Should().Be(29.99m);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(6)]
    public async Task Delete_WithExistingId_ReturnsNoContent(int productId)
    {
        using var client = CreateClient();
        // Act:
        var response = await DeleteAsync(client, $"/products/{productId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
