using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Schools.Services;

namespace Platform.Api.Establishment.Features.Schools;

[ExcludeFromCodeCoverage]
public static class SchoolsFeature
{
    public static IServiceCollection AddSchoolsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ISchoolComparatorsService, SchoolComparatorsService>();

        return serviceCollection;
    }
}