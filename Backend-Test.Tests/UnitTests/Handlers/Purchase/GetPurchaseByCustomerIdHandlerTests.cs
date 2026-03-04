using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class GetPurchaseByCustomerIdHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_ReturnsPurchase()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.GetByCustomerId(1, It.IsAny<CancellationToken>()))
            .Returns(new Model.Purchase(1, 1, [1, 2]));
        var handler = new GetPurchaseByCustomerIdHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new GetPurchaseByCustomerIdRequest(1), CancellationToken.None);

        // Assert
        response.CustomerId.Should().Be(1);
        response.ProductsIds.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.GetByCustomerId(999, It.IsAny<CancellationToken>()))
            .Returns((Model.Purchase)null);
        var handler = new GetPurchaseByCustomerIdHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new GetPurchaseByCustomerIdRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
