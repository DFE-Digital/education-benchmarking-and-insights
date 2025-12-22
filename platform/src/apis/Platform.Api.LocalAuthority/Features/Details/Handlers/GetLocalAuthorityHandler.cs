using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Details.Handlers;

public interface IGetLocalAuthorityHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetLocalAuthorityV1Handler(ILocalAuthorityDetailsService service) : IGetLocalAuthorityHandler
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