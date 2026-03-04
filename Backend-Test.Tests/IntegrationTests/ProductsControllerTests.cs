using System.Net;
using BackendTest.Application.Requests.Product;
using BackendTest.Application.Requests.Person;
using BackendTest.Application.Requests.Purchase;
using FluentAssertions;

namespace BackendTest.Test.IntegrationTests;

public class ProductsControllerIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetAll_ReturnsOkWithCreatedProducts()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/products", new AddProductRequest(2001, "Pipe Wrench", "Plumbing", 0m));
        await PostAsync(client, "/products", new AddProductRequest(2002, "Electric Drill", "Electric", 0m));

        // Act
        var response = await GetAsync(client, "/products");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<List<ProductDto>>>(response);
        wrapper.Should().NotBeNull();
        wrapper.Value.Should().Contain(product => product.Id == 2001 && product.Name == "Pipe Wrench");
        wrapper.Value.Should().Contain(product => product.Id == 2002 && product.Name == "Electric Drill");
    }

    [Fact]
    public async Task GetById_WithExistingId_ReturnsProductData()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/products", new AddProductRequest(2003, "Garden Hose", "Gardening", 12.5m));

        // Act
        var response = await GetAsync(client, "/products/2003");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<ProductDto>>(response);
        var product = wrapper.Value;

        product.Should().NotBeNull();
        product.Id.Should().Be(2003);
        product.Name.Should().Be("Garden Hose");
        product.Price.Should().Be(12.5m);
    }

    [Fact]
    public async Task Add_WithValidProduct_ReturnsCreated()
    {
        // Arrange
        using var client = CreateClient();
        var request = new AddProductRequest(2004, "New Product", "Home", 19.99m);

        // Act
        var response = await PostAsync(client, "/products", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var wrapper = await ReadAsJsonAsync<ApiResponse<ProductDto>>(response);
        var created = wrapper.Value;
        created.Should().NotBeNull();
        created.Id.Should().Be(2004);
        created.Name.Should().Be("New Product");
        created.Type.Should().Be("Home");
        created.Price.Should().Be(19.99m);
    }

    [Fact]
    public async Task Add_WithDuplicateId_ReturnsConflict()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/products", new AddProductRequest(2005, "Original", "Home", 10m));

        // Act
        var response = await PostAsync(client, "/products", new AddProductRequest(2005, "Duplicate", "Home", 12m));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Update_WithValidProduct_ReturnsAcceptedWithProduct()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/products", new AddProductRequest(2006, "Old Product", "Old Type", 20m));
        var request = new UpdateProductRequest("Updated Product", "Updated Type", 29.99m);

        // Act
        var response = await PutAsync(client, "/products/2006", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<ProductDto>>(response);
        var updated = wrapper.Value;
        updated.Should().NotBeNull();
        updated.Id.Should().Be(2006);
        updated.Name.Should().Be("Updated Product");
        updated.Type.Should().Be("Updated Type");
        updated.Price.Should().Be(29.99m);
    }

    [Fact]
    public async Task Update_WithNonExistingProduct_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();
        var request = new UpdateProductRequest("Updated Product", "Updated Type", 29.99m);

        // Act
        var response = await PutAsync(client, "/products/2999", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithExistingIdWithoutPurchases_ReturnsNoContent()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/products", new AddProductRequest(2007, "Temporary", "Test", 1.99m));

        // Act
        var response = await DeleteAsync(client, "/products/2007");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await GetAsync(client, "/products/2007");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithExistingIdWithPurchases_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1011, "Buyer", "One", 1980));
        await PostAsync(client, "/products", new AddProductRequest(2008, "Linked Product", "Test", 5m));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3002, 1011, [new PurchaseProductItemRequest(2008, 1)]));

        // Act
        var response = await DeleteAsync(client, "/products/2008");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetById_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/products/9999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await DeleteAsync(client, "/products/9999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
