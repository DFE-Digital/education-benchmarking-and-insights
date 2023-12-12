using FluentValidation;

namespace EducationBenchmarking.Platform.Shared.Validators;

public class SchoolComparatorSetRequestValidator : AbstractValidator<SchoolComparatorSetRequest>
{
    public SchoolComparatorSetRequestValidator()
    {
        RuleFor(p => p.Characteristics)
            .Must(x => x is null || x.Keys.All(key => Characteristics.Schools.AllCodes.Contains(key)))
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