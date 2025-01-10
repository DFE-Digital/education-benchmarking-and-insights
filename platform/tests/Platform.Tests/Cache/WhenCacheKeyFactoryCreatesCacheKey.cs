using System.Diagnostics.CodeAnalysis;
using Platform.Cache;
using Xunit;
namespace Platform.Tests.Cache;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class WhenCacheKeyFactoryCreatesCacheKey
{
    private readonly CacheKeyFactory _factory = new();

    [Theory]
    [InlineData("overall phase", "finance type", "dimension", "census:history:national-average:overall.phase|finance.type|dimension")]
    public void ShouldReturnKeyForCensusHistory(string overallPhase, string financeType, string dimension, string expected)
    {
        var actual = _factory.CreateCensusHistoryCacheKey(overallPhase, financeType, dimension);
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