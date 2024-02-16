using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Domain;
using FluentValidation;

namespace EducationBenchmarking.Platform.Api.Benchmark.Validators;

[ExcludeFromCodeCoverage]
public class UnknownProximitySortValidator : AbstractValidator<UnknownProximitySort>
{
    public UnknownProximitySortValidator()
    {
        RuleFor(p => p.Kind).NotEmpty()
            .WithMessage($"Must be one of '{ProximitySortKinds.Sen}', '{ProximitySortKinds.Simple}' or '{ProximitySortKinds.Bic}'.");
    }
}

[ExcludeFromCodeCoverage]
public class SenProximitySortValidator : AbstractValidator<SenProximitySort>
{
    public SenProximitySortValidator()
    {
        RuleFor(p => p.Kind).Must(p => p == ProximitySortKinds.Sen);
        RuleFor(p => p.SortBy).NotEmpty();
    }
}

[ExcludeFromCodeCoverage]
public class SimpleProximitySortValidator : AbstractValidator<SimpleProximitySort>
{
    public SimpleProximitySortValidator()
    {
        RuleFor(p => p.Kind).Must(p => p == ProximitySortKinds.Simple);
        RuleFor(p => p.SortBy).NotEmpty();
    }
}

[ExcludeFromCodeCoverage]
public class BestInClassProximitySortValidator : AbstractValidator<BestInClassProximitySort>
{
    public BestInClassProximitySortValidator()
    {
        RuleFor(p => p.Kind).Must(p => p == ProximitySortKinds.Bic);
        RuleFor(p => p.SortBy).NotEmpty();
    }
}