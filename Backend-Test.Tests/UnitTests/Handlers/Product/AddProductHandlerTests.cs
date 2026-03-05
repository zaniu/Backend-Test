using BackendTest.Application.Features.Products;
using BackendTest.Application.Features.Products.AddProduct;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class AddProductHandlerTests
{
    [Fact]
    public async Task Handle_WithNewId_AddsProduct()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.TryAdd(It.IsAny<Model.Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Product product, CancellationToken _) => product);
        var handler = new AddProductHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new AddProductRequest(999, "Unit Product", "Tools", 12.5m), CancellationToken.None);

        // Assert
        response.Id.Should().Be(999);
        repositoryMock.Verify(repository => repository.TryAdd(It.Is<Model.Product>(product =>
            product.Id == 999 &&
            product.Name == "Unit Product" &&
            product.Type == "Tools" &&
            product.Price == 12.5m), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithDuplicateId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.TryAdd(It.IsAny<Model.Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Product)null);
        var handler = new AddProductHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new AddProductRequest(1, "Dup", "Tools", 1m), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DuplicateException>().WithMessage("Item already exists");
        repositoryMock.Verify(repository => repository.TryAdd(It.IsAny<Model.Product>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
