using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.News.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.News.Handlers;

public interface IGetNewsArticleHandler : IVersionedHandler<IdContext>;

public class GetNewsArticleV1Handler(INewsService service) : IGetNewsArticleHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Id))
        {
            return context.Request.CreateNotFoundResponse();
        }

        var result = await service.GetNewsArticleOrDefault(context.Id, context.Token);
        if (result == null)
        {
            return context.Request.CreateNotFoundResponse();
        }

        return await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}