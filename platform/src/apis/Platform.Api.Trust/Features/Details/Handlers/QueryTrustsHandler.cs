using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Details.Parameters;
using Platform.Api.Trust.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Details.Handlers;

public interface IQueryTrustsHandler : IVersionedHandler<BasicContext>;

public class QueryTrustsV1Handler(ITrustDetailsService service) : IQueryTrustsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<TrustsParameters>();

        var trusts = await service.QueryAsync(queryParams.Trusts, context.Token);
        return await context.Request.CreateJsonResponseAsync(trusts, context.Token);
    }
}