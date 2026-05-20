using AutoFixture;
using Moq;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Services;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.StatisticalNeighbours.Services;

public class WhenStatisticalNeighboursServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly StatisticalNeighboursService _service;

    public WhenStatisticalNeighboursServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new StatisticalNeighboursService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsync()
    {
        var laCode = _fixture.Create<string>();
        var result = _fixture.CreateMany<StatisticalNeighbourDto>().ToList();
        string? actualSql = null;
        Dictionary<string, object>? actualParams = null;

        _connection
            .Setup(c => c.QueryAsync<StatisticalNeighbourDto>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
                actualParams = query.QueryTemplate.Parameters?.GetTemplateParameters("LaCode");
            })
            .ReturnsAsync(result);

        var actual = await _service.GetAsync(laCode, CancellationToken.None);

        Assert.Equal(result, actual);
        Assert.Equal("SELECT * FROM VW_LocalAuthorityStatisticalNeighbours WHERE LaCode = @LaCode", actualSql);
        Assert.NotNull(actualParams);
        Assert.Equal(laCode, actualParams["LaCode"]);
    }
}
