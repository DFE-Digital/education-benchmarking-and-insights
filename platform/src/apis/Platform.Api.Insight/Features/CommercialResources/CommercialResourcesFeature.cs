using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.CommercialResources.Parameters;
using Platform.Api.Insight.Features.CommercialResources.Services;
using Platform.Api.Insight.Features.CommercialResources.Validators;

namespace Platform.Api.Insight.Features.CommercialResources;

[ExcludeFromCodeCoverage]
public static class CommercialResourcesFeature
{
    public static IServiceCollection AddCommercialResourcesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ICommercialResourcesService, CommercialResourcesService>()
            .AddTransient<IValidator<CommercialResourcesParameters>, CommercialResourcesParametersValidator>();

        return serviceCollection;
    }
}