using AutoFixture;
using Moq;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Details.Services;

public class WhenLocalAuthorityDetailsServiceRuns
{
    private readonly Mock<IDatabaseConnection> _connection = new();
    private readonly Mock<IDatabaseFactory> _dbFactory = new();
    private readonly LocalAuthorityDetailsService _service;
    private readonly Fixture _fixture = new();

    public WhenLocalAuthorityDetailsServiceRuns()
    {
        _dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);
        _service = new LocalAuthorityDetailsService(_dbFactory.Object);
    }

    [Fact]
    public async Task GetAsyncShouldReturnNullIfNotFound()
    {
        var code = "LA1";
        _connection.Setup(c => c.QueryFirstOrDefaultAsync<LocalAuthorityResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LocalAuthorityResponse?)null);

        var result = await _service.GetAsync(code, CancellationToken.None);

        Assert.Null(result);
        _connection.Verify(c => c.QueryAsync<LocalAuthoritySchoolResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetAsyncShouldReturnPopulatedLaIfFoundWithCorrectQueries()
    {
        var code = "LA1";
        var la = _fixture.Create<LocalAuthorityResponse>();
        var schools = _fixture.CreateMany<LocalAuthoritySchoolResponse>().ToList();
        var stats = _fixture.Create<LocalAuthorityHeadlineStatisticsResponse>();

        PlatformQuery? actualLaQuery = null;
        PlatformQuery? actualSchoolsQuery = null;
        PlatformQuery? actualStatsQuery = null;

        _connection.Setup(c => c.QueryFirstOrDefaultAsync<LocalAuthorityResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualLaQuery = q)
            .ReturnsAsync(la);

        _connection.Setup(c => c.QueryAsync<LocalAuthoritySchoolResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualSchoolsQuery = q)
            .ReturnsAsync(schools);

        _connection.Setup(c => c.QueryFirstOrDefaultAsync<LocalAuthorityHeadlineStatisticsResponse?>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualStatsQuery = q)
            .ReturnsAsync(stats);

        var result = await _service.GetAsync(code, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(la, result);
        Assert.Equal(schools, result.Schools);
        Assert.Equal(stats, result.HeadlineStatistics);

        // Assert Callbacks
        Assert.NotNull(actualLaQuery);
        Assert.Contains("SELECT * FROM LocalAuthority", actualLaQuery.QueryTemplate.RawSql);
        var laParams = actualLaQuery.QueryTemplate.Parameters?.GetTemplateParameters("Code");
        Assert.NotNull(laParams);
        Assert.Equal(code, laParams["Code"]);

        Assert.NotNull(actualSchoolsQuery);
        Assert.Contains("SELECT", actualSchoolsQuery.QueryTemplate.RawSql);
        var schoolParams = actualSchoolsQuery.QueryTemplate.Parameters?.GetTemplateParameters("LaCode", "FinanceType");
        Assert.NotNull(schoolParams);
        Assert.Equal(code, schoolParams["LaCode"]);
        Assert.Equal(FinanceType.Maintained, schoolParams["FinanceType"]);

        Assert.NotNull(actualStatsQuery);
        Assert.Contains("VW_LocalAuthorityFinancialHeadlineStatistics", actualStatsQuery.QueryTemplate.RawSql);
        var statsParams = actualStatsQuery.QueryTemplate.Parameters?.GetTemplateParameters("LaCode");
        Assert.NotNull(statsParams);
        Assert.Equal(code, statsParams["LaCode"]);
    }

    [Fact]
    public async Task QueryAsyncShouldCallDatabaseAndReturnResultsOrderedByName()
    {
        var expected = _fixture.CreateMany<LocalAuthorityResponse>().ToList();
        PlatformQuery? actualQuery = null;

        _connection.Setup(c => c.QueryAsync<LocalAuthorityResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualQuery = q)
            .ReturnsAsync(expected);

        var actual = await _service.QueryAsync(CancellationToken.None);

        Assert.Equal(expected, actual);
        Assert.NotNull(actualQuery);
        Assert.Contains("SELECT * FROM LocalAuthority", actualQuery.QueryTemplate.RawSql);
        Assert.Contains("ORDER BY Name", actualQuery.QueryTemplate.RawSql);
    }
}
