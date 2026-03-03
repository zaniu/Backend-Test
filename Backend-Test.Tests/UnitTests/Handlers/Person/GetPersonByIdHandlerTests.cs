using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class GetPersonByIdHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_ReturnsPerson()
    {
        var data = new Data();
        var handler = new GetPersonByIdHandler(data, new HelperUtils(data));

        var response = await handler.Handle(new GetPersonByIdRequest(1), CancellationToken.None);

        response.Id.Should().Be(1);
        response.Firstname.Should().Be("John");
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        var data = new Data();
        var handler = new GetPersonByIdHandler(data, new HelperUtils(data));

        var act = async () => await handler.Handle(new GetPersonByIdRequest(999), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
