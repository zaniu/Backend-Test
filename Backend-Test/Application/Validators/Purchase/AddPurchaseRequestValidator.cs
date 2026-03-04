using BackendTest.Application.Requests.Purchase;
using FluentValidation;

namespace BackendTest.Application.Validators.Purchase;

public class AddPurchaseRequestValidator : AbstractValidator<AddPurchaseRequest>
{
    public AddPurchaseRequestValidator()
    {
        RuleFor(r => r.Id)
            .GreaterThan(0);

        RuleFor(r => r.ProductsIds)
            .NotNull()
            .Must(products => products.Count > 0)
            .WithMessage("At least one product is required");
    }
}