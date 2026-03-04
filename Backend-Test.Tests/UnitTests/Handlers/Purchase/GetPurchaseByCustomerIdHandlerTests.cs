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
    public async Task Handle_WithExistingId_ReturnsPurchases()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.GetByCustomerId(1, It.IsAny<CancellationToken>()))
            .Returns([new Model.Purchase(1, 1, [1, 2]), new Model.Purchase(2, 1, [3])]);
        var handler = new GetPurchaseByCustomerIdHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new GetPurchaseByCustomerIdRequest(1), CancellationToken.None);

        // Assert
        response.Purchases.Should().HaveCount(2);
        response.Purchases.Should().OnlyContain(purchase => purchase.CustomerId == 1);
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.GetByCustomerId(999, It.IsAny<CancellationToken>()))
            .Returns([]);
        var handler = new GetPurchaseByCustomerIdHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new GetPurchaseByCustomerIdRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
