using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.News.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Content.Features.News.Handlers;

public interface IGetNewsHandler : IVersionedHandler<BasicContext>;

public class GetNewsV1Handler(INewsService service) : IGetNewsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var result = await service.GetNews(context.Token);
        return await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}