using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App;
using Web.App.Controllers.Api;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Services;
using Xunit;
namespace Web.Tests.Controllers.Api.Expenditure;

public class WhenExpenditureApiReceivesHistoryComparisonRequest
{
    private readonly ExpenditureProxyController _api;
    private readonly Mock<IEstablishmentApi> _establishmentApi = new();
    private readonly Mock<IExpenditureApi> _expenditureApi = new();
    private readonly Fixture _fixture;
    private readonly NullLogger<ExpenditureProxyController> _logger = new();
    private readonly Mock<ISchoolComparatorSetService> _schoolComparatorSetService = new();
    private readonly Mock<ITrustComparatorSetService> _trustComparatorSetService = new();
    private readonly Mock<IUserDataService> _userDataService = new();

    public WhenExpenditureApiReceivesHistoryComparisonRequest()
    {
        _api = new ExpenditureProxyController(_logger, _establishmentApi.Object, _expenditureApi.Object, _schoolComparatorSetService.Object, _trustComparatorSetService.Object, _userDataService.Object);
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData("urn", "dimension", null, null, null, "?dimension=dimension")]
    [InlineData("urn", "dimension", null, null, true, "?dimension=dimension&excludeCentralServices=true")]
    public async Task ShouldGetExpenditureHistoryFromApiForSchool(string urn, string dimension, string? phase, string? financeType, bool? excludeCentralServices, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<ExpenditureHistory>();
        var actualQuery = string.Empty;

        var school = _fixture.Build<School>()
            .With(s => s.URN, urn)
            .Create();
        _establishmentApi
            .Setup(e => e.GetSchool(urn))
            .ReturnsAsync(ApiResult.Ok(school));

        _expenditureApi
            .Setup(e => e.SchoolHistory(urn, It.IsAny<ApiQuery?>()))
            .Callback<string, ApiQuery?>((_, query) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.HistoryComparison(OrganisationTypes.School, urn, dimension, phase, financeType, excludeCentralServices);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json?.School as ExpenditureHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("urn", "dimension", null, null, null, "?dimension=dimension")]
    [InlineData("urn", "dimension", null, null, true, "?dimension=dimension")]
    public async Task ShouldGetExpenditureHistoryComparatorSetAverageFromApiForSchool(string urn, string dimension, string? phase, string? financeType, bool? excludeCentralServices, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<ExpenditureHistory>();
        var actualQuery = string.Empty;

        var school = _fixture.Build<School>()
            .With(s => s.URN, urn)
            .Create();
        _establishmentApi
            .Setup(e => e.GetSchool(urn))
            .ReturnsAsync(ApiResult.Ok(school));

        _expenditureApi
            .Setup(e => e.SchoolHistoryComparatorSetAverage(urn, It.IsAny<ApiQuery?>()))
            .Callback<string, ApiQuery?>((_, query) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.HistoryComparison(OrganisationTypes.School, urn, dimension, phase, financeType, excludeCentralServices);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json?.ComparatorSetAverage as ExpenditureHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("urn", "dimension", null, null, null, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", null, null, true, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", "financeType", null, null, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", null, "phase", null, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", "financeType", "phase", null, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=financeType&phase=phase")]
    public async Task ShouldGetExpenditureHistoryNationalAverageFromApiForSchool(string urn, string dimension, string? financeType, string? phase, bool? excludeCentralServices, string schoolFinanceType, string schoolOverallPhase, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<ExpenditureHistory>();
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

        _expenditureApi
            .Setup(e => e.SchoolHistoryNationalAverage(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(query =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.HistoryComparison(OrganisationTypes.School, urn, dimension, phase, financeType, excludeCentralServices);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json?.NationalAverage as ExpenditureHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }
}