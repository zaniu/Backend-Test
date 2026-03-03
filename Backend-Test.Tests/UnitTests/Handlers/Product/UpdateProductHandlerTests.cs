using BackendTest;
using BackendTest.Application.Handlers.Product;
using BackendTest.Application.Requests.Product;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class UpdateProductHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_UpdatesValues()
    {
        var data = new Data();
        var handler = new UpdateProductHandler(data, new CommonExceptions(), new HelperUtils(data));

        var response = await handler.Handle(new UpdateProductRequest(2, "Updated Product", "Updated", 44.3m), CancellationToken.None);

        response.Id.Should().Be(2);
        response.Name.Should().Be("Updated Product");
        data.products.Single(p => p.Id == 2).Type.Should().Be("Updated");
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        var data = new Data();
        var handler = new UpdateProductHandler(data, new CommonExceptions(), new HelperUtils(data));

        var act = async () => await handler.Handle(new UpdateProductRequest(999, "None", "None", 1m), CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Item does not exist");
    }
}
