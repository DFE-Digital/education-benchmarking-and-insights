using EducationBenchmarking.Platform.Shared.Characteristics;
using FluentValidation;

namespace EducationBenchmarking.Platform.Shared.Validators;

public class TrustComparatorSetRequestValidator : AbstractValidator<TrustComparatorSetRequest>
{
    public TrustComparatorSetRequestValidator()
    {
        RuleFor(p => p.Characteristics)
            .Must(x => x is null || x.Keys.All(key => Questions.Trusts.AllCodes.Contains(key)))
            .WithMessage("Invalid characteristics");
    }
}