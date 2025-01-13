using Moq;
using Platform.Cache.Configuration;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;
namespace Platform.Cache.Tests.Cache;

public class WhenRedisDistributedCacheFlushesWithAdminMode(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper, CacheOptions)
{

    private static readonly RedisCacheOptions CacheOptions = new()
    {
        Host = "host",
        Port = "port",
        AllowAdmin = true
    };
    [Fact]
    public async Task ShouldPerformFlush()
    {
        Server
            .Setup(d => d.FlushDatabaseAsync(-1, CommandFlags.None))
            .Verifiable(Times.Once);

        await Cache.FlushAsync();

        Server.Verify();
    }
}

public class WhenRedisDistributedCacheFlushesWithoutAdminMode(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper)
{
    [Fact]
    public async Task ShouldNotPerformFlush()
    {
        Server
            .Setup(d => d.FlushDatabaseAsync(-1, CommandFlags.None))
            .Verifiable(Times.Never);

        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => Cache.FlushAsync());

        Server.Verify();
        Assert.NotNull(exception);
    }
}

public class WhenRedisDistributedCacheFlushesWithoutHostNameInConfig(ITestOutputHelper testOutputHelper) : RedisDistributedCacheTestBase(testOutputHelper, CacheOptions)
{

    private static readonly RedisCacheOptions CacheOptions = new()
    {
        Port = "port",
        AllowAdmin = true
    };
    [Fact]
    public async Task ShouldNotPerformFlush()
    {
        Server
            .Setup(d => d.FlushDatabaseAsync(-1, CommandFlags.None))
            .Verifiable(Times.Never);

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Cache.FlushAsync());

        Server.Verify();
        Assert.NotNull(exception);
    }
}