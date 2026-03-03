using BackendTest;
using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class GetAllPersonsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllCurrentPersons()
    {
        var data = new Data();
        var handler = new GetAllPersonsHandler(data);

        var response = await handler.Handle(new GetAllPersonsRequest(), CancellationToken.None);

        response.Should().HaveCount(data.persons.Count);
    }
}
