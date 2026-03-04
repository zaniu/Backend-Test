using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
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
            .Setup(repository => repository.GetById(2, It.IsAny<CancellationToken>()))
            .Returns(new Model.Person(2, "Old", "Name", 1980));
        var handler = new UpdatePersonHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new UpdatePersonRequest("Updated", "Person", 1988) with { Id = 2 }, CancellationToken.None);

        // Assert
        response.Id.Should().Be(2);
        response.Firstname.Should().Be("Updated");
        repositoryMock.Verify(repository => repository.Update(It.Is<Model.Person>(person =>
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
        repositoryMock
            .Setup(repository => repository.GetById(1, It.IsAny<CancellationToken>()))
            .Returns(new Model.Person(1, "John", "Doe", 1980));
        var handler = new UpdatePersonHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(
            new UpdatePersonRequest("Future", "Person", DateTime.UtcNow.Year + 1) with { Id = 1 },
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainModelException>().WithMessage("Customer can not be born after current year");
        repositoryMock.Verify(repository => repository.Update(It.IsAny<Model.Person>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
