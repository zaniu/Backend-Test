using BackendTest;
using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class GetAllPurchasesHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllCurrentPurchases()
    {
        var data = new Data();
        var handler = new GetAllPurchasesHandler(data);

        var response = await handler.Handle(new GetAllPurchasesRequest(), CancellationToken.None);

        response.Should().HaveCount(data.purchases.Count);
    }
}
