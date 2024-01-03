using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EducationBenchmarking.Platform.Api.Benchmark.Models;
using EducationBenchmarking.Platform.Api.Benchmark.Models.Characteristics;
using EducationBenchmarking.Platform.Api.Benchmark.Requests;
using FluentValidation;

namespace EducationBenchmarking.Platform.Api.Benchmark.Validators;

[ExcludeFromCodeCoverage]
public class ComparatorSetRequestValidator : AbstractValidator<ComparatorSetRequest>
{
    public ComparatorSetRequestValidator()
    {
        RuleFor(p => p.Characteristics)
            .Must(x => x is null || x.Keys.All(key => Characteristics.AllCodes.Contains(key)))
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