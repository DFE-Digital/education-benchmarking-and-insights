using Moq;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable NotAccessedPositionalProperty.Global
namespace Platform.Cache.Tests.Cache;

public class WhenRedisDistributedCacheSetsObjects(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    public static TheoryData<ShouldSetValuesInCacheTestData> ShouldReturnObjectFromStringTestDataItems =>
    [
        new([("key", new TestObject("value"), "IQAAAANEYXRhABYAAAACVmFsdWUABgAAAHZhbHVlAAAA")], CacheValueEncoding.Bson),
        new([("key", new TestObject("value"), "{\"Data\":{\"Value\":\"value\"}}")], CacheValueEncoding.Json)
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromStringTestDataItems), MemberType = typeof(WhenRedisDistributedCacheSetsObjects))]
    public async Task ShouldSetValuesInCache(ShouldSetValuesInCacheTestData input)
    {
        var actualValues = Array.Empty<KeyValuePair<RedisKey, RedisValue>>();
        Database
            .Setup(d => d.StringSetAsync(It.IsAny<KeyValuePair<RedisKey, RedisValue>[]>(), StackExchange.Redis.When.Always, CommandFlags.None))
            .Callback((KeyValuePair<RedisKey, RedisValue>[] v, StackExchange.Redis.When _, CommandFlags _) =>
            {
                actualValues = v;
            })
            .ReturnsAsync(true)
            .Verifiable(Times.Once);

        var values = input.Values
            .Select(v => new KeyValuePair<string, TestObject>(v.Key, v.Value))
            .ToArray();
        var actual = await Cache.SetAsync(values, When.Always, input.CacheValueEncoding);

        var expectedValues = input.Values
            .Select(v => new KeyValuePair<RedisKey, RedisValue>(v.Key, v.ExpectedEncoded))
            .ToArray();

        Database.Verify();
        Assert.True(actual);
        Assert.Equal(expectedValues, actualValues);
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

    public record ShouldSetValuesInCacheTestData((string Key, TestObject Value, string ExpectedEncoded)[] Values, CacheValueEncoding CacheValueEncoding);

    public record TestObject(string Value);
}