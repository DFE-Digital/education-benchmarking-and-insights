using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Platform.Cache.Configuration;
using StackExchange.Redis;
namespace Platform.Cache;

[ExcludeFromCodeCoverage]
public class RedisConnectionMultiplexerFactory(ILogger<RedisConnectionMultiplexerFactory> logger, IOptions<RedisCacheOptions> options) : IRedisConnectionMultiplexerFactory
{
    public async Task<IConnectionMultiplexer> CreateAsync()
    {
        var configurationOptions = ConfigurationOptions.Parse(options.Value.ConnectionString);
        if (string.IsNullOrWhiteSpace(options.Value.Password))
        {
            logger.LogDebug("Password is empty - configuring Redis with system assigned managed identity");
            await configurationOptions.ConfigureForAzureWithSystemAssignedManagedIdentityAsync();
        }

        logger.LogInformation("Establishing connection to Redis cache at {EndPoint}", configurationOptions.EndPoints.First());
        return await ConnectionMultiplexer.ConnectAsync(configurationOptions);
    }
}

public interface IRedisConnectionMultiplexerFactory
{
    Task<IConnectionMultiplexer> CreateAsync();
}