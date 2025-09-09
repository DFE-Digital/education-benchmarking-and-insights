using AutoFixture;
using Moq;
using Platform.Api.Content.Features.News.Services;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Content.Tests.News.Services;

public class WhenNewsServiceQueriesNewsAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly NewsService _service;

    public WhenNewsServiceQueriesNewsAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new NewsService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsync()
    {
        var results = _fixture.Build<Api.Content.Features.News.Models.News>().CreateMany().ToArray();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<Api.Content.Features.News.Models.News>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        var actual = await _service.GetNews(CancellationToken.None);

        Assert.Equal(results, actual);
        Assert.Equal("SELECT Title , Slug , Published\n FROM VW_PublishedNews  ORDER BY Published DESC", actualSql);
    }
}