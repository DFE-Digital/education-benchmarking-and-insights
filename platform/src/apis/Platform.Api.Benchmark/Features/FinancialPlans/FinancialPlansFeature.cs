using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;

namespace Platform.Api.Benchmark.Features.FinancialPlans;

[ExcludeFromCodeCoverage]
public static class FinancialPlansFeature
{
    public static IServiceCollection AddFinancialPlansFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IFinancialPlansService, FinancialPlansService>();

        return serviceCollection;
    }
}