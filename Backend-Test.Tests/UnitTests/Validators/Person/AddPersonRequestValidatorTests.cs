using BackendTest.Application.Requests.Person;
using BackendTest.Application.Validators.Person;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Validators.Person;

public class AddPersonRequestValidatorTests
{
    private readonly AddPersonRequestValidator _validator = new();

    [Fact]
    public void Validate_WithValidRequest_ShouldNotHaveErrors()
    {
        // Arrange
        var request = new AddPersonRequest(100, "Jane", "Doe", 1990);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_WithEmptyFirstname_ShouldHaveError()
    {
        // Arrange
        var request = new AddPersonRequest(100, string.Empty, "Doe", 1990);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(AddPersonRequest.Firstname));
    }

    [Fact]
    public void Validate_WithEmptyLastname_ShouldHaveError()
    {
        // Arrange
        var request = new AddPersonRequest(100, "Jane", string.Empty, 1990);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(AddPersonRequest.Lastname));
    }

    [Fact]
    public void Validate_WithFutureYear_ShouldHaveExpectedErrorMessage()
    {
        // Arrange
        var request = new AddPersonRequest(100, "Jane", "Doe", DateTime.UtcNow.Year + 1);

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error =>
            error.PropertyName == nameof(AddPersonRequest.YearOfBirth)
            && error.ErrorMessage == "Customer can not be born after current year");
    }
}
