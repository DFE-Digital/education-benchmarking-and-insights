using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Establishment.Features.Trusts.Handlers;

public interface IGetTrustHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetTrustV1Handler(ITrustsService service) : IGetTrustHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var response = await service.GetAsync(identifier, cancellationToken);

        return response == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(response, cancellationToken);
    }
}