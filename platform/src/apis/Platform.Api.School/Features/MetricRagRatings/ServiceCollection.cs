using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.School.Features.MetricRagRatings.Handlers;
using Platform.Api.School.Features.MetricRagRatings.Parameters;
using Platform.Api.School.Features.MetricRagRatings.Services;
using Platform.Api.School.Features.MetricRagRatings.Validators;
using Platform.Functions;

namespace Platform.Api.School.Features.MetricRagRatings;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddMetricRagRatingsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetUserDefinedHandler, GetUserDefinedV1Handler>()
            .AddSingleton<IQueryDetailsHandler, QueryDetailsV1Handler>()
            .AddSingleton<IQuerySummaryHandler, QuerySummaryV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetUserDefinedHandler>, VersionedHandlerDispatcher<IGetUserDefinedHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryDetailsHandler>, VersionedHandlerDispatcher<IQueryDetailsHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQuerySummaryHandler>, VersionedHandlerDispatcher<IQuerySummaryHandler>>()
            .AddTransient<IValidator<DetailParameters>, DetailParametersValidator>()
            .AddTransient<IValidator<SummaryParameters>, SummaryParametersValidator>()
            .AddSingleton<IMetricRagRatingsService, MetricRagRatingsService>();

        return serviceCollection;
    }
}