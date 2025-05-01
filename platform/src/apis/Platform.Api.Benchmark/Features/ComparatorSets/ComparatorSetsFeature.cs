using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Api.Benchmark.Features.ComparatorSets.Validators;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

[ExcludeFromCodeCoverage]
public static class ComparatorSetsFeature
{
    public static IServiceCollection AddComparatorSetsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IComparatorSetsService, ComparatorSetsService>()
            .AddTransient<IValidator<ComparatorSetUserDefinedSchool>, ComparatorSetUserDefinedSchoolValidator>()
            .AddTransient<IValidator<ComparatorSetUserDefinedTrust>, ComparatorSetUserDefinedTrustValidator>();

        return serviceCollection;
    }
}