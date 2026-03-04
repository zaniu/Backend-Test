using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class GetAllPersonsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllCurrentPersons()
    {
        // Arrange
        var persons = new List<Model.Person>
        {
            new(1, "John", "Doe", 1980),
            new(2, "Jane", "Doe", 1985)
        };
        var repositoryMock = new Mock<IPersonRepository>();
        repositoryMock
            .Setup(repository => repository.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(persons);
        var handler = new GetAllPersonsHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new GetAllPersonsRequest(), CancellationToken.None);

        // Assert
        response.Persons.Should().HaveCount(2);
        response.Persons.Should().Contain(person => person.Id == 1 && person.Firstname == "John");
    }
}
