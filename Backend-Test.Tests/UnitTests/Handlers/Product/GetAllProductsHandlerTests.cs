using BackendTest;
using BackendTest.Application.Handlers.Product;
using BackendTest.Application.Requests.Product;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class GetAllProductsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllCurrentProducts()
    {
        var data = new Data();
        var handler = new GetAllProductsHandler(data);

        var response = await handler.Handle(new GetAllProductsRequest(), CancellationToken.None);

        response.Products.Should().HaveCount(data.products.Count);
    }
}
