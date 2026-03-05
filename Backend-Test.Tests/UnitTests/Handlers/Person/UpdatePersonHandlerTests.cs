using BackendTest.Application.Features.Persons;
using BackendTest.Application.Features.Persons.UpdatePerson;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class UpdatePersonHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_UpdatesValues()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        repositoryMock
            .Setup(repository => repository.TryUpdate(It.Is<Model.Person>(person =>
                person.Id == 2 &&
                person.Firstname == "Updated" &&
                person.Lastname == "Person" &&
                person.YearOfBirth == 1988), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Person person, CancellationToken _) => person);
        var handler = new UpdatePersonHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new UpdatePersonRequest("Updated", "Person", 1988) with { Id = 2 }, CancellationToken.None);

        // Assert
        response.Id.Should().Be(2);
        response.Firstname.Should().Be("Updated");
        repositoryMock.Verify(repository => repository.TryUpdate(It.Is<Model.Person>(person =>
            person.Id == 2 &&
            person.Firstname == "Updated" &&
            person.Lastname == "Person" &&
            person.YearOfBirth == 1988), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithFutureYear_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        var handler = new UpdatePersonHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(
            new UpdatePersonRequest("Future", "Person", DateTime.UtcNow.Year + 1) with { Id = 1 },
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainModelException>().WithMessage("Customer can not be born after current year");
        repositoryMock.Verify(repository => repository.TryUpdate(It.IsAny<Model.Person>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
