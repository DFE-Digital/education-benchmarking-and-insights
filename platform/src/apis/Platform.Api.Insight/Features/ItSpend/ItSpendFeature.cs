using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Api.Insight.Features.ItSpend.Services;
using Platform.Api.Insight.Features.ItSpend.Validators;

namespace Platform.Api.Insight.Features.ItSpend;

[ExcludeFromCodeCoverage]
public static class ItSpendFeature
{
    public static IServiceCollection AddItSpendFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IItSpendService, ItSpendService>()
            .AddTransient<IValidator<ItSpendSchoolsParameters>, ItSpendSchoolsParametersValidator>();

        return serviceCollection;
    }
}