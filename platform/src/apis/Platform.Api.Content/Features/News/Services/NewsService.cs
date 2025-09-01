using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Content.Features.News.Services;

public interface INewsService
{
    Task<Models.News?> GetNewsArticleOrDefault(string slug, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class NewsService(IDatabaseFactory dbFactory) : INewsService
{
    public async Task<Models.News?> GetNewsArticleOrDefault(string slug, CancellationToken cancellationToken = default)
    {
        var query = new PublishedNewsQuery()
            .WhereSlugEqual(slug);
        using var conn = await dbFactory.GetConnection();

        return await conn.QueryFirstOrDefaultAsync<Models.News>(query, cancellationToken);
    }
}