using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.Files.Handlers;
using Platform.Api.Content.Features.Files.Services;
using Platform.Functions;

namespace Platform.Api.Content.Features.Files;

[ExcludeFromCodeCoverage]
public static class FilesFeature
{
    public static IServiceCollection AddFilesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetTransparencyFilesHandler, GetTransparencyFilesV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetTransparencyFilesHandler>, VersionedHandlerDispatcher<IGetTransparencyFilesHandler>>()
            .AddSingleton<IFilesService, FilesService>();

        return serviceCollection;
    }
}