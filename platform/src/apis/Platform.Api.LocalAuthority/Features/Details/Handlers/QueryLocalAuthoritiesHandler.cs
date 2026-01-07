using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Details.Handlers;

public interface IQueryLocalAuthoritiesHandler : IVersionedHandler<BasicContext>;

public class QueryLocalAuthoritiesV1Handler(ILocalAuthorityDetailsService service) : IQueryLocalAuthoritiesHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var localAuthorities = await service.QueryAsync(context.Token);
        return await context.Request.CreateJsonResponseAsync(localAuthorities, context.Token);
    }
}