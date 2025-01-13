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
        new("key", "FgAAAAJWYWx1ZQAGAAAAdmFsdWUAAA==", new TestObject("Lookup"), new TestObject("value"))
    ];

    public static TheoryData<ShouldReturnExpectedValueFromCacheTestData> ShouldReturnObjectGetterTestDataItems =>
    [
        new("key", null, new TestObject("Lookup"), new TestObject("Lookup")),
        new("key", "not base64", new TestObject("Lookup"), new TestObject("Lookup")),
        new("key", "bm90IGJzb24=", new TestObject("Lookup"), new TestObject("Lookup"))
    ];

    public static TheoryData<ShouldSetValueInCacheTestData> ShouldSetObjectTestDataItems =>
    [
        new("key", null, new TestObject("Lookup"), "FwAAAAJWYWx1ZQAHAAAATG9va3VwAAA="),
        new("key", "not base64", new TestObject("Lookup"), "FwAAAAJWYWx1ZQAHAAAATG9va3VwAAA="),
        new("key", "bm90IGJzb24=", new TestObject("Lookup"), "FwAAAAJWYWx1ZQAHAAAATG9va3VwAAA=")
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromCacheWhenPresentDataItems), MemberType = typeof(WhenRedisDistributedCacheGetSetsObject))]
    public async Task ShouldReturnExpectedValueFromCacheWhenPresent(ShouldReturnExpectedValueFromCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Bson)
            .Verifiable(Times.Once);

        var actual = await Cache.GetSetAsync(input.Key, () => Task.FromResult(input.Lookup));

        Database.Verify();
        Assert.Equal(input.ExpectedValue, actual);
    }

    [Theory]
    [MemberData(nameof(ShouldReturnObjectGetterTestDataItems), MemberType = typeof(WhenRedisDistributedCacheGetSetsObject))]
    public async Task ShouldReturnExpectedValueFromGetterWhenNotPresentInCache(ShouldReturnExpectedValueFromCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Bson)
            .Verifiable(Times.Once);

        var actual = await Cache.GetSetAsync(input.Key, () => Task.FromResult(input.Lookup));

        Database.Verify();
        Assert.Equal(input.ExpectedValue, actual);
    }

    [Theory]
    [MemberData(nameof(ShouldSetObjectTestDataItems), MemberType = typeof(WhenRedisDistributedCacheGetSetsObject))]
    public async Task ShouldSetWhenNotPresentInCache(ShouldSetValueInCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Bson)
            .Verifiable(Times.Once);

        Database
            .Setup(d => d.StringSetAsync(input.Key, input.ExpectedBson, null, false, StackExchange.Redis.When.NotExists, CommandFlags.None))
            .ReturnsAsync(true)
            .Verifiable(Times.Once);

        await Cache.GetSetAsync(input.Key, () => Task.FromResult(input.Lookup));

        Database.Verify();
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

    public record ShouldReturnExpectedValueFromCacheTestData(string Key, string? Bson, TestObject Lookup, TestObject? ExpectedValue);

    public record ShouldSetValueInCacheTestData(string Key, string? Bson, TestObject Lookup, string ExpectedBson);

    public record TestObject(string Value);
}