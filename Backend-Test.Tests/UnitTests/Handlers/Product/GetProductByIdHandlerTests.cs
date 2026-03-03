using BackendTest.Application.Handlers.Product;
using BackendTest.Application.Requests.Product;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class GetProductByIdHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_ReturnsProduct()
    {
        var data = new Data();
        var handler = new GetProductByIdHandler(data, new HelperUtils(data));

        var response = await handler.Handle(new GetProductByIdRequest(1), CancellationToken.None);

        response.Id.Should().Be(1);
        response.Name.Should().Be("Pipe Wrench");
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        var data = new Data();
        var handler = new GetProductByIdHandler(data, new HelperUtils(data));

        var act = async () => await handler.Handle(new GetProductByIdRequest(999), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
