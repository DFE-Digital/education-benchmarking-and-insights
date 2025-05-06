using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;

namespace Platform.Api.Benchmark.Features.ComparatorSets.Validators;

[ExcludeFromCodeCoverage]
public class ComparatorSetUserDefinedSchoolValidator : AbstractValidator<ComparatorSetUserDefinedSchool>
{
    public ComparatorSetUserDefinedSchoolValidator()
    {
        RuleFor(p => p.Set)
            .Must((obj, x) => x != null && obj.URN != null && x.Contains(obj.URN))
            .WithMessage("Set must contain target school");
    }
}