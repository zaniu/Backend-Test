using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class DeletePersonHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_RemovesPerson()
    {
        var data = new Data();
        var handler = new DeletePersonHandler(data, new HelperUtils(data));

        await handler.Handle(new DeletePersonRequest(3), CancellationToken.None);

        data.persons.Should().NotContain(p => p.Id == 3);
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        var data = new Data();
        var handler = new DeletePersonHandler(data, new HelperUtils(data));

        var act = async () => await handler.Handle(new DeletePersonRequest(999), CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
