using Moq;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;
namespace Platform.Cache.Tests.Cache;

public class WhenRedisDistributedCacheSetsString(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    [Theory]
    [InlineData("key", "value")]
    public async Task ShouldSetValueInCache(string key, string value)
    {
        Database
            .Setup(d => d.StringSetAsync(key, value, null, false, StackExchange.Redis.When.Always, CommandFlags.None))
            .ReturnsAsync(true)
            .Verifiable(Times.Once);

        var actual = await Cache.SetStringAsync(key, value);

        Database.Verify();
        Assert.True(actual);
    }

    [Fact]
    public async Task ShouldThrowExceptionIfRedisUnavailable()
    {
        const string key = nameof(key);
        const string value = nameof(value);
        Database
            .Setup(d => d.StringSetAsync(key, value, null, false, StackExchange.Redis.When.Always, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.SetStringAsync(key, value));

        Database.Verify();
        Assert.NotNull(actual);
    }
}