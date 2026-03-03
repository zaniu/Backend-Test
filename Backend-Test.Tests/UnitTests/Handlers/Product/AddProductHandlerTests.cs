using BackendTest.Application.Handlers.Product;
using BackendTest.Application.Requests.Product;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class AddProductHandlerTests
{
    [Fact]
    public async Task Handle_WithNewId_AddsProduct()
    {
        var data = new Data();
        var handler = new AddProductHandler(data);

        var response = await handler.Handle(new AddProductRequest(999, "Unit Product", "Tools", 12.5m), CancellationToken.None);

        response.Id.Should().Be(999);
        data.products.Should().Contain(p => p.Id == 999 && p.Name == "Unit Product");
    }

    [Fact]
    public async Task Handle_WithDuplicateId_ThrowsException()
    {
        var data = new Data();
        var handler = new AddProductHandler(data);

        var act = async () => await handler.Handle(new AddProductRequest(1, "Dup", "Tools", 1m), CancellationToken.None);

        await act.Should().ThrowAsync<DuplicateException>().WithMessage("Item already exists");
    }
}
