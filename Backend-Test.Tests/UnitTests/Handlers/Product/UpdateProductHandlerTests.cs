using BackendTest.Application.Handlers.Product;
using BackendTest.Application.Requests.Product;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class UpdateProductHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_UpdatesValues()
    {
        var data = new Data();
        var handler = new UpdateProductHandler(data, new HelperUtils(data));

        var response = await handler.Handle(new UpdateProductRequest("Updated Product", "Updated", 44.3m) with { Id = 2 }, CancellationToken.None);

        response.Id.Should().Be(2);
        response.Name.Should().Be("Updated Product");
        data.products.Single(p => p.Id == 2).Type.Should().Be("Updated");
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        var data = new Data();
        var handler = new UpdateProductHandler(data, new HelperUtils(data));

        var act = async () => await handler.Handle(new UpdateProductRequest("None", "None", 1m) with { Id = 999 }, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
