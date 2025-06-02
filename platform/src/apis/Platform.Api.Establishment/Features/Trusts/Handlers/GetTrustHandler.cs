using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Establishment.Features.Trusts.Handlers;

public interface IGetTrustHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier,
        CancellationToken cancellationToken = default);
}

public class GetTrustV1Handler : IGetTrustHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier,
        CancellationToken cancellationToken = default)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("V1 Handler Response", cancellationToken);
        return response;
    }
}

public class GetTrustV2Handler(ITrustsService service) : IGetTrustHandler
{
    public string Version => "2.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier,
        CancellationToken cancellationToken = default)
    {
        var response = await service.GetAsync(identifier, cancellationToken);
        if (response == null)
        {
            return request.CreateNotFoundResponse();
        }

        return await request.CreateJsonResponseAsync(response, cancellationToken: cancellationToken);
    }
}