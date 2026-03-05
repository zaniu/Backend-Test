using BackendTest.Application.Features.Persons;
using BackendTest.Application.Features.Persons.GetPersonById;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class GetPersonByIdHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_ReturnsPerson()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        repositoryMock
            .Setup(repository => repository.GetById(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Model.Person(1, "John", "Doe", 1980));
        var handler = new GetPersonByIdHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new GetPersonByIdRequest(1), CancellationToken.None);

        // Assert
        response.Id.Should().Be(1);
        response.Firstname.Should().Be("John");
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        repositoryMock
            .Setup(repository => repository.GetById(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Person)null);
        var handler = new GetPersonByIdHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new GetPersonByIdRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
