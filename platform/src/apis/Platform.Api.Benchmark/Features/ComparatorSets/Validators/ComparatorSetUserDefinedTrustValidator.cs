using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;

namespace Platform.Api.Benchmark.Features.ComparatorSets.Validators;

[ExcludeFromCodeCoverage]
public class ComparatorSetUserDefinedTrustValidator : AbstractValidator<ComparatorSetUserDefinedTrust>
{
    public ComparatorSetUserDefinedTrustValidator()
    {
        RuleFor(p => p.Set)
            .Must((obj, x) => x != null && obj.CompanyNumber != null && x.Contains(obj.CompanyNumber))
            .WithMessage("Set must contain target trust");
    }
}