using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class DeletePurchaseByCustomerHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingCustomerId_RemovesPurchase()
    {
        var data = new Data();
        var handler = new DeletePurchaseByCustomerHandler(data);

        await handler.Handle(new DeletePurchaseByCustomerRequest(1), CancellationToken.None);

        data.purchases.Should().NotContain(p => p.CustomerId == 1 && p.Id == 1);
    }

    [Fact]
    public async Task Handle_WithMissingCustomerId_ThrowsException()
    {
        var data = new Data();
        var handler = new DeletePurchaseByCustomerHandler(data);

        var act = async () => await handler.Handle(new DeletePurchaseByCustomerRequest(999), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
