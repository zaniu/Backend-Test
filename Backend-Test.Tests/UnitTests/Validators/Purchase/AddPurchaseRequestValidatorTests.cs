using BackendTest.Application.Features.Purchases.AddPurchase;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Validators.Purchase;

public class AddPurchaseRequestValidatorTests
{
    private readonly AddPurchaseRequestValidator _validator = new();

    [Fact]
    public void Validate_WithValidRequest_ShouldNotHaveErrors()
    {
        // Arrange
        var request = new AddPurchaseRequest(1, 10,
        [
            new AddPurchaseRequest.PurchaseItem(1, 1),
            new AddPurchaseRequest.PurchaseItem(2, 1)
        ]);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_WithZeroId_ShouldNotHaveError()
    {
        // Arrange
        var request = new AddPurchaseRequest(0, 10, [new AddPurchaseRequest.PurchaseItem(1, 1)]);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().NotContain(error => error.PropertyName == nameof(AddPurchaseRequest.Id));
    }

    [Fact]
    public void Validate_WithZeroCustomerId_ShouldNotHaveError()
    {
        // Arrange
        var request = new AddPurchaseRequest(1, 0, [new AddPurchaseRequest.PurchaseItem(1, 1)]);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().NotContain(error => error.PropertyName == nameof(AddPurchaseRequest.CustomerId));
    }

    [Fact]
    public void Validate_WithEmptyProducts_ShouldHaveExpectedErrorMessage()
    {
        // Arrange
        var request = new AddPurchaseRequest(1, 10, []);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error =>
            error.PropertyName == nameof(AddPurchaseRequest.Items)
            && error.ErrorMessage == "At least one product is required");
    }

    [Fact]
    public void Validate_WithNonPositiveProductIds_ShouldNotHaveError()
    {
        // Arrange
        var request = new AddPurchaseRequest(1, 10,
        [
            new AddPurchaseRequest.PurchaseItem(1, 1),
            new AddPurchaseRequest.PurchaseItem(0, 1)
        ]);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().NotContain(error => error.PropertyName.Contains(nameof(AddPurchaseRequest.PurchaseItem.ProductId)));
    }

    [Fact]
    public void Validate_WithDuplicateProductIds_ShouldNotHaveError()
    {
        // Arrange
        var request = new AddPurchaseRequest(1, 10,
        [
            new AddPurchaseRequest.PurchaseItem(1, 1),
            new AddPurchaseRequest.PurchaseItem(1, 1)
        ]);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().NotContain(error =>
            error.PropertyName == nameof(AddPurchaseRequest.Items)
            && error.ErrorMessage.Contains("only once"));
    }
}
