using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.Files.Services;

namespace Platform.Api.Content.Features.Files;

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