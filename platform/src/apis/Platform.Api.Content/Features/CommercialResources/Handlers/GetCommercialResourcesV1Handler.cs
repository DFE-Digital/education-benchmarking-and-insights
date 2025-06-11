using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.CommercialResources.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.CommercialResources.Handlers;

public interface IGetCommercialResourcesHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class GetCommercialResourcesV1Handler(ICommercialResourcesService service) : IGetCommercialResourcesHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var result = await service.GetCommercialResources(cancellationToken);
        return await request.CreateJsonResponseAsync(result, cancellationToken);
    }
}