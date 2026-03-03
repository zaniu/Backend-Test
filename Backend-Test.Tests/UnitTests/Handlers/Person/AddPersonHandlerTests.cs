using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using BackendTest.Exceptions;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class AddPersonHandlerTests
{
    [Fact]
    public async Task Handle_WithNewId_AddsPerson()
    {
        var data = new Data();
        var handler = new AddPersonHandler(data);
        var initialCount = data.persons.Count;

        var response = await handler.Handle(new AddPersonRequest(999, "Unit", "Tester", 1990), CancellationToken.None);

        response.Id.Should().Be(999);
        data.persons.Should().HaveCount(initialCount + 1);
        data.persons.Should().Contain(p => p.Id == 999 && p.Firstname == "Unit");
    }

    [Fact]
    public async Task Handle_WithDuplicateId_ThrowsException()
    {
        var data = new Data();
        var handler = new AddPersonHandler(data);

        var act = async () => await handler.Handle(new AddPersonRequest(1, "John", "Dup", 1980), CancellationToken.None);

        await act.Should().ThrowAsync<DuplicateException>().WithMessage("Item already exists");
    }
}
