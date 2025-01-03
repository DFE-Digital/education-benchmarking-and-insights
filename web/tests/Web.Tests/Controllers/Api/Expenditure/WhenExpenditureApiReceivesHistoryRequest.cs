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
    [InlineData("urn", "dimension", null, "?dimension=dimension")]
    [InlineData("urn", "dimension", true, "?dimension=dimension&excludeCentralServices=true")]
    public async Task ShouldGetExpenditureHistoryFromApiForSchool(string urn, string dimension, bool? excludeCentralServices, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<ExpenditureHistory>();
        var actualQuery = string.Empty;

        _expenditureApi
            .Setup(e => e.SchoolHistory(urn, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .Callback<string, ApiQuery?, CancellationToken>((_, query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.History(OrganisationTypes.School, urn, dimension, excludeCentralServices);

        // assert
        var json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("companyNumber", "dimension", null, "?dimension=dimension")]
    [InlineData("companyNumber", "dimension", true, "?dimension=dimension&excludeCentralServices=true")]
    public async Task ShouldGetExpenditureHistoryFromApiForTrust(string companyNumber, string dimension, bool? excludeCentralServices, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<ExpenditureHistory>();
        var actualQuery = string.Empty;

        _expenditureApi
            .Setup(e => e.TrustHistory(companyNumber, It.IsAny<ApiQuery?>()))
            .Callback<string, ApiQuery?>((_, query) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.History(OrganisationTypes.Trust, companyNumber, dimension, excludeCentralServices);

        // assert
        var json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json);
        Assert.Equal(expectedQuery, actualQuery);
    }
}