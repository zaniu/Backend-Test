using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class DeletePurchaseHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_RemovesPurchase()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.TryDeleteById(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var handler = new DeletePurchaseHandler(repositoryMock.Object);

        // Act
        await handler.Handle(new DeletePurchaseRequest(1), CancellationToken.None);

        // Assert
        repositoryMock.Verify(repository => repository.TryDeleteById(1, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.TryDeleteById(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var handler = new DeletePurchaseHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new DeletePurchaseRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
        repositoryMock.Verify(repository => repository.TryDeleteById(999, It.IsAny<CancellationToken>()), Times.Once);
    }
}
