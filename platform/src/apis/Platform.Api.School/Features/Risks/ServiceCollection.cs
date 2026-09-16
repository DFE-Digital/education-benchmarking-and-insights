using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.School.Features.Risks.Handlers;
using Platform.Api.School.Features.Risks.Services;

namespace Platform.Api.School.Features.Risks;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddRisksFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetSchoolRisksHistoryHandler, GetSchoolRisksHistoryHandlerV1>()
            .AddSingleton<ISchoolRisksService, SchoolRisksService>();

        return serviceCollection;
    }
}
