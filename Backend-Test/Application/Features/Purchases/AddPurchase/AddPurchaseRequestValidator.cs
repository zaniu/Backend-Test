using FluentValidation;

namespace BackendTest.Application.Features.Purchases.AddPurchase;

public class AddPurchaseRequestValidator : AbstractValidator<AddPurchaseRequest>
{
    public AddPurchaseRequestValidator()
    {
        RuleFor(r => r.Items)
            .NotNull()
            .Must(items => items.Count > 0)
            .WithMessage("At least one product is required");

        RuleForEach(r => r.Items)
            .ChildRules(item =>
            {
                item.RuleFor(i => i.Count).GreaterThan(0);
            });
    }
}
