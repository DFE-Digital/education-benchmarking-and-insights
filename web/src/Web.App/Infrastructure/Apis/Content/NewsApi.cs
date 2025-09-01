namespace Web.App.Infrastructure.Apis.Content;

public interface INewsApi
{
    Task<ApiResult> GetNewsArticle(string slug, CancellationToken cancellationToken = default);
}

public class NewsApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), INewsApi
{
    public async Task<ApiResult> GetNewsArticle(string slug, CancellationToken cancellationToken = default)
    {
        return await GetAsync(Api.News.Article(slug), cancellationToken);
    }
}