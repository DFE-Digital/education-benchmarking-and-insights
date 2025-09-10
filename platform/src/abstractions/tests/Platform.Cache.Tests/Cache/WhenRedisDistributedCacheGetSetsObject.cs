using Moq;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable NotAccessedPositionalProperty.Global
namespace Platform.Cache.Tests.Cache;

public class WhenRedisDistributedCacheGetSetsObject(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    public static TheoryData<ShouldReturnExpectedValueFromCacheTestData> ShouldReturnObjectFromCacheWhenPresentDataItems =>
    [
        new("key", "IQAAAANEYXRhABYAAAACVmFsdWUABgAAAHZhbHVlAAAA", new TestObject("Lookup"), new TestObject("value"), CacheValueEncoding.Bson),
        new("key", "{\"Data\":{\"Value\":\"value\"}}", new TestObject("Lookup"), new TestObject("value"), CacheValueEncoding.Json)
    ];

    public static TheoryData<ShouldReturnExpectedCollectionValueFromCacheTestData> ShouldReturnCollectionObjectFromCacheWhenPresentDataItems =>
    [
        new("key", "KQAAAAREYXRhAB4AAAADMAAWAAAAAlZhbHVlAAYAAAB2YWx1ZQAAAAA=", [new TestObject("Lookup")], [new TestObject("value")], CacheValueEncoding.Bson),
        new("key", "{\"Data\":[{\"Value\":\"value\"}]}", [new TestObject("Lookup")], [new TestObject("value")], CacheValueEncoding.Json)
    ];

    public static TheoryData<ShouldReturnExpectedValueFromCacheTestData> ShouldReturnObjectGetterTestDataItems =>
    [
        new("key", null, new TestObject("Lookup"), new TestObject("Lookup"), CacheValueEncoding.Bson),
        new("key", "not base64", new TestObject("Lookup"), new TestObject("Lookup"), CacheValueEncoding.Bson),
        new("key", "bm90IGJzb24=", new TestObject("Lookup"), new TestObject("Lookup"), CacheValueEncoding.Bson),
        new("key", "FgAAAAJWYWx1ZQAGAAAAdmFsdWUAAA==", new TestObject("Lookup"), new TestObject("Lookup"), CacheValueEncoding.Bson),
        new("key", null, new TestObject("Lookup"), new TestObject("Lookup"), CacheValueEncoding.Json),
        new("key", "not json", new TestObject("Lookup"), new TestObject("Lookup"), CacheValueEncoding.Json),
        new("key", "{}", new TestObject("Lookup"), new TestObject("Lookup"), CacheValueEncoding.Json)
    ];

    public static TheoryData<ShouldSetValueInCacheTestData> ShouldSetObjectTestDataItems =>
    [
        new("key", null, new TestObject("Lookup"), "IgAAAANEYXRhABcAAAACVmFsdWUABwAAAExvb2t1cAAAAA==", CacheValueEncoding.Bson),
        new("key", null, new[]
        {
            new TestObject("Lookup")
        }, "KgAAAAREYXRhAB8AAAADMAAXAAAAAlZhbHVlAAcAAABMb29rdXAAAAAA", CacheValueEncoding.Bson),
        new("key", "not base64", new TestObject("Lookup"), "IgAAAANEYXRhABcAAAACVmFsdWUABwAAAExvb2t1cAAAAA==", CacheValueEncoding.Bson),
        new("key", "bm90IGJzb24=", new TestObject("Lookup"), "IgAAAANEYXRhABcAAAACVmFsdWUABwAAAExvb2t1cAAAAA==", CacheValueEncoding.Bson),
        new("key", null, new TestObject("Lookup"), "{\"Data\":{\"Value\":\"Lookup\"}}", CacheValueEncoding.Json),
        new("key", "not json", new TestObject("Lookup"), "{\"Data\":{\"Value\":\"Lookup\"}}", CacheValueEncoding.Json),
        new("key", "{}", new TestObject("Lookup"), "{\"Data\":{\"Value\":\"Lookup\"}}", CacheValueEncoding.Json)
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromCacheWhenPresentDataItems), MemberType = typeof(WhenRedisDistributedCacheGetSetsObject))]
    public async Task ShouldReturnExpectedValueFromCacheWhenPresent(ShouldReturnExpectedValueFromCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Encoded)
            .Verifiable(Times.Once);

        var actual = await Cache.GetSetAsync(input.Key, () => Task.FromResult(input.Lookup), input.CacheValueEncoding);

        Database.Verify();
        Assert.Equal(input.ExpectedValue, actual);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnCollectionObjectFromCacheWhenPresentDataItems), MemberType = typeof(WhenRedisDistributedCacheGetSetsObject))]
    public async Task ShouldReturnExpectedCollectionValueFromCacheWhenPresent(ShouldReturnExpectedCollectionValueFromCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Encoded)
            .Verifiable(Times.Once);

        var actual = await Cache.GetSetAsync(input.Key, () => Task.FromResult(input.Lookup), input.CacheValueEncoding);

        Database.Verify();
        Assert.Equal(input.ExpectedValue, actual);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnObjectGetterTestDataItems), MemberType = typeof(WhenRedisDistributedCacheGetSetsObject))]
    public async Task ShouldReturnExpectedValueFromGetterWhenNotPresentInCache(ShouldReturnExpectedValueFromCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Encoded)
            .Verifiable(Times.Once);

        var actual = await Cache.GetSetAsync(input.Key, () => Task.FromResult(input.Lookup), input.CacheValueEncoding);

        Database.Verify();
        Assert.Equal(input.ExpectedValue, actual);
    }

    [Theory]
    [MemberData(nameof(ShouldSetObjectTestDataItems), MemberType = typeof(WhenRedisDistributedCacheGetSetsObject))]
    public async Task ShouldSetWhenNotPresentInCache(ShouldSetValueInCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Encoded)
            .Verifiable(Times.Once);

        var actualEncoded = string.Empty;
        Database
            .Setup(d => d.StringSetAsync(input.Key, It.IsAny<RedisValue>(), null, false, StackExchange.Redis.When.NotExists, CommandFlags.None))
            .ReturnsAsync(true)
            .Callback<RedisKey, RedisValue, TimeSpan?, bool, StackExchange.Redis.When, CommandFlags>((_, value, _, _, _, _) =>
            {
                actualEncoded = value;
            }).Verifiable(Times.Once);

        await Cache.GetSetAsync(input.Key, () => Task.FromResult(input.Lookup), input.CacheValueEncoding);

        Database.Verify();
        Assert.Equal(input.ExpectedEncoded, actualEncoded);
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

    public record ShouldReturnExpectedValueFromCacheTestData(string Key, string? Encoded, TestObject Lookup, TestObject? ExpectedValue, CacheValueEncoding CacheValueEncoding);

    public record ShouldReturnExpectedCollectionValueFromCacheTestData(string Key, string? Encoded, IEnumerable<TestObject> Lookup, IEnumerable<TestObject>? ExpectedValue, CacheValueEncoding CacheValueEncoding);

    public record ShouldSetValueInCacheTestData(string Key, string? Encoded, object Lookup, string ExpectedEncoded, CacheValueEncoding CacheValueEncoding);

    public record TestObject(string Value);
}