using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.Files.Handlers;
using Platform.Api.Content.Features.Files.Services;

namespace Platform.Api.Content.Features.Files;

[ExcludeFromCodeCoverage]
public static class FilesFeature
{
    public static IServiceCollection AddFilesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetTransparencyFilesHandler, GetTransparencyFilesV1Handler>()
            .AddSingleton<IGetLocalAuthorityRiskScoreFileHandler, GetLocalAuthorityRiskScoreFileV1Handler>()
            .AddSingleton<IFilesService, FilesService>();

        return serviceCollection;
    }
}
