using FluentValidation;

namespace EducationBenchmarking.Platform.Shared.Validators;

public class TrustComparatorSetRequestValidator : AbstractValidator<TrustComparatorSetRequest>
{
    public TrustComparatorSetRequestValidator()
    {
        RuleFor(p => p.Characteristics)
            .Must(x => x is null || x.Keys.All(key => Characteristics.Trusts.AllCodes.Contains(key)))
            .WithMessage("Invalid characteristics");
        
        RuleFor(x => x.SortMethod).SetInheritanceValidator(v =>
        {
            v.Add(new UnknownProximitySortValidator());
            v.Add(new SenProximitySortValidator());
            v.Add(new SimpleProximitySortValidator());
            v.Add(new BestInClassProximitySortValidator());
        });
    }
}