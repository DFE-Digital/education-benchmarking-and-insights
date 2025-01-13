using System.Diagnostics.CodeAnalysis;
using Moq;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;
namespace Platform.Cache.Tests.Cache;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class WhenRedisDistributedCacheDeletes(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    [Theory]
    [InlineData(new[]
    {
        "key1",
        "key2"
    }, 123)]
    public async Task ShouldReturnExpectedValueFromCache(string[] keys, long count)
    {
        RedisKey[] actualKeys = [];
        Database
            .Setup(d => d.KeyDeleteAsync(It.IsAny<RedisKey[]>(), CommandFlags.None))
            .Callback<RedisKey[], CommandFlags>((k, _) =>
            {
                actualKeys = k;
            })
            .ReturnsAsync(count)
            .Verifiable(Times.Once);

        var actual = await Cache.DeleteAsync(keys);

        Database.Verify();
        Assert.Equal(count, actual);
        Assert.Equal(keys.Select(k => new RedisKey(k)).ToArray(), actualKeys);
    }

    [Fact]
    public async Task ShouldThrowExceptionIfRedisUnavailable()
    {
        const string key = nameof(key);
        Database
            .Setup(d => d.KeyDeleteAsync(new RedisKey[]
            {
                key
            }, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.DeleteAsync([key]));

        Database.Verify();
        Assert.NotNull(actual);
    }

    [Theory]
    [InlineData("key:*", 123, "for _,k in ipairs(redis.call('keys','key:*')) do redis.call('del',k) end")]
    public async Task ShouldReturnExpectedValueFromCacheUsingPattern(string pattern, long count, string expectedScript)
    {
        var actualScript = string.Empty;
        Database
            .Setup(d => d.ScriptEvaluateAsync(It.IsAny<string>(), It.IsAny<RedisKey[]>(), It.IsAny<RedisValue[]>(), CommandFlags.None))
            .Callback<string, RedisKey[], RedisValue[], CommandFlags>((script, _, _, _) =>
            {
                actualScript = script;
            })
            .ReturnsAsync(RedisResult.Create(count))
            .Verifiable(Times.Once);

        await Cache.DeleteAsync(pattern);

        Database.Verify();
        Assert.Equal(expectedScript, actualScript);
    }

    [Theory]
    [InlineData("invalid_key:*")]
    public async Task ShouldThrowExceptionIfPatternInvalid(string pattern)
    {
        var actual = await Assert.ThrowsAsync<ArgumentException>(() => Cache.DeleteAsync(pattern));
        Assert.NotNull(actual);
    }
}