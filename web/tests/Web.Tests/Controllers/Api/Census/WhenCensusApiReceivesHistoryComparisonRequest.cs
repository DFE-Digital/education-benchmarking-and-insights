using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App.Controllers.Api;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Services;
using Xunit;
namespace Web.Tests.Controllers.Api.Census;

public class WhenCensusApiReceivesHistoryComparisonRequest
{
    private readonly CensusProxyController _api;
    private readonly Mock<ICensusApi> _censusApi = new();
    private readonly Mock<IEstablishmentApi> _establishmentApi = new();
    private readonly Fixture _fixture;
    private readonly NullLogger<CensusProxyController> _logger = new();
    private readonly Mock<ISchoolComparatorSetService> _schoolComparatorSetService = new();
    private readonly Mock<IUserDataService> _userDataService = new();

    public WhenCensusApiReceivesHistoryComparisonRequest()
    {
        _api = new CensusProxyController(_logger, _establishmentApi.Object, _censusApi.Object, _schoolComparatorSetService.Object, _userDataService.Object);
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData("urn", "dimension", null, null, "?dimension=dimension")]
    public async Task ShouldGetCensusHistoryFromApiForSchool(string urn, string dimension, string? phase, string? financeType, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<CensusHistory>();
        var actualQuery = string.Empty;

        var school = _fixture.Build<School>()
            .With(s => s.URN, urn)
            .Create();
        _establishmentApi
            .Setup(e => e.GetSchool(urn))
            .ReturnsAsync(ApiResult.Ok(school));

        _censusApi
            .Setup(e => e.SchoolHistory(urn, It.IsAny<ApiQuery?>()))
            .Callback<string, ApiQuery?>((_, query) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.HistoryComparison(urn, dimension, phase, financeType);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json?.School as CensusHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("urn", "dimension", null, null, "?dimension=dimension")]
    public async Task ShouldGetCensusHistoryComparatorSetAverageFromApiForSchool(string urn, string dimension, string? phase, string? financeType, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<CensusHistory>();
        var actualQuery = string.Empty;

        var school = _fixture.Build<School>()
            .With(s => s.URN, urn)
            .Create();
        _establishmentApi
            .Setup(e => e.GetSchool(urn))
            .ReturnsAsync(ApiResult.Ok(school));

        _censusApi
            .Setup(e => e.SchoolHistoryComparatorSetAverage(urn, It.IsAny<ApiQuery?>()))
            .Callback<string, ApiQuery?>((_, query) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.HistoryComparison(urn, dimension, phase, financeType);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json?.ComparatorSetAverage as CensusHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("urn", "dimension", null, null, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", "financeType", null, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", null, "phase", "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", "financeType", "phase", "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=financeType&phase=phase")]
    public async Task ShouldGetCensusHistoryNationalAverageFromApiForSchool(string urn, string dimension, string? financeType, string? phase, string schoolFinanceType, string schoolOverallPhase, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<CensusHistory>();
        var actualQuery = string.Empty;

        var school = new School
        {
            URN = urn,
            FinanceType = schoolFinanceType,
            OverallPhase = schoolOverallPhase
        };
        _establishmentApi
            .Setup(e => e.GetSchool(urn))
            .ReturnsAsync(ApiResult.Ok(school));

        _censusApi
            .Setup(e => e.SchoolHistoryNationalAverage(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(query =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.HistoryComparison(urn, dimension, phase, financeType);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json?.NationalAverage as CensusHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }
}