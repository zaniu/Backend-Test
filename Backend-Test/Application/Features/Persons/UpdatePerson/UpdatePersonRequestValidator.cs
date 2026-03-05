using FluentValidation;

namespace BackendTest.Application.Features.Persons.UpdatePerson;

public class UpdatePersonRequestValidator : AbstractValidator<UpdatePersonRequest>
{
    public UpdatePersonRequestValidator()
    {
        RuleFor(r => r.Firstname)
            .NotEmpty();

        RuleFor(r => r.Lastname)
            .NotEmpty();

        RuleFor(r => r.YearOfBirth)
            .LessThanOrEqualTo(_ => DateTime.UtcNow.Year)
            .WithMessage("Customer can not be born after current year");
    }
}
