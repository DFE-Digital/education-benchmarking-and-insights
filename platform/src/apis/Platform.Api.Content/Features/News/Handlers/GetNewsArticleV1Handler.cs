using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.News.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.News.Handlers;

public interface IGetNewsArticleHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string slug, CancellationToken cancellationToken);
}

public class GetNewsArticleV1Handler(INewsService service) : IGetNewsArticleHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return request.CreateNotFoundResponse();
        }

        var result = await service.GetNewsArticleOrDefault(slug, cancellationToken);
        if (result == null)
        {
            return request.CreateNotFoundResponse();
        }

        return await request.CreateJsonResponseAsync(result, cancellationToken);
    }
}