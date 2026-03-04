using BackendTest.Application.Handlers.Product;
using BackendTest.Application.Requests.Product;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class DeleteProductHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_RemovesProduct()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        var purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.Exists(3, It.IsAny<CancellationToken>()))
            .Returns(true);
        purchaseRepositoryMock
            .Setup(repository => repository.ExistsByProductId(3, It.IsAny<CancellationToken>()))
            .Returns(false);
        var handler = new DeleteProductHandler(repositoryMock.Object, purchaseRepositoryMock.Object);

        // Act
        await handler.Handle(new DeleteProductRequest(3), CancellationToken.None);

        // Assert
        repositoryMock.Verify(repository => repository.DeleteById(3, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        var purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.Exists(999, It.IsAny<CancellationToken>()))
            .Returns(false);
        var handler = new DeleteProductHandler(repositoryMock.Object, purchaseRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new DeleteProductRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
        repositoryMock.Verify(repository => repository.DeleteById(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithExistingPurchases_ThrowsException()
    {
        var repositoryMock = new Mock<IProductRepository>();
        var purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.Exists(3, It.IsAny<CancellationToken>()))
            .Returns(true);
        purchaseRepositoryMock
            .Setup(repository => repository.ExistsByProductId(3, It.IsAny<CancellationToken>()))
            .Returns(true);
        var handler = new DeleteProductHandler(repositoryMock.Object, purchaseRepositoryMock.Object);

        var act = async () => await handler.Handle(new DeleteProductRequest(3), CancellationToken.None);

        await act.Should().ThrowAsync<DomainModelException>().WithMessage("Cannot delete product with existing purchases");
        repositoryMock.Verify(repository => repository.DeleteById(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
