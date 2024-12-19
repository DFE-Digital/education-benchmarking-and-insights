using Microsoft.Extensions.Logging;
using Moq;
using Platform.Cache;
using StackExchange.Redis;
namespace Platform.Tests.Cache;

public abstract class RedisDistributedCacheTestBase
{
    private readonly Mock<IConnectionMultiplexer> _connectionMultiplexer = new();
    private readonly Mock<IRedisConnectionMultiplexerFactory> _factory = new();
    private readonly Mock<ILogger<RedisDistributedCache>> _logger = new();
    protected readonly RedisDistributedCache Cache;
    protected readonly Mock<IDatabase> Database = new();

    protected RedisDistributedCacheTestBase()
    {
        _connectionMultiplexer.Setup(c => c.GetDatabase(It.IsAny<int>(), It.IsAny<object?>())).Returns(Database.Object);
        _factory.Setup(f => f.CreateAsync()).ReturnsAsync(_connectionMultiplexer.Object);

        Cache = new RedisDistributedCache(_logger.Object, _factory.Object);
    }
}