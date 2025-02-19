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
    [InlineData(new[] { "key1", "key2" }, 123)]
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
            .Setup(d => d.KeyDeleteAsync(new RedisKey[] { key }, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.DeleteAsync([key]));

        Database.Verify();
        Assert.NotNull(actual);
    }

    [Fact]
    public async Task ShouldEvaluateScriptIfKeysArePresent()
    {
        const string pattern = "key:*";

        LuaScript? actualScript = null;
        object? actualParam = null;
        Database
            .Setup(d => d.ScriptEvaluateAsync(It.IsAny<LuaScript>(), It.IsAny<object?>(), CommandFlags.None))
            .Callback<LuaScript, object?, CommandFlags>((script, param, _) =>
            {
                actualScript = script;
                actualParam = param;
            });

        await Cache.DeleteAsync(pattern);

        const string expectedScript = "return redis.call('DEL', unpack(redis.call('SCAN', 0, 'COUNT', 1000, 'MATCH', @pattern)[2]))";
        Assert.Equal(expectedScript, actualScript?.OriginalScript);
        Assert.Equivalent(new
        {
            pattern
        }, actualParam);
    }

    [Fact]
    public async Task ShouldNotThrowExceptionWhenEvaluateScriptIfKeysAreNotPresent()
    {
        const string pattern = "key:*";

        Database
            .Setup(d => d.ScriptEvaluateAsync(It.IsAny<LuaScript>(), It.IsAny<object?>(), CommandFlags.None))
            .Throws(() => new RedisServerException("Wrong number of args calling Redis command From Lua script"));

        await Cache.DeleteAsync(pattern);
    }

    [Fact]
    public async Task ShouldThrowExceptionIfPatternInvalid()
    {
        const string pattern = "invalid_key:*";

        var actual = await Assert.ThrowsAsync<ArgumentException>(() => Cache.DeleteAsync(pattern));
        Assert.NotNull(actual);
    }
}