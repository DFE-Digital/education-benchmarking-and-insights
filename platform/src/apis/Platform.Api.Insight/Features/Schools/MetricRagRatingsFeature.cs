using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.Schools.Services;

namespace Platform.Api.Insight.Features.Schools;

[ExcludeFromCodeCoverage]
public static class SchoolsFeature
{
    public static IServiceCollection AddSchoolsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ISchoolsService, SchoolsService>();

        return serviceCollection;
    }
}