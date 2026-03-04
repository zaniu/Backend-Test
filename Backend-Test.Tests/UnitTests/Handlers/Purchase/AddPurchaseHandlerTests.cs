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
        repositoryMock
            .Setup(repository => repository.Exists(999, It.IsAny<CancellationToken>()))
            .Returns(false);
        var handler = new AddPurchaseHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new AddPurchaseRequest(999, 500, [1, 2]), CancellationToken.None);

        // Assert
        response.Id.Should().Be(999);
        repositoryMock.Verify(repository => repository.Add(It.Is<Model.Purchase>(purchase =>
            purchase.Id == 999 &&
            purchase.CustomerId == 500 &&
            purchase.ProductsIds.SequenceEqual(new[] { 1, 2 })), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithDuplicateId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.Exists(1, It.IsAny<CancellationToken>()))
            .Returns(true);
        var handler = new AddPurchaseHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new AddPurchaseRequest(1, 1, [1]), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DuplicateException>().WithMessage("Item already exists");
        repositoryMock.Verify(repository => repository.Add(It.IsAny<Model.Purchase>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
