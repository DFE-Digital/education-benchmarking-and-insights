using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Moq;
using Web.App;
using Web.App.Domain;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenSchoolPerformanceDataServiceIsCalled
{
    private const string Urn = "123456";
    private readonly Mock<IFeatureManager> _featureManager;
    private readonly Mock<IProgressBandingsService> _progressBandingsService;
    private readonly Mock<IComparatorSetRankingService> _attainment8RankingService;
    private readonly Mock<ILogger<SchoolPerformanceDataService>> _logger;
    private readonly SchoolPerformanceDataService _service;

    public WhenSchoolPerformanceDataServiceIsCalled()
    {
        _featureManager = new Mock<IFeatureManager>();
        _progressBandingsService = new Mock<IProgressBandingsService>();
        _attainment8RankingService = new Mock<IComparatorSetRankingService>();
        _logger = new Mock<ILogger<SchoolPerformanceDataService>>();

        _service = new SchoolPerformanceDataService(
            _featureManager.Object,
            _progressBandingsService.Object,
            _attainment8RankingService.Object,
            _logger.Object);
    }

    [Fact]
    public async Task ShouldDisableBothAndLogWarningWhenBothFeatureFlagsAreEnabled()
    {
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.KS4ProgressBanding)).ReturnsAsync(true);
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.Attainment8)).ReturnsAsync(true);

        var actual = await _service.GetPerformanceDataAsync(Urn, ["111111"], ["222222"]);

        Assert.False(actual.ShowKs4Progress);
        Assert.False(actual.ShowAttainment8);
        Assert.Null(actual.Bandings);
        Assert.Empty(actual.TopRankedBuilding);
        Assert.Empty(actual.TopRankedPupil);

        _progressBandingsService.Verify(p => p.GetKS4ProgressBandings(It.IsAny<string[]>()), Times.Never);
        _attainment8RankingService.Verify(a => a.GetTopRankedAttainment8Async(It.IsAny<string>(), It.IsAny<string[]>()), Times.Never);
        _logger.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("An error detected in feature management")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task ShouldGetBandingsWithDistinctUnionOfUrnsWhenKs4ProgressEnabled()
    {
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.KS4ProgressBanding)).ReturnsAsync(true);
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.Attainment8)).ReturnsAsync(false);

        var expectedBandings = new KS4ProgressBandings(new Dictionary<string, string?>
        {
            { "111111", "Well above average" }
        });

        _progressBandingsService
            .Setup(p => p.GetKS4ProgressBandings(It.Is<string[]>(urns =>
                urns.Length == 3 &&
                ((IEnumerable<string>)urns).Contains("111111") &&
                ((IEnumerable<string>)urns).Contains("222222") &&
                ((IEnumerable<string>)urns).Contains("333333"))))
            .ReturnsAsync(expectedBandings);

        var actual = await _service.GetPerformanceDataAsync(
            Urn,
            ["111111", "222222"],
            ["222222", "333333"]);

        Assert.True(actual.ShowKs4Progress);
        Assert.False(actual.ShowAttainment8);
        Assert.Same(expectedBandings, actual.Bandings);
        Assert.Empty(actual.TopRankedBuilding);
        Assert.Empty(actual.TopRankedPupil);

        _attainment8RankingService.Verify(a => a.GetTopRankedAttainment8Async(It.IsAny<string>(), It.IsAny<string[]>()), Times.Never);
    }

    [Fact]
    public async Task ShouldGetTopRankedRankingsWhenAttainment8Enabled()
    {
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.KS4ProgressBanding)).ReturnsAsync(false);
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.Attainment8)).ReturnsAsync(true);

        string[] buildingUrns = ["111111", "222222"];
        string[] pupilUrns = ["333333", "444444"];
        string[] expectedBuildingTop = ["111111"];
        string[] expectedPupilTop = ["333333"];

        _attainment8RankingService
            .Setup(a => a.GetTopRankedAttainment8Async(Urn, buildingUrns))
            .ReturnsAsync(expectedBuildingTop);

        _attainment8RankingService
            .Setup(a => a.GetTopRankedAttainment8Async(Urn, pupilUrns))
            .ReturnsAsync(expectedPupilTop);

        var actual = await _service.GetPerformanceDataAsync(Urn, buildingUrns, pupilUrns);

        Assert.False(actual.ShowKs4Progress);
        Assert.True(actual.ShowAttainment8);
        Assert.Null(actual.Bandings);
        Assert.Equal(expectedBuildingTop, actual.TopRankedBuilding);
        Assert.Equal(expectedPupilTop, actual.TopRankedPupil);

        _progressBandingsService.Verify(p => p.GetKS4ProgressBandings(It.IsAny<string[]>()), Times.Never);
    }

    [Fact]
    public async Task ShouldReturnDefaultsWhenNoFeatureFlagsEnabled()
    {
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.KS4ProgressBanding)).ReturnsAsync(false);
        _featureManager.Setup(f => f.IsEnabledAsync(FeatureFlags.Attainment8)).ReturnsAsync(false);

        var actual = await _service.GetPerformanceDataAsync(Urn, ["111111"], ["222222"]);

        Assert.False(actual.ShowKs4Progress);
        Assert.False(actual.ShowAttainment8);
        Assert.Null(actual.Bandings);
        Assert.Empty(actual.TopRankedBuilding);
        Assert.Empty(actual.TopRankedPupil);

        _progressBandingsService.Verify(p => p.GetKS4ProgressBandings(It.IsAny<string[]>()), Times.Never);
        _attainment8RankingService.Verify(a => a.GetTopRankedAttainment8Async(It.IsAny<string>(), It.IsAny<string[]>()), Times.Never);
    }

    [Fact]
    public void ShouldReturnEmptyArrayWhenMergingNullOrEmptyExpenditures()
    {
        var actualFromNull = _service.Merge(null, null, []);
        var actualFromEmpty = _service.Merge([], null, []);

        Assert.Empty(actualFromNull);
        Assert.Empty(actualFromEmpty);
    }

    [Fact]
    public void ShouldMergeBandingAndAttainmentPerformanceOntoExpenditure()
    {
        var expenditures = new[]
        {
            new SchoolExpenditure { URN = "111111" },
            new SchoolExpenditure { URN = "222222" },
            new SchoolExpenditure { URN = "333333" }
        };

        var bandings = new KS4ProgressBandings(new Dictionary<string, string?>
        {
            { "111111", "Well above average" },
            { "222222", "Above average" }
        });

        var actual = _service.Merge(expenditures, bandings, ["222222"]);

        Assert.Equal(3, actual.Length);

        // 111111: has banding, not top ranked
        Assert.Equal("111111", actual[0].URN);
        Assert.Equal("Well above average", actual[0].ProgressBanding);
        Assert.False(actual[0].TopRankedAttainment8);

        // 222222: has banding, top ranked
        Assert.Equal("222222", actual[1].URN);
        Assert.Equal("Above average", actual[1].ProgressBanding);
        Assert.True(actual[1].TopRankedAttainment8);

        // 333333: no banding, not top ranked
        Assert.Equal("333333", actual[2].URN);
        Assert.Null(actual[2].ProgressBanding);
        Assert.False(actual[2].TopRankedAttainment8);
    }
}
