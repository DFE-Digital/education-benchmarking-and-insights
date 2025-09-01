using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Content.Features.News.Handlers;
using Platform.Api.Content.Features.News.Services;
using Platform.Functions;

namespace Platform.Api.Content.Features.News;

[ExcludeFromCodeCoverage]
public static class NewsFeature
{
    public static IServiceCollection AddNewsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetNewsArticleHandler, GetNewsArticleV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetNewsArticleHandler>, VersionedHandlerDispatcher<IGetNewsArticleHandler>>()
            .AddSingleton<INewsService, NewsService>();

        return serviceCollection;
    }
}