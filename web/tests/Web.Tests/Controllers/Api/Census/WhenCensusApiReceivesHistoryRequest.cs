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

public class WhenCensusApiReceivesHistoryRequest
{
    private readonly CensusProxyController _api;
    private readonly Mock<ICensusApi> _censusApi = new();
    private readonly Mock<IEstablishmentApi> _establishmentApi = new();
    private readonly NullLogger<CensusProxyController> _logger = new();
    private readonly Mock<ISchoolComparatorSetService> _schoolComparatorSetService = new();
    private readonly Mock<IUserDataService> _userDataService = new();

    public WhenCensusApiReceivesHistoryRequest()
    {
        _api = new CensusProxyController(_logger, _establishmentApi.Object, _censusApi.Object, _schoolComparatorSetService.Object, _userDataService.Object);
    }

    [Theory]
    [InlineData("urn", "dimension", "?dimension=dimension")]
    public async Task ShouldGetCensusHistoryFromApiForSchool(string urn, string dimension, string expectedQuery)
    {
        // arrange
        var results = new CensusHistoryRows();
        var actualQuery = string.Empty;

        _censusApi
            .Setup(e => e.SchoolHistory(urn, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .Callback<string, ApiQuery?, CancellationToken>((_, query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var actual = await _api.History(urn, dimension);

        // assert
        var json = Assert.IsType<JsonResult>(actual).Value;
        Assert.Equivalent(results, json);
        Assert.Equal(expectedQuery, actualQuery);
    }
}