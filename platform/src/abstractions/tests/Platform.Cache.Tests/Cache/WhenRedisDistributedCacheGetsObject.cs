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
        new("key", "FgAAAAJWYWx1ZQAGAAAAdmFsdWUAAA==", new TestObject("value")),
        new("key", "not base64", null),
        new("key", "bm90IGJzb24=", null)
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromStringTestDataItems), MemberType = typeof(WhenRedisDistributedCacheGetsObject))]
    public async Task ShouldReturnExpectedValueFromCache(ShouldReturnExpectedValueFromCacheTestData input)
    {
        Database
            .Setup(d => d.StringGetAsync(input.Key, CommandFlags.None))
            .ReturnsAsync(input.Bson)
            .Verifiable(Times.Once);

        var actual = await Cache.GetAsync<TestObject>(input.Key);

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

    public record ShouldReturnExpectedValueFromCacheTestData(string Key, string Bson, TestObject? ExpectedValue);

    public record TestObject(string Value);
}