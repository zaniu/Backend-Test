using BackendTest.Application.Handlers.Person;
using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Person;

public class DeletePersonHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_RemovesPerson()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        var purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        purchaseRepositoryMock
            .Setup(repository => repository.ExistsByCustomerId(3, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        repositoryMock
            .Setup(repository => repository.TryDeleteById(3, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var handler = new DeletePersonHandler(repositoryMock.Object, purchaseRepositoryMock.Object);

        // Act
        await handler.Handle(new DeletePersonRequest(3), CancellationToken.None);

        // Assert
        repositoryMock.Verify(repository => repository.TryDeleteById(3, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        var purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        purchaseRepositoryMock
            .Setup(repository => repository.ExistsByCustomerId(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        repositoryMock
            .Setup(repository => repository.TryDeleteById(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        var handler = new DeletePersonHandler(repositoryMock.Object, purchaseRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new DeletePersonRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
        repositoryMock.Verify(repository => repository.TryDeleteById(999, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithExistingPurchases_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IPersonRepository>();
        var purchaseRepositoryMock = new Mock<IPurchaseRepository>();
        purchaseRepositoryMock
            .Setup(repository => repository.ExistsByCustomerId(3, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        var handler = new DeletePersonHandler(repositoryMock.Object, purchaseRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new DeletePersonRequest(3), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainModelException>().WithMessage("Cannot delete person with existing purchases");
        repositoryMock.Verify(repository => repository.TryDeleteById(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
