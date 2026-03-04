using System.Net;
using BackendTest.Application.Requests.Person;
using BackendTest.Application.Requests.Product;
using BackendTest.Application.Requests.Purchase;
using FluentAssertions;

namespace BackendTest.Test.IntegrationTests;

public class PurchasesControllerIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task GetAll_ReturnsOkWithCreatedPurchases()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1012, "John", "Doe", 1980));
        await PostAsync(client, "/products", new AddProductRequest(2009, "P1", "Type", 1m));
        await PostAsync(client, "/products", new AddProductRequest(2010, "P2", "Type", 2m));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3003, 1012, [new PurchaseProductItemRequest(2009, 1)]));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3004, 1012, [new PurchaseProductItemRequest(2010, 1)]));

        // Act
        var response = await GetAsync(client, "/purchases");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<List<PurchaseDto>>>(response);
        wrapper.Should().NotBeNull();
        wrapper.Value.Should().Contain(p => p.Id == 3003 && p.CustomerId == 1012);
        wrapper.Value.Should().Contain(p => p.Id == 3004 && p.CustomerId == 1012);
    }

    [Fact]
    public async Task GetByCustomerId_WithExistingCustomerId_ReturnsPurchaseData()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1013, "Jane", "Doe", 1985));
        await PostAsync(client, "/products", new AddProductRequest(2011, "P3", "Type", 3m));
        await PostAsync(client, "/products", new AddProductRequest(2012, "P4", "Type", 4m));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3005, 1013, [new PurchaseProductItemRequest(2011, 1)]));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3006, 1013, [new PurchaseProductItemRequest(2012, 1)]));

        // Act
        var response = await GetAsync(client, "/purchases/customer/1013");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<PurchasesByCustomerDto>>(response);
        wrapper.Value.Should().NotBeNull();
        wrapper.Value.Purchases.Should().HaveCount(2);
        wrapper.Value.Purchases.Should().OnlyContain(purchase => purchase.CustomerId == 1013);
        wrapper.Value.Purchases.Should().OnlyContain(purchase => purchase.Items.Count > 0);
    }

    [Fact]
    public async Task GetByCustomerId_WithNonExistingCustomerId_ReturnsEmptyPurchases()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await GetAsync(client, "/purchases/customer/9999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var wrapper = await ReadAsJsonAsync<ApiResponse<PurchasesByCustomerDto>>(response);
        wrapper.Value.Purchases.Should().BeEmpty();
    }

    [Fact]
    public async Task Add_WithValidPurchase_ReturnsCreated()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1014, "Buyer", "One", 1990));
        await PostAsync(client, "/products", new AddProductRequest(2013, "A", "Type", 1m));
        await PostAsync(client, "/products", new AddProductRequest(2014, "B", "Type", 2m));
        var request = new AddPurchaseRequest(3007, 1014,
        [
            new PurchaseProductItemRequest(2013, 1),
            new PurchaseProductItemRequest(2014, 1)
        ]);

        // Act
        var response = await PostAsync(client, "/purchases", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var wrapper = await ReadAsJsonAsync<ApiResponse<PurchaseDto>>(response);
        var created = wrapper.Value;
        created.Should().NotBeNull();
        created.Id.Should().Be(3007);
        created.CustomerId.Should().Be(1014);
        created.Items.Should().Contain(item => item.ProductId == 2013 && item.Count == 1);
        created.Items.Should().Contain(item => item.ProductId == 2014 && item.Count == 1);

        var getResponse = await GetAsync(client, "/purchases/customer/1014");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getWrapper = await ReadAsJsonAsync<ApiResponse<PurchasesByCustomerDto>>(getResponse);
        getWrapper.Value.Purchases.Should().Contain(purchase => purchase.Id == 3007 && purchase.CustomerId == 1014);
    }

    [Fact]
    public async Task Add_WithNonExistingCustomer_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/products", new AddProductRequest(2015, "X", "Type", 1m));
        var request = new AddPurchaseRequest(3008, 9999, [new PurchaseProductItemRequest(2015, 1)]);

        // Act
        var response = await PostAsync(client, "/purchases", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Add_WithNonExistingProduct_ReturnsBadRequest()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1015, "Buyer", "Two", 1991));
        var request = new AddPurchaseRequest(3009, 1015, [new PurchaseProductItemRequest(9999, 1)]);

        // Act
        var response = await PostAsync(client, "/purchases", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Add_WithDuplicateId_ReturnsConflict()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1016, "Buyer", "Three", 1992));
        await PostAsync(client, "/products", new AddProductRequest(2016, "Y", "Type", 1m));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3010, 1016, [new PurchaseProductItemRequest(2016, 1)]));

        // Act
        var response = await PostAsync(client, "/purchases", new AddPurchaseRequest(3010, 1016, [new PurchaseProductItemRequest(2016, 1)]));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Add_WithNonPositiveId_ReturnsCreated()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1020, "Buyer", "Validation", 1992));
        await PostAsync(client, "/products", new AddProductRequest(2020, "Valid Product", "Type", 1m));

        // Act
        var response = await PostAsync(client, "/purchases", new AddPurchaseRequest(0, 1020, [new PurchaseProductItemRequest(2020, 1)]));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Add_WithEmptyProducts_ReturnsBadRequestFromValidationPipeline()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1021, "Buyer", "Validation", 1992));

        // Act
        var response = await PostAsync(client, "/purchases", new AddPurchaseRequest(3021, 1021, []));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var wrapper = await ReadAsJsonAsync<ApiResponse<object>>(response);
        wrapper.Errors.Should().Contain(error => error.Contains("At least one product is required"));
    }

    [Fact]
    public async Task Delete_WithExistingId_ReturnsNoContent()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1017, "Buyer", "Four", 1993));
        await PostAsync(client, "/products", new AddProductRequest(2017, "Z", "Type", 1m));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3011, 1017, [new PurchaseProductItemRequest(2017, 1)]));

        // Act
        var response = await DeleteAsync(client, "/purchases/3011");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await DeleteAsync(client, "/purchases/9999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteFromCustomer_WithExistingCustomerId_ReturnsNoContent()
    {
        // Arrange
        using var client = CreateClient();
        await PostAsync(client, "/persons", new AddPersonRequest(1018, "Buyer", "Five", 1994));
        await PostAsync(client, "/products", new AddProductRequest(2018, "P", "Type", 1m));
        await PostAsync(client, "/products", new AddProductRequest(2019, "Q", "Type", 2m));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3012, 1018, [new PurchaseProductItemRequest(2018, 1)]));
        await PostAsync(client, "/purchases", new AddPurchaseRequest(3013, 1018, [new PurchaseProductItemRequest(2019, 1)]));

        // Act
        var response = await DeleteAsync(client, "/purchases/customer/1018");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await GetAsync(client, "/purchases/customer/1018");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var getWrapper = await ReadAsJsonAsync<ApiResponse<PurchasesByCustomerDto>>(getResponse);
        getWrapper.Value.Purchases.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteFromCustomer_WithNonExistingCustomerId_ReturnsNotFound()
    {
        // Arrange
        using var client = CreateClient();

        // Act
        var response = await DeleteAsync(client, "/purchases/customer/9999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
