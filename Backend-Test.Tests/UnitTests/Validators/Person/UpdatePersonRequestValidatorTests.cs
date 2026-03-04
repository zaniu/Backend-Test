using BackendTest.Application.Requests.Person;
using BackendTest.Application.Validators.Person;
using FluentAssertions;

namespace BackendTest.Test.UnitTests.Validators.Person;

public class UpdatePersonRequestValidatorTests
{
    private readonly UpdatePersonRequestValidator _validator = new();

    [Fact]
    public void Validate_WithValidRequest_ShouldNotHaveErrors()
    {
        var request = new UpdatePersonRequest("Jane", "Doe", 1990) with { Id = 1 };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_WithEmptyFirstname_ShouldHaveError()
    {
        var request = new UpdatePersonRequest(string.Empty, "Doe", 1990) with { Id = 1 };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(UpdatePersonRequest.Firstname));
    }

    [Fact]
    public void Validate_WithEmptyLastname_ShouldHaveError()
    {
        var request = new UpdatePersonRequest("Jane", string.Empty, 1990) with { Id = 1 };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(UpdatePersonRequest.Lastname));
    }

    [Fact]
    public void Validate_WithFutureYear_ShouldHaveExpectedErrorMessage()
    {
        var request = new UpdatePersonRequest("Jane", "Doe", DateTime.UtcNow.Year + 1) with { Id = 1 };

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error =>
            error.PropertyName == nameof(UpdatePersonRequest.YearOfBirth)
            && error.ErrorMessage == "Customer can not be born after current year");
    }
}
