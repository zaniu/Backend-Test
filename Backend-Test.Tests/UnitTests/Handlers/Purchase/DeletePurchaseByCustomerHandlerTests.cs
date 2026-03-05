using BackendTest.Application.Features.Purchases;
using BackendTest.Application.Features.Purchases.DeletePurchaseByCustomer;
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
            .Setup(repository => repository.TryDeleteByCustomerId(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var handler = new DeletePurchaseByCustomerHandler(repositoryMock.Object);

        // Act
        await handler.Handle(new DeletePurchaseByCustomerRequest(1), CancellationToken.None);

        // Assert
        repositoryMock.Verify(repository => repository.TryDeleteByCustomerId(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithMissingCustomerId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.TryDeleteByCustomerId(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var handler = new DeletePurchaseByCustomerHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new DeletePurchaseByCustomerRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
        repositoryMock.Verify(repository => repository.TryDeleteByCustomerId(999, It.IsAny<CancellationToken>()), Times.Once);
    }
}
