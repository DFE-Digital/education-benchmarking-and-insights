using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Details.Handlers;

public interface IQueryLocalAuthoritiesHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QueryLocalAuthoritiesV1Handler(ILocalAuthorityDetailsService service) : IQueryLocalAuthoritiesHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var localAuthorities = await service.QueryAsync(cancellationToken);
        return await request.CreateJsonResponseAsync(localAuthorities, cancellationToken);
    }
}