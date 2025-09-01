using AutoFixture;
using Moq;
using Platform.Api.Content.Features.News.Services;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Content.Tests.News.Services;

public class WhenNewsServiceQueriesNewsArticleAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly NewsService _service;

    public WhenNewsServiceQueriesNewsArticleAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new NewsService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsync()
    {
        const string slug = nameof(slug);
        var result = _fixture.Create<Api.Content.Features.News.Models.News>();
        string? actualSql = null;
        Dictionary<string, object>? actualParams = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<Api.Content.Features.News.Models.News>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
                actualParams = query.QueryTemplate.Parameters?.GetTemplateParameters("Slug");
            })
            .ReturnsAsync(result);

        var actual = await _service.GetNewsArticleOrDefault(slug, CancellationToken.None);

        Assert.Equal(result, actual);
        Assert.Equal("SELECT * FROM VW_PublishedNews WHERE Slug = @Slug", actualSql);
        Assert.Equal(new Dictionary<string, object>
        {
            {
                "Slug", slug
            }
        }, actualParams);
    }
}