using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class GetAllPurchasesHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllCurrentPurchases()
    {
        // Arrange
        var purchases = new List<Model.Purchase>
        {
            new(1, 1, [1, 2]),
            new(2, 2, [3])
        };
        var repositoryMock = new Mock<IPurchaseRepository>();
        repositoryMock
            .Setup(repository => repository.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(purchases);
        var handler = new GetAllPurchasesHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new GetAllPurchasesRequest(), CancellationToken.None);

        // Assert
        response.Purchases.Should().HaveCount(2);
        response.Purchases.Should().Contain(purchase => purchase.Id == 1 && purchase.CustomerId == 1);
    }
}
