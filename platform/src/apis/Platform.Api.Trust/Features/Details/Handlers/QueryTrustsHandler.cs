using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Details.Parameters;
using Platform.Api.Trust.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Details.Handlers;

public interface IQueryTrustsHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QueryTrustsV1Handler(ITrustDetailsService service) : IQueryTrustsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<TrustsParameters>();

        var trusts = await service.QueryAsync(queryParams.Trusts, cancellationToken);
        return await request.CreateJsonResponseAsync(trusts, cancellationToken);
    }
}