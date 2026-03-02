using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PersonApi.Models;
using Xunit;

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
        var response = await GetAsync(client, "/products/products/getAll/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData(1, "Pipe Wrench")]
    [InlineData(2, "Electric Drill")]
    public async Task GetById_WithExistingId_ReturnsProductData(int productId, string expectedProductName)
    {
        using var client = CreateClient();
        
        // Act:
        var response = await GetAsync(client, $"/products/products/get/{productId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var product = await ReadAsJsonAsync<ObjProduct>(response);
        
        product.Should().NotBeNull("Product object should be deserialized");
        product!.Id.Should().Be(productId, $"Product ID should match {productId}");
        product.Name.Should().Be(expectedProductName, $"Product name should be {expectedProductName}");
    }

    [Fact]
    public async Task Add_WithValidProduct_ReturnsAccepted()
    {
        using var client = CreateClient();
        var product = new ObjProduct(99, "New Product", "Home");
        var response = await PostAsync(client, "/products/products/add/", product);
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(6)]
    public async Task Delete_WithExistingId_ReturnsAccepted(int productId)
    {
        using var client = CreateClient();
        // Act:
        var response = await DeleteAsync(client, $"/products/products/delete/{productId}");
        
        // Assert:
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }
}
