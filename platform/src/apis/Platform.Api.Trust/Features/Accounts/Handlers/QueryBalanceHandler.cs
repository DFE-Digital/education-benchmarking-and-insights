using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Api.Trust.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Accounts.Handlers;

public interface IQueryBalanceHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QueryBalanceV1Handler(IAccountsService service) : IQueryBalanceHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<BalanceQueryParameters>();

        var result = await service.QueryBalanceAsync(queryParams.Trusts, queryParams.Dimension, cancellationToken);
        return await request.CreateJsonResponseAsync(result.MapToApiResponse(), cancellationToken);
    }
}