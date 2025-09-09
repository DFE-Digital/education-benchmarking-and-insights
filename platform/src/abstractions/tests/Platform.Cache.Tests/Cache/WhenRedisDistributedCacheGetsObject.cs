using Moq;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable NotAccessedPositionalProperty.Global
namespace Platform.Cache.Tests.Cache;

public class WhenRedisDistributedCacheGetsObject(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    public static TheoryData<ShouldReturnExpectedValueFromCacheTestData> ShouldReturnObjectFromStringTestDataItems =>
    [
        new("key", "IQAAAANEYXRhABYAAAACVmFsdWUABgAAAHZhbHVlAAAA", new TestObject("value"), CacheValueEncoding.Bson),
        new("key", "FgAAAAJWYWx1ZQAGAAAAdmFsdWUAAA==", null, CacheValueEncoding.Bson),
        new("key", "not base64", null, CacheValueEncoding.Bson),
        new("key", "bm90IGJzb24=", null, CacheValueEncoding.Bson),
        new("key", "{\"Data\":{\"Value\":\"value\"}}", new TestObject("value"), CacheValueEncoding.Json),
        new("key", "not json", null, CacheValueEncoding.Json),
        new("key", "{}", null, CacheValueEncoding.Json)
    ];

    public static TheoryData<ShouldReturnExpectedNumericValueFromCacheTestData> ShouldReturnNumericObjectFromStringTestDataItems =>
    [
        new("key", "HwAAAANEYXRhABQAAAABVmFsdWUAlRcvtGWH9D8AAA==", new TestNumericObject(1.28305597671849m), CacheValueEncoding.Bson), // decimal precision lost when parsing via Bson reader
        new("key", "HwAAAANEYXRhABQAAAABVmFsdWUAjxcvtGWH9D8AAA==", new TestNumericObject(1.28305597671849m), CacheValueEncoding.Bson),
        new("key", "{\"Data\":{\"Value\":1.2830559767184912978}}", new TestNumericObject(1.2830559767184912978m), CacheValueEncoding.Json),
        new("key", "{\"Data\":{\"Value\":1.28305597671849}}", new TestNumericObject(1.28305597671849m), CacheValueEncoding.Json)
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromStringTestDataItems), MemberType = typeof(WhenRedisDistributedCacheGetsObject))]
    public async Task ShouldReturnExpectedValueFromCache(ShouldReturnExpectedValueFromCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Encoded)
            .Verifiable(Times.Once);

        var actual = await Cache.GetAsync<TestObject>(input.Key, input.CacheValueEncoding);

        Database.Verify();
        Assert.Equal(input.ExpectedValue, actual);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnNumericObjectFromStringTestDataItems), MemberType = typeof(WhenRedisDistributedCacheGetsObject))]
    public async Task ShouldReturnExpectedNumericValueFromCache(ShouldReturnExpectedNumericValueFromCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Encoded)
            .Verifiable(Times.Once);

        var actual = await Cache.GetAsync<TestNumericObject>(input.Key, input.CacheValueEncoding);

        Database.Verify();
        Assert.Equal(input.ExpectedValue, actual);
    }

    [Fact]
    public async Task ShouldThrowExceptionIfRedisUnavailable()
    {
        const string key = nameof(key);
        Database
            .Setup(d => d.StringGetAsync(key, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.GetAsync<TestObject>(key));

        Database.Verify();
        Assert.NotNull(actual);
    }

    public record ShouldReturnExpectedValueFromCacheTestData(string Key, string Encoded, TestObject? ExpectedValue, CacheValueEncoding CacheValueEncoding);

    public record ShouldReturnExpectedNumericValueFromCacheTestData(string Key, string Encoded, TestNumericObject? ExpectedValue, CacheValueEncoding CacheValueEncoding);

    public record TestObject(string Value);

    public record TestNumericObject(decimal Value);
}