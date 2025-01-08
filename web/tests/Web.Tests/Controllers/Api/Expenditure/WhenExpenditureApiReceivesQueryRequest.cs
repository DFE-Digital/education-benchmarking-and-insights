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

public class WhenExpenditureApiReceivesQueryRequest
{
    private readonly ExpenditureProxyController _api;
    private readonly Mock<IEstablishmentApi> _establishmentApi = new();
    private readonly Mock<IExpenditureApi> _expenditureApi = new();
    private readonly NullLogger<ExpenditureProxyController> _logger = new();
    private readonly Mock<ISchoolComparatorSetService> _schoolComparatorSetService = new();
    private readonly Mock<ITrustComparatorSetService> _trustComparatorSetService = new();
    private readonly Mock<IUserDataService> _userDataService = new();

    public WhenExpenditureApiReceivesQueryRequest()
    {
        _api = new ExpenditureProxyController(_logger, _establishmentApi.Object, _expenditureApi.Object, _schoolComparatorSetService.Object, _trustComparatorSetService.Object, _userDataService.Object);
    }

    [Theory]
    [InlineData("companyNumber", "category", "dimension", "phase", "?category=category&dimension=dimension&phase=phase&companyNumber=companyNumber")]
    public async Task ShouldQueryExpenditureApiForTrust(string companyNumber, string category, string dimension, string phase, string expectedQuery)
    {
        // arrange
        var trust = new Trust
        {
            CompanyNumber = companyNumber
        };

        var results = Array.Empty<SchoolExpenditure>();
        var actualQuery = string.Empty;

        _establishmentApi
            .Setup(e => e.GetTrust(companyNumber))
            .ReturnsAsync(ApiResult.Ok(trust));
        _expenditureApi
            .Setup(e => e.QuerySchools(It.IsAny<ApiQuery?>()))
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
    public async Task ShouldQueryExpenditureApiForLocalAuthority(string laCode, string category, string dimension, string phase, string expectedQuery)
    {
        // arrange
        var la = new LocalAuthority
        {
            Code = laCode
        };

        var results = Array.Empty<SchoolExpenditure>();
        var actualQuery = string.Empty;

        _establishmentApi
            .Setup(e => e.GetLocalAuthority(laCode))
            .ReturnsAsync(ApiResult.Ok(la));
        _expenditureApi
            .Setup(e => e.QuerySchools(It.IsAny<ApiQuery?>()))
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