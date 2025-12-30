using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Census.Handlers;

public interface IGetHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetV1Handler(ICensusService service) : IGetHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(identifier, cancellationToken);
        return result == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(result.MapToApiResponse(), cancellationToken);
    }
}