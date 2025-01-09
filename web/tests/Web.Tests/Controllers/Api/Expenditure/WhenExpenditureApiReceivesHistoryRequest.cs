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

public class WhenExpenditureApiReceivesHistoryRequest
{
    private readonly ExpenditureProxyController _api;
    private readonly Mock<IEstablishmentApi> _establishmentApi = new();
    private readonly Mock<IExpenditureApi> _expenditureApi = new();
    private readonly NullLogger<ExpenditureProxyController> _logger = new();
    private readonly Mock<ISchoolComparatorSetService> _schoolComparatorSetService = new();
    private readonly Mock<ITrustComparatorSetService> _trustComparatorSetService = new();
    private readonly Mock<IUserDataService> _userDataService = new();

    public WhenExpenditureApiReceivesHistoryRequest()
    {
        _api = new ExpenditureProxyController(_logger, _establishmentApi.Object, _expenditureApi.Object, _schoolComparatorSetService.Object, _trustComparatorSetService.Object, _userDataService.Object);
    }

    [Theory]
    [InlineData("urn", "dimension", "?dimension=dimension")]
    public async Task ShouldGetExpenditureHistoryFromApiForSchool(string urn, string dimension, string expectedQuery)
    {
        // arrange
        var results = new ExpenditureHistoryRows();
        var actualQuery = string.Empty;

        _expenditureApi
            .Setup(e => e.SchoolHistory(urn, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .Callback<string, ApiQuery?, CancellationToken>((_, query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.History(OrganisationTypes.School, urn, dimension);

        // assert
        var json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equivalent(results, json);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("companyNumber", "dimension", "?dimension=dimension")]
    public async Task ShouldGetExpenditureHistoryFromApiForTrust(string companyNumber, string dimension, string expectedQuery)
    {
        // arrange
        var results = new ExpenditureHistoryRows();
        var actualQuery = string.Empty;

        _expenditureApi
            .Setup(e => e.TrustHistory(companyNumber, It.IsAny<ApiQuery?>()))
            .Callback<string, ApiQuery?>((_, query) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.History(OrganisationTypes.Trust, companyNumber, dimension);

        // assert
        var json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equivalent(results, json);
        Assert.Equal(expectedQuery, actualQuery);
    }
}