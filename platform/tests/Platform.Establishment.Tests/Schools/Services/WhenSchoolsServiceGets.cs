using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Domain;
using Platform.Search;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Establishment.Tests.Schools.Services;

public class WhenSchoolsServiceGets
{
    private readonly Mock<IDatabaseConnection> _connection = new();
    private readonly Mock<IIndexClient> _client = new();
    private readonly Fixture _fixture = new();
    private readonly SchoolsService _service;

    public WhenSchoolsServiceGets()
    {
        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new SchoolsService(_client.Object, dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryWhenGetAsyncWithoutFederationLeadURN()
    {
        // arrange
        const string urn = nameof(urn);
        var result = _fixture.Build<School>()
            .Without(x => x.FederationLeadURN)
            .Without(x => x.FederationSchools)
            .Create();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<School>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(result);

        // act
        var actual = await _service.GetAsync(urn, CancellationToken.None);

        // assert
        Assert.Equal(result, actual);
        Assert.Null(result.FederationSchools);
        Assert.Equal("SELECT * FROM School WHERE URN = @URN", actualSql);
    }

    [Fact]
    public async Task ShouldQueryTwiceWhenGetAsyncWithFederationLeadURN()
    {
        // arrange
        const string urn = nameof(urn);
        var result = _fixture.Build<School>()
            .Without(x => x.FederationSchools)
            .Create();
        string? firstSql = null;
        string? secondSql = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<School>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                firstSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(result);

        _connection
            .Setup(c => c.QueryAsync<School>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                secondSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync([result]);

        // act
        var actual = await _service.GetAsync(urn, CancellationToken.None);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal([result], result.FederationSchools);
        Assert.Equal("SELECT * FROM School WHERE URN = @URN", firstSql);
        Assert.Equal("SELECT * FROM School WHERE FederationLeadURN = @FederationLeadURN", secondSql);
    }

    [Fact]
    public async Task ShouldQueryWhenGetSchoolStatusAsync()
    {
        // arrange
        const string urn = nameof(urn);
        var result = _fixture.Create<SchoolStatus>();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<SchoolStatus>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(result);

        // act
        var actual = await _service.GetSchoolStatusAsync(urn, CancellationToken.None);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal("SELECT * FROM VW_SchoolStatus WHERE URN = @URN", actualSql);
    }
}