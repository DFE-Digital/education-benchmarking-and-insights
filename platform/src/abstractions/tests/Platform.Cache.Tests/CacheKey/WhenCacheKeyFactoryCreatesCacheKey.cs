using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Platform.Cache.Tests.CacheKey;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class WhenCacheKeyFactoryCreatesCacheKey
{
    private readonly CacheKeyFactory _factory = new();

    [Theory]
    [InlineData(2000, "overall phase", "finance type", "dimension", "2000:census:history:national-average:overall.phase|finance.type|dimension")]
    public void ShouldReturnKeyForCensusHistoryNationalAverage(int endYear, string overallPhase, string financeType, string dimension, string expected)
    {
        var actual = _factory.CreateCensusHistoryNationalAverageCacheKey(endYear, overallPhase, financeType, dimension);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2000, "overall phase", "finance type", "dimension", "2000:expenditure:history:national-average:overall.phase|finance.type|dimension")]
    public void ShouldReturnKeyForExpenditureHistoryNationalAverage(int endYear, string overallPhase, string financeType, string dimension, string expected)
    {
        var actual = _factory.CreateExpenditureHistoryNationalAverageCacheKey(endYear, overallPhase, financeType, dimension);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(new[]
    {
        "type",
        "id",
        "field"
    }, "type:id:field")]
    public void ShouldReturnKeyForParts(string[] parts, string expected)
    {
        var actual = _factory.CreateCacheKey(parts);
        Assert.Equal(expected, actual);
    }
}