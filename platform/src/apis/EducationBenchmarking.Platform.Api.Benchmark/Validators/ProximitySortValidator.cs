using EducationBenchmarking.Platform.Shared;
using FluentValidation;

namespace EducationBenchmarking.Platform.Api.Benchmark.Validators;

public class UnknownProximitySortValidator : AbstractValidator<UnknownProximitySort>
{
    public UnknownProximitySortValidator()
    {
        RuleFor(p => p.Kind).NotEmpty()
            .WithMessage($"Must be one of '{ProximitySortKinds.Sen}', '{ProximitySortKinds.Simple}' or '{ProximitySortKinds.Bic}'.");
    }
}

public class SenProximitySortValidator : AbstractValidator<SenProximitySort>
{
    public SenProximitySortValidator()
    {
        RuleFor(p => p.Kind).Must(p => p == ProximitySortKinds.Sen);
        RuleFor(p => p.SortBy).NotEmpty();
    }
}

public class SimpleProximitySortValidator : AbstractValidator<SimpleProximitySort>
{
    public SimpleProximitySortValidator()
    {
        RuleFor(p => p.Kind).Must(p => p == ProximitySortKinds.Simple);
        RuleFor(p => p.SortBy).NotEmpty();
    }
}

public class BestInClassProximitySortValidator : AbstractValidator<BestInClassProximitySort>
{
    public BestInClassProximitySortValidator()
    {
        RuleFor(p => p.Kind).Must(p => p == ProximitySortKinds.Bic);
        RuleFor(p => p.SortBy).NotEmpty(); 
    }
}