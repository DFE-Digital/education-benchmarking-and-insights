using Moq;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;
namespace Platform.Cache.Tests.Cache;

public class WhenRedisDistributedCacheDeletes(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    [Theory]
    [InlineData("key", 123)]
    public async Task ShouldReturnExpectedValueFromCache(string key, long count)
    {
        Database
            .Setup(d => d.KeyDeleteAsync(new RedisKey[]
            {
                key
            }, CommandFlags.None))
            .ReturnsAsync(count)
            .Verifiable(Times.Once);

        var actual = await Cache.DeleteAsync(key);

        Database.Verify();
        Assert.Equal(count, actual);
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

        var actual = await Assert.ThrowsAsync<RedisConnectionException>(() => Cache.DeleteAsync(key));

        Database.Verify();
        Assert.NotNull(actual);
    }
}