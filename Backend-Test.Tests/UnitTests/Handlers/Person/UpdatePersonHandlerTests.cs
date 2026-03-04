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
        // Arrange
        var data = new Data();
        var handler = new UpdatePersonHandler(data, new HelperUtils(data));

        // Act
        var response = await handler.Handle(new UpdatePersonRequest("Updated", "Person", 1988) with { Id = 2 }, CancellationToken.None);

        // Assert
        response.Id.Should().Be(2);
        response.Firstname.Should().Be("Updated");
        data.persons.Single(p => p.Id == 2).Lastname.Should().Be("Person");
    }

    [Fact]
    public async Task Handle_WithFutureYear_ThrowsException()
    {
        // Arrange
        var data = new Data();
        var handler = new UpdatePersonHandler(data, new HelperUtils(data));

        // Act
        var act = async () => await handler.Handle(
            new UpdatePersonRequest("Future", "Person", DateTime.UtcNow.Year + 1) with { Id = 1 },
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainModelException>().WithMessage("Customer can not be born after current year");
    }
}
