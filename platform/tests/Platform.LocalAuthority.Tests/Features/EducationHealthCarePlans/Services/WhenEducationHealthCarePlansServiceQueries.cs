using AutoFixture;
using Moq;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Services;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.EducationHealthCarePlans.Services;

public class WhenEducationHealthCarePlansServiceQueries
{
    private readonly Mock<IDatabaseConnection> _connection = new();
    private readonly Mock<IDatabaseFactory> _dbFactory = new();
    private readonly EducationHealthCarePlansService _service;
    private readonly Fixture _fixture = new();

    public WhenEducationHealthCarePlansServiceQueries()
    {
        _dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);
        _service = new EducationHealthCarePlansService(_dbFactory.Object);
    }

    [Fact]
    public async Task QueryAsyncShouldCallDatabaseAndReturnResults()
    {
        var codes = new[] { "LA1", "LA2" };
        var expected = _fixture.CreateMany<EducationHealthCarePlansDto>().ToList();
        PlatformQuery? actualQuery = null;

        _connection.Setup(c => c.QueryAsync<EducationHealthCarePlansDto>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualQuery = q)
            .ReturnsAsync(expected);

        var actual = await _service.QueryAsync(codes, Dimensions.EducationHealthCarePlans.Actuals, CancellationToken.None);

        Assert.Equal(expected, actual);
        Assert.NotNull(actualQuery);
        Assert.Contains("VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentActual", actualQuery.QueryTemplate.RawSql);

        var templateParams = actualQuery.QueryTemplate.Parameters?.GetTemplateParameters("LaCodes");
        Assert.NotNull(templateParams);
        Assert.Equal(codes, templateParams["LaCodes"]);
    }

    [Fact]
    public async Task QueryHistoryAsyncShouldCallDatabaseAndReturnResultsWhenYearsFound()
    {
        var codes = new[] { "LA1", "LA2" };
        var years = _fixture.Create<YearsModelDto>();
        var expectedResults = _fixture.CreateMany<EducationHealthCarePlansYearDto>().ToList();
        PlatformQuery? actualYearsQuery = null;
        PlatformQuery? actualHistoryQuery = null;

        _connection.Setup(c => c.QueryFirstOrDefaultAsync<YearsModelDto>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualYearsQuery = q)
            .ReturnsAsync(years);

        _connection.Setup(c => c.QueryAsync<EducationHealthCarePlansYearDto>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualHistoryQuery = q)
            .ReturnsAsync(expectedResults);

        var (actualYears, actualResults) = await _service.QueryHistoryAsync(codes, Dimensions.EducationHealthCarePlans.Per1000, CancellationToken.None);

        Assert.Equal(years, actualYears);
        Assert.Equal(expectedResults, actualResults);

        Assert.NotNull(actualYearsQuery);
        Assert.Contains("VW_YearsLocalAuthority", actualYearsQuery.QueryTemplate.RawSql);

        Assert.NotNull(actualHistoryQuery);
        Assert.Contains("VW_LocalAuthorityEducationHealthCarePlansDefaultPerPopulation", actualHistoryQuery.QueryTemplate.RawSql);

        var historyParams = actualHistoryQuery.QueryTemplate.Parameters?.GetTemplateParameters("StartYear", "EndYear");
        Assert.NotNull(historyParams);
        Assert.Equal(years.StartYear, historyParams["StartYear"]);
        Assert.Equal(years.EndYear, historyParams["EndYear"]);
    }

    [Fact]
    public async Task QueryHistoryAsyncShouldReturnNullWhenNoYearsFound()
    {
        var codes = new[] { "LA1" };
        _connection.Setup(c => c.QueryFirstOrDefaultAsync<YearsModelDto>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((YearsModelDto?)null);

        var (actualYears, actualResults) = await _service.QueryHistoryAsync(codes, Dimensions.EducationHealthCarePlans.Per1000Pupil, CancellationToken.None);

        Assert.Null(actualYears);
        Assert.Empty(actualResults);
        _connection.Verify(c => c.QueryAsync<EducationHealthCarePlansYearDto>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
