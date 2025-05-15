using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.CommercialResources.Responses;
using Platform.Api.Insight.Features.CommercialResources.Services;
using Platform.Domain;
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
    public async Task ShouldQueryAsyncWhenGetAll()
    {
        string[] categories = [];
        var results = _fixture.Build<CommercialResourcesResponse>().CreateMany().ToArray();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<CommercialResourcesResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        var actual = await _service.GetCommercialResourcesByCategory(categories, CancellationToken.None);

        Assert.Equal(results, actual);
        Assert.Equal("SELECT * FROM VW_CommercialResources", actualSql);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetCategories()
    {
        string[] categories = [CostCategories.TeachingStaff, CostCategories.EducationalSupplies];
        var results = _fixture.Build<CommercialResourcesResponse>().CreateMany().ToArray();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<CommercialResourcesResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        var actual = await _service.GetCommercialResourcesByCategory(categories, CancellationToken.None);

        Assert.Equal(results, actual);
        Assert.Equal("SELECT * FROM VW_CommercialResources WHERE Category IN @Categories", actualSql);
    }
}