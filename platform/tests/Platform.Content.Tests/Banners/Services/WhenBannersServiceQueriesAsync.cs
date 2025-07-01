using AutoFixture;
using Moq;
using Platform.Api.Content.Features.Banners.Models;
using Platform.Api.Content.Features.Banners.Services;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Content.Tests.Banners.Services;

public class WhenBannersServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly BannersService _service;

    public WhenBannersServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new BannersService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsync()
    {
        const string target = nameof(target);
        var result = _fixture.Create<Banner>();
        string? actualSql = null;
        Dictionary<string, object>? actualParams = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<Banner>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
                actualParams = query.QueryTemplate.Parameters?.GetTemplateParameters("Target");
            })
            .ReturnsAsync(result);

        var actual = await _service.GetBannerOrDefault(target, CancellationToken.None);

        Assert.Equal(result, actual);
        Assert.Equal("SELECT * FROM VW_ActiveBanners WHERE Target = @Target\n ORDER BY ValidFrom DESC", actualSql);
        Assert.Equal(new Dictionary<string, object>
        {
            { "Target", target }
        }, actualParams);
    }
}