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
    [InlineData("urn", "dimension", null, null, "?dimension=dimension")]
    public async Task ShouldGetExpenditureHistoryFromApiForSchool(string urn, string dimension, string? phase, string? financeType, string expectedQuery)
    {
        // arrange
        var results = new ExpenditureHistoryRows();
        var actualQuery = string.Empty;
        var cancellationToken = CancellationToken.None;

        var school = _fixture.Build<School>()
            .With(s => s.URN, urn)
            .Create();
        _establishmentApi
            .Setup(e => e.GetSchool(urn, cancellationToken))
            .ReturnsAsync(ApiResult.Ok(school));

        SetupExpenditureHistory(urn, results, q => actualQuery = q, cancellationToken);

        // act
        var actual = await _api.HistoryComparison(OrganisationTypes.School, urn, dimension, phase, financeType, cancellationToken);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results.Rows, json?.School as ExpenditureHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("urn", "dimension", null, null, "?dimension=dimension")]
    public async Task ShouldGetExpenditureHistoryComparatorSetAverageFromApiForSchool(string urn, string dimension, string? phase, string? financeType, string expectedQuery)
    {
        // arrange
        var results = new ExpenditureHistoryRows();
        var actualQuery = string.Empty;
        var cancellationToken = CancellationToken.None;

        var school = _fixture.Build<School>()
            .With(s => s.URN, urn)
            .Create();

        _establishmentApi
            .Setup(e => e.GetSchool(urn, cancellationToken))
            .ReturnsAsync(ApiResult.Ok(school));

        SetupExpenditureHistory(urn, cancellationToken: cancellationToken);
        _expenditureApi
            .Setup(e => e.SchoolHistoryComparatorSetAverage(urn, It.IsAny<ApiQuery?>(), cancellationToken))
            .Callback<string, ApiQuery?, CancellationToken>((_, query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.HistoryComparison(OrganisationTypes.School, urn, dimension, phase, financeType, cancellationToken);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results.Rows, json?.ComparatorSetAverage as ExpenditureHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("urn", "dimension", null, null, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", "financeType", null, "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", null, "phase", "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=schoolFinanceType&phase=schoolOverallPhase")]
    [InlineData("urn", "dimension", "financeType", "phase", "schoolFinanceType", "schoolOverallPhase", "?dimension=dimension&financeType=financeType&phase=phase")]
    public async Task ShouldGetExpenditureHistoryNationalAverageFromApiForSchool(string urn, string dimension, string? financeType, string? phase, string schoolFinanceType, string schoolOverallPhase, string expectedQuery)
    {
        // arrange
        var results = new ExpenditureHistoryRows();
        var actualQuery = string.Empty;
        var cancellationToken = CancellationToken.None;

        var school = new School
        {
            URN = urn,
            FinanceType = schoolFinanceType,
            OverallPhase = schoolOverallPhase
        };

        _establishmentApi
            .Setup(e => e.GetSchool(urn, cancellationToken))
            .ReturnsAsync(ApiResult.Ok(school));

        SetupExpenditureHistory(urn, cancellationToken: cancellationToken);
        _expenditureApi
            .Setup(e => e.SchoolHistoryNationalAverage(It.IsAny<ApiQuery?>(), cancellationToken))
            .Callback<ApiQuery?, CancellationToken>((query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.HistoryComparison(OrganisationTypes.School, urn, dimension, phase, financeType, cancellationToken);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results.Rows, json?.NationalAverage as ExpenditureHistory[]);
        Assert.Equal(expectedQuery, actualQuery);
    }

    private void SetupExpenditureHistory(string urn, ExpenditureHistoryRows? results = null, Action<string?>? callback = null, CancellationToken cancellationToken = default)
    {
        _expenditureApi
            .Setup(e => e.SchoolHistory(urn, It.IsAny<ApiQuery?>(), cancellationToken))
            .Callback<string, ApiQuery?, CancellationToken>((_, query, _) =>
            {
                callback?.Invoke(query?.ToQueryString());
            })
            .ReturnsAsync(ApiResult.Ok(results ?? new ExpenditureHistoryRows()));
    }
}