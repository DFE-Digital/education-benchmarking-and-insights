using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.Files.Services;

namespace Platform.Api.Insight.Features.Files;

[ExcludeFromCodeCoverage]
public static class FilesFeature
{
    public static IServiceCollection AddFilesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IFilesService, FilesService>();

        return serviceCollection;
    }
}