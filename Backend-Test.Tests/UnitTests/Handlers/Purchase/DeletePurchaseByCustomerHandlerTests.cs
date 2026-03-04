using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class DeletePurchaseByCustomerHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingCustomerId_RemovesPurchase()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.GetByCustomerId(1, It.IsAny<CancellationToken>()))
            .Returns([new Model.Purchase(1, 1, [1, 2]), new Model.Purchase(2, 1, [3])]);
        var handler = new DeletePurchaseByCustomerHandler(repositoryMock.Object);

        // Act
        await handler.Handle(new DeletePurchaseByCustomerRequest(1), CancellationToken.None);

        // Assert
        repositoryMock.Verify(repository => repository.DeleteByCustomerId(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithMissingCustomerId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.GetByCustomerId(999, It.IsAny<CancellationToken>()))
            .Returns([]);
        var handler = new DeletePurchaseByCustomerHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new DeletePurchaseByCustomerRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
        repositoryMock.Verify(repository => repository.DeleteByCustomerId(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
