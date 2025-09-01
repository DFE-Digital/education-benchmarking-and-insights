using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Content.Features.News.Services;

public interface INewsService
{
    Task<IEnumerable<Models.News>> GetNews(CancellationToken cancellationToken = default);
    Task<Models.News?> GetNewsArticleOrDefault(string slug, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class NewsService(IDatabaseFactory dbFactory) : INewsService
{
    public async Task<IEnumerable<Models.News>> GetNews(CancellationToken cancellationToken = default)
    {
        var query = new PublishedNewsQuery(nameof(Models.News.Title), nameof(Models.News.Slug), nameof(Models.News.Published))
            .OrderBy($"{nameof(Models.News.Published)} DESC");
        using var conn = await dbFactory.GetConnection();

        return await conn.QueryAsync<Models.News>(query, cancellationToken);
    }

    public async Task<Models.News?> GetNewsArticleOrDefault(string slug, CancellationToken cancellationToken = default)
    {
        var query = new PublishedNewsQuery()
            .WhereSlugEqual(slug);
        using var conn = await dbFactory.GetConnection();

        return await conn.QueryFirstOrDefaultAsync<Models.News>(query, cancellationToken);
    }
}