using BackendTest;
using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class UpdatePersonHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_UpdatesValues()
    {
        var data = new Data();
        var handler = new UpdatePersonHandler(data, new HelperUtils(data));

        var response = await handler.Handle(new UpdatePersonRequest(2, "Updated", "Person", 1988), CancellationToken.None);

        response.Id.Should().Be(2);
        response.Firstname.Should().Be("Updated");
        data.persons.Single(p => p.Id == 2).Lastname.Should().Be("Person");
    }

    [Fact]
    public async Task Handle_WithFutureYear_ThrowsException()
    {
        var data = new Data();
        var handler = new UpdatePersonHandler(data, new HelperUtils(data));

        var act = async () => await handler.Handle(
            new UpdatePersonRequest(1, "Future", "Person", DateTime.UtcNow.Year + 1),
            CancellationToken.None);

        await act.Should().ThrowAsync<DomainModelException>().WithMessage("Customer can not be born after current year");
    }
}
