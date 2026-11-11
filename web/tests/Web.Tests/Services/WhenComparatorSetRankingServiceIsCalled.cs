using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenComparatorSetRankingServiceIsCalled
{
    private const string CurrentUrn = "123456";
    private readonly Mock<ISchoolApi> _schoolApi;
    private readonly ComparatorSetRankingService _service;

    public WhenComparatorSetRankingServiceIsCalled()
    {
        _schoolApi = new Mock<ISchoolApi>();
        _service = new ComparatorSetRankingService(_schoolApi.Object);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ShouldReturnEmptyListWhenCountIsZeroOrNegative(int count)
    {
        var actual = await _service.GetTopRankedAttainment8Async(
            CurrentUrn,
            ["111111", "222222"],
            count);

        Assert.Empty(actual);
        _schoolApi.Verify(a => a.QueryAsync(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ShouldReturnEmptyListWhenNoCandidateUrnsAvailable()
    {
        var actual = await _service.GetTopRankedAttainment8Async(
            CurrentUrn,
            [CurrentUrn, CurrentUrn]);

        Assert.Empty(actual);
        _schoolApi.Verify(a => a.QueryAsync(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ShouldQuerySchoolApiExcludingCurrentUrnAndDuplicates()
    {
        ApiQuery? capturedQuery = null;

        _schoolApi
            .Setup(a => a.QueryAsync(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>()))
            .Callback<ApiQuery, CancellationToken>((query, _) => capturedQuery = query)
            .ReturnsAsync(ApiResult.Ok(Array.Empty<SchoolCharacteristic>()));

        await _service.GetTopRankedAttainment8Async(
            CurrentUrn,
            [CurrentUrn, "111111", "222222", "111111", null]);

        Assert.NotNull(capturedQuery);
        var urnValues = capturedQuery
            .Where(p => p.Key == "urns")
            .Select(p => p.Value)
            .ToArray();

        Assert.Equal(2, urnValues.Length);
        Assert.Contains("111111", urnValues);
        Assert.Contains("222222", urnValues);
        Assert.DoesNotContain(CurrentUrn, urnValues);
    }

    [Fact]
    public async Task ShouldRankByAttainment8DescendingThenUrnAscendingAndTakeSpecifiedCount()
    {
        var characteristics = new[]
        {
            new SchoolCharacteristic { URN = "111111", Attainment8 = 40.0m },
            new SchoolCharacteristic { URN = "222222", Attainment8 = 90.0m },
            new SchoolCharacteristic { URN = "333333", Attainment8 = 75.0m },
            new SchoolCharacteristic { URN = "444444", Attainment8 = 90.0m },
            new SchoolCharacteristic { URN = "555555", Attainment8 = 60.0m },
            new SchoolCharacteristic { URN = "666666", Attainment8 = 85.0m },
            new SchoolCharacteristic { URN = "777777", Attainment8 = 20.0m }
        };

        _schoolApi
            .Setup(a => a.QueryAsync(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(characteristics));

        var actual = await _service.GetTopRankedAttainment8Async(
            CurrentUrn,
            ["111111", "222222", "333333", "444444", "555555", "666666", "777777"],
            count: 5);

        Assert.Equal(5, actual.Count);
        Assert.Equal(["222222", "444444", "666666", "333333", "555555"], actual);
    }

    [Fact]
    public async Task ShouldExcludeCurrentUrnAndNullAttainment8ValuesFromRanking()
    {
        var characteristics = new[]
        {
            new SchoolCharacteristic { URN = CurrentUrn, Attainment8 = 99.0m },
            new SchoolCharacteristic { URN = "111111", Attainment8 = null },
            new SchoolCharacteristic { URN = "222222", Attainment8 = 50.0m },
            new SchoolCharacteristic { URN = "333333", Attainment8 = 70.0m }
        };

        _schoolApi
            .Setup(a => a.QueryAsync(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(characteristics));

        var actual = await _service.GetTopRankedAttainment8Async(
            CurrentUrn,
            ["111111", "222222", "333333"]);

        Assert.Equal(2, actual.Count);
        Assert.Equal(["333333", "222222"], actual);
        Assert.DoesNotContain(CurrentUrn, actual);
        Assert.DoesNotContain("111111", actual);
    }
}
