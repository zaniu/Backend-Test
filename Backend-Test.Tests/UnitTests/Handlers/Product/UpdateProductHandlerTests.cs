using BackendTest.Application.Handlers.Product;
using BackendTest.Application.Requests.Product;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class UpdateProductHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_UpdatesValues()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.TryUpdate(It.Is<Model.Product>(product =>
                product.Id == 2 &&
                product.Name == "Updated Product" &&
                product.Type == "Updated" &&
                product.Price == 44.3m), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Product product, CancellationToken _) => product);
        var handler = new UpdateProductHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new UpdateProductRequest("Updated Product", "Updated", 44.3m) with { Id = 2 }, CancellationToken.None);

        // Assert
        response.Id.Should().Be(2);
        response.Name.Should().Be("Updated Product");
        repositoryMock.Verify(repository => repository.TryUpdate(It.Is<Model.Product>(product =>
            product.Id == 2 &&
            product.Name == "Updated Product" &&
            product.Type == "Updated" &&
            product.Price == 44.3m), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.TryUpdate(It.IsAny<Model.Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Product)null);
        var handler = new UpdateProductHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new UpdateProductRequest("None", "None", 1m) with { Id = 999 }, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
        repositoryMock.Verify(repository => repository.TryUpdate(It.IsAny<Model.Product>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
