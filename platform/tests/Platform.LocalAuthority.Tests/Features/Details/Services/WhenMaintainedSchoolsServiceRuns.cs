using Moq;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Details.Services;

public class WhenMaintainedSchoolsServiceRuns
{
    private readonly Mock<IDatabaseConnection> _connection = new();
    private readonly Mock<IDatabaseFactory> _dbFactory = new();
    private readonly MaintainedSchoolsService _service;

    public WhenMaintainedSchoolsServiceRuns()
    {
        _dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);
        _service = new MaintainedSchoolsService(_dbFactory.Object);
    }

    [Fact]
    public async Task GetFinanceSummaryAsyncShouldExecuteCorrectQueryWithNoOptionalFilters()
    {
        PlatformQuery? actualQuery = null;
        _connection.Setup(c => c.QueryAsync<LocalAuthoritySchoolFinanceSummaryResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualQuery = q)
            .ReturnsAsync(new List<LocalAuthoritySchoolFinanceSummaryResponse>());

        await _service.GetFinanceSummaryAsync("LA1", Dimensions.Finance.Actuals, [], [], [], null, "SchoolName", SortDirection.Asc, [], CancellationToken.None);

        Assert.NotNull(actualQuery);
        Assert.Contains("SELECT *", actualQuery.QueryTemplate.RawSql);
        Assert.DoesNotContain("TOP", actualQuery.QueryTemplate.RawSql);
        Assert.Contains("ORDER BY SchoolName ASC", actualQuery.QueryTemplate.RawSql);
        
        var laCodeParam = actualQuery.QueryTemplate.Parameters?.GetTemplateParameters("LaCode");
        Assert.NotNull(laCodeParam);
        Assert.Equal("LA1", laCodeParam["LaCode"]);
    }

    [Fact]
    public async Task GetFinanceSummaryAsyncShouldExecuteCorrectQueryWithAllOptionalFiltersAndLimit()
    {
        PlatformQuery? actualQuery = null;
        _connection.Setup(c => c.QueryAsync<LocalAuthoritySchoolFinanceSummaryResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualQuery = q)
            .ReturnsAsync(new List<LocalAuthoritySchoolFinanceSummaryResponse>());

        await _service.GetFinanceSummaryAsync(
            "LA1", 
            Dimensions.Finance.Actuals, 
            ["Nursery"], 
            ["SixthForm"], 
            ["Special"], 
            50, 
            "TotalPupils", 
            SortDirection.Desc, 
            ["Primary"], 
            CancellationToken.None);

        Assert.NotNull(actualQuery);
        Assert.Contains("SELECT TOP(50) *", actualQuery.QueryTemplate.RawSql);
        Assert.Contains("ORDER BY TotalPupils DESC", actualQuery.QueryTemplate.RawSql);
        
        var parameters = actualQuery.QueryTemplate.Parameters?.GetTemplateParameters("Phase", "NurseryProvision", "SixthFormProvision", "SpecialClassProvision");
        Assert.NotNull(parameters);
        Assert.Equal(new[] { "Primary" }, parameters["Phase"]);
        Assert.Equal(new[] { "Nursery" }, parameters["NurseryProvision"]);
        Assert.Equal(new[] { "SixthForm" }, parameters["SixthFormProvision"]);
        Assert.Equal(new[] { "Special" }, parameters["SpecialClassProvision"]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(101)]
    public async Task GetFinanceSummaryAsyncShouldThrowArgumentOutOfRangeExceptionForInvalidLimit(int invalidLimit)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => 
            _service.GetFinanceSummaryAsync("LA1", Dimensions.Finance.Actuals, [], [], [], invalidLimit, "SchoolName", SortDirection.Asc, [], CancellationToken.None));
    }

    [Fact]
    public async Task GetWorkforceSummaryAsyncShouldExecuteCorrectQueryWithNoOptionalFilters()
    {
        PlatformQuery? actualQuery = null;
        _connection.Setup(c => c.QueryAsync<LocalAuthoritySchoolWorkforceSummaryResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualQuery = q)
            .ReturnsAsync(new List<LocalAuthoritySchoolWorkforceSummaryResponse>());

        await _service.GetWorkforceSummaryAsync("LA1", Dimensions.SchoolsSummaryWorkforce.Actuals, [], [], [], null, "SchoolName", SortDirection.Asc, [], CancellationToken.None);

        Assert.NotNull(actualQuery);
        Assert.Contains("SELECT *", actualQuery.QueryTemplate.RawSql);
        Assert.DoesNotContain("TOP", actualQuery.QueryTemplate.RawSql);
        Assert.Contains("ORDER BY SchoolName ASC", actualQuery.QueryTemplate.RawSql);
    }

    [Fact]
    public async Task GetWorkforceSummaryAsyncShouldExecuteCorrectQueryWithAllOptionalFiltersAndLimit()
    {
        PlatformQuery? actualQuery = null;
        _connection.Setup(c => c.QueryAsync<LocalAuthoritySchoolWorkforceSummaryResponse>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((q, _) => actualQuery = q)
            .ReturnsAsync(new List<LocalAuthoritySchoolWorkforceSummaryResponse>());

        await _service.GetWorkforceSummaryAsync(
            "LA1", 
            Dimensions.SchoolsSummaryWorkforce.Actuals, 
            ["Nursery"], 
            ["SixthForm"], 
            ["Special"], 
            25, 
            "PupilTeacherRatio", 
            SortDirection.Desc, 
            ["Secondary"], 
            CancellationToken.None);

        Assert.NotNull(actualQuery);
        Assert.Contains("SELECT TOP(25) *", actualQuery.QueryTemplate.RawSql);
        Assert.Contains("ORDER BY PupilTeacherRatio DESC", actualQuery.QueryTemplate.RawSql);

        var parameters = actualQuery.QueryTemplate.Parameters?.GetTemplateParameters("Phase", "NurseryProvision", "SixthFormProvision", "SpecialClassProvision");
        Assert.NotNull(parameters);
        Assert.Equal(new[] { "Secondary" }, parameters["Phase"]);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1)]
    [InlineData(101)]
    public async Task GetWorkforceSummaryAsyncShouldThrowArgumentOutOfRangeExceptionForInvalidLimit(int invalidLimit)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => 
            _service.GetWorkforceSummaryAsync("LA1", Dimensions.SchoolsSummaryWorkforce.Actuals, [], [], [], invalidLimit, "SchoolName", SortDirection.Asc, [], CancellationToken.None));
    }
}