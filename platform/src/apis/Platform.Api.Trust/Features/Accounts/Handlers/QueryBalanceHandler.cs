using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Api.Trust.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Accounts.Handlers;

public interface IQueryBalanceHandler : IVersionedHandler<BasicContext>;

public class QueryBalanceV1Handler(IAccountsService service) : IQueryBalanceHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<BalanceQueryParameters>();

        var result = await service.QueryBalanceAsync(queryParams.Trusts, queryParams.Dimension, context.Token);
        return await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(), context.Token);
    }
}