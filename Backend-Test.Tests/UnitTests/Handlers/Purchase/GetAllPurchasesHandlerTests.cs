using BackendTest.Application.Features.Purchases;
using BackendTest.Application.Features.Purchases.GetAllPurchases;
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
            new Model.Purchase(1, 1, new List<Model.PurchaseProductItem>
            {
                new Model.PurchaseProductItem(1, 1),
                new Model.PurchaseProductItem(2, 1)
            }),
            new Model.Purchase(2, 2, new List<Model.PurchaseProductItem>
            {
                new Model.PurchaseProductItem(3, 1)
            })
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
