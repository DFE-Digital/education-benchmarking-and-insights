using Moq;
using StackExchange.Redis;
using Xunit;
// ReSharper disable NotAccessedPositionalProperty.Global
namespace Platform.Tests.Cache;

public class WhenRedisDistributedCacheSetsObject : RedisDistributedCacheTestBase
{
    public static TheoryData<ShouldSetValueInCacheTestData> ShouldReturnObjectFromStringTestDataItems =>
    [
        new("key", new TestObject("value"), "FgAAAAJWYWx1ZQAGAAAAdmFsdWUAAA==")
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromStringTestDataItems), MemberType = typeof(WhenRedisDistributedCacheSetsObject))]
    public async Task ShouldReturnExpectedValueFromCache(ShouldSetValueInCacheTestData input)
    {
        Database
            .Setup(d => d.StringSetAsync(input.Key, input.ExpectedBson, null, false, When.Always, CommandFlags.None))
            .ReturnsAsync(true)
            .Verifiable(Times.Once);

        var actual = await Cache.SetAsync(input.Key, input.Value);

        Database.Verify();
        Assert.True(actual);
    }
    
    [Fact]
    public async Task ShouldThrowExceptionIfRedisUnavailable()
    {
        const string key = nameof(key);
        var value = new TestObject("value");
        Database
            .Setup(d => d.StringSetAsync(key, It.IsAny<RedisValue>(), null, false, When.Always, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.SetAsync(key, value));

        Database.Verify();
        Assert.NotNull(actual);
    }

    public record ShouldSetValueInCacheTestData(string Key, TestObject Value, string ExpectedBson);

    public record TestObject(string Value);
}