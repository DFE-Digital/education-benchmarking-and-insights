namespace Web.App.Infrastructure.Apis.Content;

public interface INewsApi
{
    Task<ApiResult> GetNewsArticle(string slug);
}

public class NewsApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), INewsApi
{
    public async Task<ApiResult> GetNewsArticle(string slug)
    {
        return await GetAsync(Api.News.Article(slug));
    }
}