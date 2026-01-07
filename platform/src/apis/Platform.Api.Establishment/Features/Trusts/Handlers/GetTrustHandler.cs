using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Establishment.Features.Trusts.Handlers;

public interface IGetTrustHandler : IVersionedHandler<IdContext>;

public class GetTrustV1Handler(ITrustsService service) : IGetTrustHandler
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