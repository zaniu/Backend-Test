using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class AddPurchaseHandlerTests
{
    [Fact]
    public async Task Handle_WithNewId_AddsPurchase()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        var personRepositoryMock = new Mock<IPersonRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.TryAdd(It.IsAny<Model.Purchase>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Purchase purchase, CancellationToken _) => purchase);
        personRepositoryMock
            .Setup(repository => repository.Exists(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        productRepositoryMock
            .Setup(repository => repository.ExistsAll(It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new[] { 1, 2 })), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var handler = new AddPurchaseHandler(repositoryMock.Object, personRepositoryMock.Object, productRepositoryMock.Object);

        // Act
        var response = await handler.Handle(new AddPurchaseRequest(999, 1, [1, 2]), CancellationToken.None);

        // Assert
        response.Id.Should().Be(999);
        repositoryMock.Verify(repository => repository.TryAdd(It.Is<Model.Purchase>(purchase =>
            purchase.Id == 999 &&
            purchase.CustomerId == 1 &&
            purchase.ProductsIds.SequenceEqual(new[] { 1, 2 })), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithDuplicateId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        var personRepositoryMock = new Mock<IPersonRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.TryAdd(It.IsAny<Model.Purchase>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Purchase)null);
        personRepositoryMock
            .Setup(repository => repository.Exists(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        productRepositoryMock
            .Setup(repository => repository.ExistsAll(It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new[] { 1 })), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var handler = new AddPurchaseHandler(repositoryMock.Object, personRepositoryMock.Object, productRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new AddPurchaseRequest(1, 1, [1]), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DuplicateException>().WithMessage("Item already exists");
        repositoryMock.Verify(repository => repository.TryAdd(It.IsAny<Model.Purchase>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithMissingCustomer_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        var personRepositoryMock = new Mock<IPersonRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        personRepositoryMock
            .Setup(repository => repository.Exists(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var handler = new AddPurchaseHandler(repositoryMock.Object, personRepositoryMock.Object, productRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new AddPurchaseRequest(1000, 999, [1]), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainModelException>().WithMessage("Customer does not exist");
        repositoryMock.Verify(repository => repository.TryAdd(It.IsAny<Model.Purchase>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithMissingProduct_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        var personRepositoryMock = new Mock<IPersonRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        personRepositoryMock
            .Setup(repository => repository.Exists(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        productRepositoryMock
            .Setup(repository => repository.ExistsAll(It.Is<IEnumerable<int>>(ids => ids.SequenceEqual(new[] { 999 })), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var handler = new AddPurchaseHandler(repositoryMock.Object, personRepositoryMock.Object, productRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new AddPurchaseRequest(1001, 1, [999]), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainModelException>().WithMessage("One or more products do not exist");
        repositoryMock.Verify(repository => repository.TryAdd(It.IsAny<Model.Purchase>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
