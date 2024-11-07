using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Services;
using Xunit;
namespace Web.Tests.Services;

public class WhenSchoolBenchmarkingReportCardsServiceIsCalled
{
    private const string? URN = nameof(URN);
    private readonly Mock<IExpenditureApi> _expenditureApi;
    private readonly Mock<IMetricRagRatingApi> _metricRagRatingApi;
    private readonly SchoolBenchmarkingReportCardsService _sut;

    public WhenSchoolBenchmarkingReportCardsServiceIsCalled()
    {
        _expenditureApi = new Mock<IExpenditureApi>();
        _metricRagRatingApi = new Mock<IMetricRagRatingApi>();
        _sut = new SchoolBenchmarkingReportCardsService(
            _expenditureApi.Object,
            _metricRagRatingApi.Object,
            new NullLogger<SchoolBenchmarkingReportCardsService>());
    }

    [Fact]
    public async Task CanShowBrcForSchoolShouldReturnFalseIfNonLeadFederatedSchool()
    {
        var school = new School
        {
            URN = URN,
            FederationLeadURN = "FederationLeadURN"
        };

        var result = await _sut.CanShowBrcForSchool(school);
        Assert.False(result);
    }

    [Fact]
    public async Task CanShowBrcForSchoolShouldReturnFalseIfMissingExpenditureData()
    {
        var school = new School
        {
            URN = URN
        };

        _expenditureApi.Setup(e => e.School(URN, null)).ReturnsAsync(ApiResult.NotFound);

        var result = await _sut.CanShowBrcForSchool(school);
        Assert.False(result);
    }

    [Fact]
    public async Task CanShowBrcForSchoolShouldReturnFalseIfExpenditureCoversPartYear()
    {
        var school = new School
        {
            URN = URN
        };

        var expenditure = new SchoolExpenditure
        {
            PeriodCoveredByReturn = 10
        };

        _expenditureApi.Setup(e => e.School(URN, null)).ReturnsAsync(ApiResult.Ok(expenditure));

        var result = await _sut.CanShowBrcForSchool(school);
        Assert.False(result);
    }

    [Fact]
    public async Task CanShowBrcForSchoolShouldReturnFalseIfMissingRatings()
    {
        var school = new School
        {
            URN = URN
        };

        var expenditure = new SchoolExpenditure
        {
            PeriodCoveredByReturn = 12
        };

        _expenditureApi.Setup(e => e.School(URN, null)).ReturnsAsync(ApiResult.Ok(expenditure));
        _metricRagRatingApi.Setup(e => e.GetDefaultAsync(It.IsAny<ApiQuery>())).ReturnsAsync(ApiResult.NotFound());

        var result = await _sut.CanShowBrcForSchool(school);
        Assert.False(result);
    }

    [Fact]
    public async Task CanShowBrcForSchoolShouldReturnTrueIfRatingsValid()
    {
        var school = new School
        {
            URN = URN
        };

        var expenditure = new SchoolExpenditure
        {
            PeriodCoveredByReturn = 12
        };

        var ragRatings = new List<RagRating>
        {
            new()
            {
                Category = Category.AdministrativeSupplies,
                RAG = "red"
            }
        };

        _expenditureApi.Setup(e => e.School(URN, null)).ReturnsAsync(ApiResult.Ok(expenditure));
        _metricRagRatingApi.Setup(e => e.GetDefaultAsync(It.IsAny<ApiQuery>())).ReturnsAsync(ApiResult.Ok(ragRatings));

        var result = await _sut.CanShowBrcForSchool(school);
        Assert.True(result);
    }
}