using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Details.Handlers;

public interface IGetLocalAuthorityHandler : IVersionedHandler<IdContext>;

public class GetLocalAuthorityV1Handler(ILocalAuthorityDetailsService service) : IGetLocalAuthorityHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var response = await service.GetAsync(context.Id, context.Token);

        return response == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(response, context.Token);
    }
}