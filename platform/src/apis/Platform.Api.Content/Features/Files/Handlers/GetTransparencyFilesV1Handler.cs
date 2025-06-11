using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.Files.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.Files.Handlers;

public interface IGetTransparencyFilesHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class GetTransparencyFilesV1Handler(IFilesService service) : IGetTransparencyFilesHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var result = await service.GetActiveFilesByType(cancellationToken, "transparency-aar", "transparency-cfr");
        return await request.CreateJsonResponseAsync(result.MapToApiResponse(), cancellationToken);
    }
}