using Moq;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Cache.Tests.Cache;

public class WhenRedisDistributedCacheSetsStrings(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    [Theory]
    [InlineData("key", "value")]
    public async Task ShouldSetValuesInCache(string key, string value)
    {
        Database
            .Setup(d => d.StringSetAsync(new[]
            {
                new KeyValuePair<RedisKey, RedisValue>(key, value)
            }, StackExchange.Redis.When.Always, CommandFlags.None))
            .ReturnsAsync(true)
            .Verifiable(Times.Once);

        var actual = await Cache.SetStringsAsync([new KeyValuePair<string, string>(key, value)]);

        Database.Verify();
        Assert.True(actual);
    }

    [Fact]
    public async Task ShouldThrowExceptionIfRedisUnavailable()
    {
        const string key = nameof(key);
        const string value = nameof(value);
        Database
            .Setup(d => d.StringSetAsync(It.IsAny<KeyValuePair<RedisKey, RedisValue>[]>(), StackExchange.Redis.When.Always, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.SetStringsAsync([new KeyValuePair<string, string>(key, value)]));

        Database.Verify();
        Assert.NotNull(actual);
    }
}