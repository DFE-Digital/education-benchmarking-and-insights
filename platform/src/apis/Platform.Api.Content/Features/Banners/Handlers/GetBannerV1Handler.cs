using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.Banners.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.Banners.Handlers;

public interface IGetBannerHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string target, CancellationToken cancellationToken);
}

public class GetBannerV1Handler(IBannersService service) : IGetBannerHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string target, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            return request.CreateNotFoundResponse();
        }

        var result = await service.GetBannerOrDefault(target, cancellationToken);
        if (result == null)
        {
            return request.CreateNotFoundResponse();
        }

        return await request.CreateJsonResponseAsync(result, cancellationToken);
    }
}