using Moq;
using Platform.Cache.Tests.Mocks;
using StackExchange.Redis;
using Xunit.Abstractions;
namespace Platform.Cache.Tests.Cache;

public abstract class RedisDistributedCacheTestBase
{

    private static readonly RedisCacheOptions CacheOptions = new()
    {
        Host = "host",
        Port = "port"
    };
    private readonly Mock<IConnectionMultiplexer> _connectionMultiplexer = new();
    private readonly Mock<IRedisConnectionMultiplexerFactory> _factory = new();
    protected readonly RedisDistributedCache Cache;
    protected readonly Mock<IDatabase> Database = new();
    protected readonly Mock<IServer> Server = new();

    protected RedisDistributedCacheTestBase(ITestOutputHelper testOutputHelper, RedisCacheOptions? options = null)
    {
        _connectionMultiplexer.Setup(c => c.GetDatabase(It.IsAny<int>(), It.IsAny<object?>())).Returns(Database.Object);
        _factory.Setup(f => f.CreateAsync()).ReturnsAsync(_connectionMultiplexer.Object);
        var logger = MockLogger.Create<RedisDistributedCache>(testOutputHelper);

        options ??= CacheOptions;
        _factory.SetupGet(f => f.Options).Returns(options);
        _connectionMultiplexer.Setup(c => c.GetServer(options.Server, It.IsAny<object?>())).Returns(Server.Object);

        Cache = new RedisDistributedCache(logger.Object, _factory.Object);
    }
}