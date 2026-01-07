using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.Banners.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.Banners.Handlers;

public interface IGetBannerHandler : IVersionedHandler<IdContext>;

public class GetBannerV1Handler(IBannersService service) : IGetBannerHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Id))
        {
            return context.Request.CreateNotFoundResponse();
        }

        var result = await service.GetBannerOrDefault(context.Id, context.Token);
        if (result == null)
        {
            return context.Request.CreateNotFoundResponse();
        }

        return await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}