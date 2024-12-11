using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App;
using Web.App.Controllers.Api;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Services;
using Xunit;
namespace Web.Tests.Controllers.Api.Census;

public class WhenCensusApiReceivesQueryRequest
{
    private readonly CensusProxyController _api;
    private readonly Mock<ICensusApi> _censusApi = new();
    private readonly Mock<IEstablishmentApi> _establishmentApi = new();
    private readonly NullLogger<CensusProxyController> _logger = new();
    private readonly Mock<ISchoolComparatorSetService> _schoolComparatorSetService = new();
    private readonly Mock<IUserDataService> _userDataService = new();

    public WhenCensusApiReceivesQueryRequest()
    {
        _api = new CensusProxyController(_logger, _establishmentApi.Object, _censusApi.Object, _schoolComparatorSetService.Object, _userDataService.Object);
    }

    [Theory]
    [InlineData("companyNumber", "category", "dimension", "phase", "?category=category&dimension=dimension&phase=phase&companyNumber=companyNumber")]
    public async Task ShouldQueryCensusApiForTrust(string companyNumber, string category, string dimension, string phase, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<App.Domain.Census>();
        var actualQuery = string.Empty;

        _censusApi
            .Setup(e => e.Query(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(c =>
            {
                actualQuery = c?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.Query(OrganisationTypes.Trust, companyNumber, category, dimension, phase, null);

        // assert
        var json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("laCode", "category", "dimension", "phase", "?category=category&dimension=dimension&phase=phase&laCode=laCode")]
    public async Task ShouldQueryCensusApiForLocalAuthority(string laCode, string category, string dimension, string phase, string expectedQuery)
    {
        // arrange
        var results = Array.Empty<App.Domain.Census>();
        var actualQuery = string.Empty;

        _censusApi
            .Setup(e => e.Query(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(c =>
            {
                actualQuery = c?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.Query(OrganisationTypes.LocalAuthority, laCode, category, dimension, phase, null);

        // assert
        var json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equal(results, json);
        Assert.Equal(expectedQuery, actualQuery);
    }
}