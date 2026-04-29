using AutoFixture;
using Moq;
using Platform.Api.LocalAuthority.Features.Accounts.Models;
using Platform.Api.LocalAuthority.Features.Accounts.Services;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Accounts.Services;

public class WhenHighNeedsServiceQueries
{
    private readonly Mock<IDatabaseConnection> _connection = new();
    private readonly Mock<IDatabaseFactory> _dbFactory = new();
    private readonly HighNeedsService _service;
    private readonly Fixture _fixture = new();

    public WhenHighNeedsServiceQueries()
    {
        _dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);
        _service = new HighNeedsService(_dbFactory.Object);
    }

    [Fact]
    public async Task QueryAsyncShouldCallDatabaseAndReturnResults()
    {
        var codes = new[] { "LA1", "LA2" };
        var expected = _fixture.CreateMany<LocalAuthority<HighNeeds>>().ToList();
        PlatformQuery? actualQuery = null;

        _connection.Setup(c => c.QueryAsync(
                It.IsAny<PlatformQuery>(),
                It.IsAny<Type[]>(),
                It.IsAny<Func<object[], LocalAuthority<HighNeeds>>>(),
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, Type[], Func<object[], LocalAuthority<HighNeeds>>, string[], CancellationToken>((q, _, _, _, _) => actualQuery = q)
            .ReturnsAsync(expected);

        var actual = await _service.QueryAsync(codes, Dimensions.HighNeeds.Actuals, CancellationToken.None);

        Assert.Equal(expected, actual);
        Assert.NotNull(actualQuery);
        Assert.Contains("VW_LocalAuthorityFinancialDefaultCurrentActual", actualQuery.QueryTemplate.RawSql);
        
        var templateParams = actualQuery.QueryTemplate.Parameters?.GetTemplateParameters("LaCodes");
        Assert.NotNull(templateParams);
        Assert.Equal(codes, templateParams["LaCodes"]);
    }

    [Fact]
    public async Task QueryHistoryAsyncShouldCallDatabaseAndReturnResultsWhenYearsFound()
    {
        var codes = new[] { "LA1" };
        var years = _fixture.Create<YearsModelDto>();
        var expectedResults = _fixture.CreateMany<(HighNeedsYear outturn, HighNeedsYear budget)>().ToList();
        PlatformQuery? actualYearsQuery = null;
        PlatformQuery? actualHistoryQuery = null;

        _connection.Setup(c => c.QueryFirstOrDefaultAsync<YearsModelDto>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualYearsQuery = q)
            .ReturnsAsync(years);

        _connection.Setup(c => c.QueryAsync(
                It.IsAny<PlatformQuery>(),
                It.IsAny<Type[]>(),
                It.IsAny<Func<object[], (HighNeedsYear outturn, HighNeedsYear budget)>>(),
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, Type[], Func<object[], (HighNeedsYear outturn, HighNeedsYear budget)>, string[], CancellationToken>((q, _, _, _, _) => actualHistoryQuery = q)
            .ReturnsAsync(expectedResults);

        var actual = await _service.QueryHistoryAsync(codes, Dimensions.HighNeeds.PerHead, CancellationToken.None);

        Assert.NotNull(actual);
        Assert.Equal(years.StartYear, actual.StartYear);
        Assert.Equal(years.EndYear, actual.EndYear);
        Assert.Equal(expectedResults.Count, actual.Outturn!.Length);
        Assert.Equal(expectedResults.Count, actual.Budget!.Length);
        
        Assert.NotNull(actualYearsQuery);
        Assert.Contains("VW_YearsLocalAuthority", actualYearsQuery.QueryTemplate.RawSql);
        
        Assert.NotNull(actualHistoryQuery);
        Assert.Contains("VW_LocalAuthorityFinancialDefaultPerPopulation", actualHistoryQuery.QueryTemplate.RawSql);
        
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

        var actual = await _service.QueryHistoryAsync(codes, Dimensions.HighNeeds.PerPupil, CancellationToken.None);

        Assert.Null(actual);
        _connection.Verify(c => c.QueryAsync(
            It.IsAny<PlatformQuery>(),
            It.IsAny<Type[]>(),
            It.IsAny<Func<object[], (HighNeedsYear outturn, HighNeedsYear budget)>>(),
            It.IsAny<string[]>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }
}