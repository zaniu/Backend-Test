using BackendTest.Application.Features.Products;
using BackendTest.Application.Features.Products.GetAllProducts;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class GetAllProductsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllCurrentProducts()
    {
        // Arrange
        var products = new List<Model.Product>
        {
            new(1, "Pipe Wrench", "Tools", 20.00m),
            new(2, "Screwdriver", "Tools", 10.00m)
        };
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);
        var handler = new GetAllProductsHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new GetAllProductsRequest(), CancellationToken.None);

        // Assert
        response.Value.Should().HaveCount(2);
        response.Value.Should().Contain(product => product.Id == 1 && product.Name == "Pipe Wrench");
    }
}
