using Moq;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;
// ReSharper disable NotAccessedPositionalProperty.Global
namespace Platform.Cache.Tests.Cache;

public class WhenRedisDistributedCacheSetsObject(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    public static TheoryData<ShouldSetValueInCacheTestData> ShouldReturnObjectFromStringTestDataItems =>
    [
        new("key", new TestObject("value"), "IQAAAANEYXRhABYAAAACVmFsdWUABgAAAHZhbHVlAAAA", CacheValueEncoding.Bson),
        new("key", new TestObject("value"), "{\"Data\":{\"Value\":\"value\"}}", CacheValueEncoding.Json)
    ];

    public static TheoryData<ShouldSetNumericValueInCacheTestData> ShouldReturnNumericObjectFromStringTestDataItems =>
    [
        new("key", new TestNumericObject(1.2830559767184912978m), "HwAAAANEYXRhABQAAAABVmFsdWUAlRcvtGWH9D8AAA==", CacheValueEncoding.Bson),
        new("key", new TestNumericObject(1.28305597671849m), "HwAAAANEYXRhABQAAAABVmFsdWUAjxcvtGWH9D8AAA==", CacheValueEncoding.Bson),
        new("key", new TestNumericObject(1.2830559767184912978m), "{\"Data\":{\"Value\":1.2830559767184912978}}", CacheValueEncoding.Json),
        new("key", new TestNumericObject(1.28305597671849m), "{\"Data\":{\"Value\":1.28305597671849}}", CacheValueEncoding.Json)
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromStringTestDataItems), MemberType = typeof(WhenRedisDistributedCacheSetsObject))]
    public async Task ShouldReturnExpectedValueFromCache(ShouldSetValueInCacheTestData input)
    {
        var actualEncoded = string.Empty;
        Database
            .Setup(d => d.StringSetAsync(input.Key, It.IsAny<RedisValue>(), null, false, StackExchange.Redis.When.Always, CommandFlags.None))
            .ReturnsAsync(true)
            .Callback<RedisKey, RedisValue, TimeSpan?, bool, StackExchange.Redis.When, CommandFlags>((_, value, _, _, _, _) =>
            {
                actualEncoded = value;
            })
            .Verifiable(Times.Once);

        var actual = await Cache.SetAsync(input.Key, input.Value, When.Always, input.CacheValueEncoding);

        Database.Verify();
        Assert.Equal(input.ExpectedEncoded, actualEncoded);
        Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnNumericObjectFromStringTestDataItems), MemberType = typeof(WhenRedisDistributedCacheSetsObject))]
    public async Task ShouldReturnExpectedNumericValueFromCache(ShouldSetNumericValueInCacheTestData input)
    {
        var actualEncoded = string.Empty;
        Database
            .Setup(d => d.StringSetAsync(input.Key, It.IsAny<RedisValue>(), null, false, StackExchange.Redis.When.Always, CommandFlags.None))
            .ReturnsAsync(true)
            .Callback<RedisKey, RedisValue, TimeSpan?, bool, StackExchange.Redis.When, CommandFlags>((_, value, _, _, _, _) =>
            {
                actualEncoded = value;
            })
            .Verifiable(Times.Once);

        var actual = await Cache.SetAsync(input.Key, input.Value, When.Always, input.CacheValueEncoding);

        Database.Verify();
        Assert.Equal(input.ExpectedEncoded, actualEncoded);
        Assert.True(actual);
    }

    [Fact]
    public async Task ShouldThrowExceptionIfRedisUnavailable()
    {
        const string key = nameof(key);
        var value = new TestObject("value");
        Database
            .Setup(d => d.StringSetAsync(key, It.IsAny<RedisValue>(), null, false, StackExchange.Redis.When.Always, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.SetAsync(key, value));

        Database.Verify();
        Assert.NotNull(actual);
    }

    public record ShouldSetValueInCacheTestData(string Key, TestObject Value, string ExpectedEncoded, CacheValueEncoding CacheValueEncoding);

    public record ShouldSetNumericValueInCacheTestData(string Key, TestNumericObject Value, string ExpectedEncoded, CacheValueEncoding CacheValueEncoding);

    public record TestObject(string Value);

    public record TestNumericObject(decimal Value);
}