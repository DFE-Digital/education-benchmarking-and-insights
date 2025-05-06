using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Api.Insight.Features.MetricRagRatings.Services;
using Platform.Api.Insight.Features.MetricRagRatings.Validators;

namespace Platform.Api.Insight.Features.MetricRagRatings;

[ExcludeFromCodeCoverage]
public static class MetricRagRatingsFeature
{
    public static IServiceCollection AddMetricRagRatingsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IMetricRagRatingsService, MetricRagRatingsService>()
            .AddTransient<IValidator<MetricRagRatingsParameters>, MetricRagRatingsParametersValidator>();

        return serviceCollection;
    }
}