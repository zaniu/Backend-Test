using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class DeletePurchaseHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_RemovesPurchase()
    {
        var data = new Data();
        var handler = new DeletePurchaseHandler(data, new HelperUtils(data));

        await handler.Handle(new DeletePurchaseRequest(1), CancellationToken.None);

        data.purchases.Should().NotContain(p => p.Id == 1);
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        var data = new Data();
        var handler = new DeletePurchaseHandler(data, new HelperUtils(data));

        var act = async () => await handler.Handle(new DeletePurchaseRequest(999), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
