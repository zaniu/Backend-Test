using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class AddPersonHandlerTests
{
    [Fact]
    public async Task Handle_WithNewId_AddsPerson()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        repositoryMock
            .Setup(repository => repository.TryAdd(It.IsAny<Model.Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Person person, CancellationToken _) => person);
        var handler = new AddPersonHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new AddPersonRequest(999, "Unit", "Tester", 1990), CancellationToken.None);

        // Assert
        response.Id.Should().Be(999);
        repositoryMock.Verify(repository => repository.TryAdd(It.Is<Model.Person>(person =>
            person.Id == 999 &&
            person.Firstname == "Unit" &&
            person.Lastname == "Tester" &&
            person.YearOfBirth == 1990), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithDuplicateId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        repositoryMock
            .Setup(repository => repository.TryAdd(It.IsAny<Model.Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Person)null);
        var handler = new AddPersonHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new AddPersonRequest(1, "John", "Dup", 1980), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DuplicateException>().WithMessage("Item already exists");
        repositoryMock.Verify(repository => repository.TryAdd(It.IsAny<Model.Person>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithFutureYear_ThrowsDomainModelException()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        var handler = new AddPersonHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(
            new AddPersonRequest(1001, "Future", "Person", DateTime.UtcNow.Year + 1),
            CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainModelException>().WithMessage("Customer can not be born after current year");
        repositoryMock.Verify(repository => repository.TryAdd(It.IsAny<Model.Person>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
