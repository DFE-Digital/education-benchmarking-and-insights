using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Search;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities.Services;

public class WhenLocalAuthoritiesServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly LocalAuthoritiesService _service;

    public WhenLocalAuthoritiesServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        var indexClient = new Mock<IIndexClient>();

        _service = new LocalAuthoritiesService(indexClient.Object, dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAndMapWhenGetStatisticalNeighbours()
    {
        // arrange
        const string code = nameof(code);
        const string name = nameof(name);
        var results = _fixture
            .Build<LocalAuthorityStatisticalNeighbour>()
            .With(n => n.LaCode, code)
            .With(n => n.LaName, name)
            .CreateMany()
            .ToArray();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<LocalAuthorityStatisticalNeighbour>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetStatisticalNeighboursAsync(code);

        // assert
        var expected = new LocalAuthorityStatisticalNeighboursResponse
        {
            Code = code,
            Name = name,
            StatisticalNeighbours = results.Select(r => new LocalAuthorityStatisticalNeighbourResponse
            {
                Code = r.NeighbourLaCode,
                Name = r.NeighbourLaName,
                Position = r.NeighbourPosition
            }).ToArray()
        };
        Assert.Equivalent(expected, actual);
        Assert.Equal("SELECT * FROM VW_LocalAuthorityStatisticalNeighbours WHERE LaCode = @LaCode", actualSql);
    }

    [Fact]
    public async Task ShouldQueryButNotMapWhenGetStatisticalNeighboursReturnsNull()
    {
        // arrange
        const string code = nameof(code);
        var results = Array.Empty<LocalAuthorityStatisticalNeighbour>();

        _connection
            .Setup(c => c.QueryAsync<LocalAuthorityStatisticalNeighbour>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(results)
            .Verifiable();

        // act
        var actual = await _service.GetStatisticalNeighboursAsync(code);

        // assert
        _connection.Verify();
        Assert.Null(actual);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetAll()
    {
        // arrange
        LocalAuthority[] results = [new()];
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<LocalAuthority>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetAllAsync();

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * FROM LocalAuthority  ORDER BY Name", actualSql);
    }
}