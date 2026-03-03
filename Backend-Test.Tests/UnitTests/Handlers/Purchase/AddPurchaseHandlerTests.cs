using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class AddPurchaseHandlerTests
{
    [Fact]
    public async Task Handle_WithNewId_AddsPurchase()
    {
        var data = new Data();
        var handler = new AddPurchaseHandler(data);

        var response = await handler.Handle(new AddPurchaseRequest(999, 500, [1, 2]), CancellationToken.None);

        response.Id.Should().Be(999);
        data.purchases.Should().Contain(p => p.Id == 999 && p.CustomerId == 500);
    }

    [Fact]
    public async Task Handle_WithDuplicateId_ThrowsException()
    {
        var data = new Data();
        var handler = new AddPurchaseHandler(data);

        var act = async () => await handler.Handle(new AddPurchaseRequest(1, 1, [1]), CancellationToken.None);

        await act.Should().ThrowAsync<DuplicateException>().WithMessage("Item already exists");
    }
}
