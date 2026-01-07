using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.CommercialResources.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.CommercialResources.Handlers;

public interface IGetCommercialResourcesHandler : IVersionedHandler<BasicContext>;

public class GetCommercialResourcesV1Handler(ICommercialResourcesService service) : IGetCommercialResourcesHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var result = await service.GetCommercialResources(context.Token);
        return await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}