using Moq;
using StackExchange.Redis;
using Xunit;
namespace Platform.Tests.Cache;

public class WhenRedisDistributedCacheGetsString : RedisDistributedCacheTestBase
{
    [Theory]
    [InlineData("key", "value", "value")]
    [InlineData("key", null, null)]
    public async Task ShouldReturnExpectedValueFromCache(string key, string? value, string? expectedValue)
    {
        Database
            .Setup(d => d.StringGetAsync(key, CommandFlags.None))
            .ReturnsAsync(value)
            .Verifiable(Times.Once);

        var actual = await Cache.GetStringAsync(key);

        Database.Verify();
        Assert.Equal(expectedValue, actual);
    }
    
    [Fact]
    public async Task ShouldThrowExceptionIfRedisUnavailable()
    {
        const string key = nameof(key);
        Database
            .Setup(d => d.StringGetAsync(key, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.GetStringAsync(key));

        Database.Verify();
        Assert.NotNull(actual);
    }
}