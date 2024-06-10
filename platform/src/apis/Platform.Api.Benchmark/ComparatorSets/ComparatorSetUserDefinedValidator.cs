using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation;

namespace Platform.Api.Benchmark.ComparatorSets;

[ExcludeFromCodeCoverage]
public class ComparatorSetUserDefinedSchoolValidator : AbstractValidator<ComparatorSetUserDefinedSchool>
{
    public ComparatorSetUserDefinedSchoolValidator()
    {
        RuleFor(p => p.Set)
            .Must((obj, x) => x.Contains(obj.URN))
            .WithMessage("Set must contain target school");
    }
}

[ExcludeFromCodeCoverage]
public class ComparatorSetUserDefinedTrustValidator : AbstractValidator<ComparatorSetUserDefinedTrust>
{
    public ComparatorSetUserDefinedTrustValidator()
    {
        RuleFor(p => p.Set)
            .Must((obj, x) => x.Contains(obj.CompanyNumber))
            .WithMessage("Set must contain target trust");
    }
}