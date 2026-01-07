using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.School.Features.Details.Handlers;
using Platform.Api.School.Features.Details.Services;
using Platform.Functions;

namespace Platform.Api.School.Features.Details;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddDetailsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetSchoolHandler, GetSchoolV1Handler>()
            .AddSingleton<IQuerySchoolsHandler, QuerySchoolsV1Handler>()
            .AddSingleton<IGetSchoolCharacteristicsHandler, GetSchoolCharacteristicsV1Handler>()
            .AddSingleton<ISchoolDetailsService, SchoolDetailsService>();


        return serviceCollection;
    }
}