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
        new("key", new TestObject("value"), "IQAAAANEYXRhABYAAAACVmFsdWUABgAAAHZhbHVlAAAA")
    ];

    [Theory]
    [MemberData(nameof(ShouldReturnObjectFromStringTestDataItems), MemberType = typeof(WhenRedisDistributedCacheSetsObject))]
    public async Task ShouldReturnExpectedValueFromCache(ShouldSetValueInCacheTestData input)
    {
        Database
            .Setup(d => d.StringSetAsync(input.Key, input.ExpectedBson, null, false, StackExchange.Redis.When.Always, CommandFlags.None))
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
            .Setup(d => d.StringSetAsync(key, It.IsAny<RedisValue>(), null, false, StackExchange.Redis.When.Always, CommandFlags.None))
            .ThrowsAsync(new RedisConnectionException(ConnectionFailureType.UnableToConnect, "Unable to connect to Redis"))
            .Verifiable(Times.Once);

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.SetAsync(key, value));

        Database.Verify();
        Assert.NotNull(actual);
    }

    public record ShouldSetValueInCacheTestData(string Key, TestObject Value, string ExpectedBson);

    public record TestObject(string Value);
}