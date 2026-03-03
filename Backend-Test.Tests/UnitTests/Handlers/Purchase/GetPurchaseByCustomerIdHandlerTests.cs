using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Purchase;

public class GetPurchaseByCustomerIdHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_ReturnsPurchase()
    {
        var data = new Data();
        var handler = new GetPurchaseByCustomerIdHandler(data);

        var response = await handler.Handle(new GetPurchaseByCustomerIdRequest(1), CancellationToken.None);

        response.CustomerId.Should().Be(1);
        response.ProductsIds.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        var data = new Data();
        var handler = new GetPurchaseByCustomerIdHandler(data);

        var act = async () => await handler.Handle(new GetPurchaseByCustomerIdRequest(999), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
