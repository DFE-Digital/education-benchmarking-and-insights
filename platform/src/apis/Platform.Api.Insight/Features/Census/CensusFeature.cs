using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Api.Insight.Features.Census.Services;
using Platform.Api.Insight.Features.Census.Validators;

namespace Platform.Api.Insight.Features.Census;

[ExcludeFromCodeCoverage]
public static class CensusFeature
{
    public static IServiceCollection AddCensusFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ICensusService, CensusService>()
            .AddTransient<IValidator<CensusParameters>, CensusParametersValidator>()
            .AddTransient<IValidator<CensusNationalAvgParameters>, CensusNationalAvgParametersValidator>()
            .AddTransient<IValidator<CensusQuerySchoolsParameters>, CensusQuerySchoolsParametersValidator>();

        return serviceCollection;
    }
}