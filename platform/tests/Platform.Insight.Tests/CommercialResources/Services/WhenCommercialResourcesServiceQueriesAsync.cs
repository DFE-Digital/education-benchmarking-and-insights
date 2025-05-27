using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.CommercialResources.Models;
using Platform.Api.Insight.Features.CommercialResources.Services;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Insight.Tests.CommercialResources.Services;

public class WhenCommercialResourcesServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly CommercialResourcesService _service;

    public WhenCommercialResourcesServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new CommercialResourcesService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsync()
    {
        var results = _fixture.Build<CommercialResource>().CreateMany().ToArray();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<CommercialResource>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        var actual = await _service.GetCommercialResources(CancellationToken.None);

        Assert.Equal(results, actual);
        Assert.Equal("SELECT * FROM VW_CommercialResources", actualSql);
    }
}