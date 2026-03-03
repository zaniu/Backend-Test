using BackendTest;
using BackendTest.Application.Handlers.Product;
using BackendTest.Application.Requests.Product;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class DeleteProductHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_RemovesProduct()
    {
        var data = new Data();
        var handler = new DeleteProductHandler(data, new HelperUtils(data), new CommonExceptions());

        await handler.Handle(new DeleteProductRequest(3), CancellationToken.None);

        data.products.Should().NotContain(p => p.Id == 3);
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        var data = new Data();
        var handler = new DeleteProductHandler(data, new HelperUtils(data), new CommonExceptions());

        var act = async () => await handler.Handle(new DeleteProductRequest(999), CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Item does not exist");
    }
}
