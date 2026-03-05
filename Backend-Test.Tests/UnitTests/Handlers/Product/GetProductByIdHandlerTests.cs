using BackendTest.Application.Features.Products;
using BackendTest.Application.Features.Products.GetProductById;
using BackendTest.Exceptions;
using FluentAssertions;
using Moq;

namespace BackendTest.Test.UnitTests.Handlers.Product;

public class GetProductByIdHandlerTests
{
    [Fact]
    public async Task Handle_WithExistingId_ReturnsProduct()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.GetById(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Model.Product(1, "Pipe Wrench", "Tools", 20.00m));
        var handler = new GetProductByIdHandler(repositoryMock.Object);

        // Act
        var response = await handler.Handle(new GetProductByIdRequest(1), CancellationToken.None);

        // Assert
        response.Id.Should().Be(1);
        response.Name.Should().Be("Pipe Wrench");
    }

    [Fact]
    public async Task Handle_WithMissingId_ThrowsException()
    {
        // Arrange
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(repository => repository.GetById(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Model.Product)null);
        var handler = new GetProductByIdHandler(repositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new GetProductByIdRequest(999), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Item does not exist");
    }
}
