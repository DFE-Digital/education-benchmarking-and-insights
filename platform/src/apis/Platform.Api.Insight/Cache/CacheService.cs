using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Platform.Functions.Configuration;
using StackExchange.Redis;
namespace Platform.Api.Insight.Cache;

/// <remarks>
///     From the
///     <a href="https://github.com/StackExchange/StackExchange.Redis/blob/main/docs/Basics.md#using-a-redis-database">docs</a>
///     :
///     The object returned from <c>GetDatabase</c> is a cheap pass-thru object, and does not need to be stored.
/// </remarks>
public class RedisDistributedCache : IDistributedCache
{
    public RedisDistributedCache(IOptions<RedisCacheOptions> options, ILogger<RedisDistributedCache> logger)
    {
        Connection = new Lazy<Task<ConnectionMultiplexer>>(async () =>
        {
            var configurationOptions = ConfigurationOptions.Parse(options.Value.ConnectionString);
            if (string.IsNullOrWhiteSpace(options.Value.Password))
            {
                logger.LogDebug("Password is empty - configuring Redis with system assigned managed identity");
                await configurationOptions.ConfigureForAzureWithSystemAssignedManagedIdentityAsync();
            }

            logger.LogDebug("Connecting to Redis at {EndPoint}", configurationOptions.EndPoints.First());
            return await ConnectionMultiplexer.ConnectAsync(configurationOptions);
        });
    }

    private Lazy<Task<ConnectionMultiplexer>> Connection { get; }

    public async Task<string?> GetStringAsync(string key)
    {
        var connection = await Connection.Value;
        return await connection.GetDatabase().StringGetAsync(key);
    }

    public async Task<string?> GetSetStringAsync(string key, string value)
    {
        var connection = await Connection.Value;
        return await connection.GetDatabase().StringGetSetAsync(key, value);
    }

    public async Task<bool> SetStringAsync(string key, string value)
    {
        var connection = await Connection.Value;
        return await connection.GetDatabase().StringSetAsync(key, value);
    }
}

public interface IDistributedCache
{
    /// <inheritdoc cref="IDatabase.StringGet(RedisKey, CommandFlags)" />
    Task<string?> GetStringAsync(string key);

    /// <inheritdoc cref="IDatabase.StringGetSet(RedisKey, RedisValue, CommandFlags)" />
    Task<string?> GetSetStringAsync(string key, string value);

    /// <inheritdoc cref="IDatabase.StringSet(RedisKey, RedisValue, TimeSpan?, bool, When, CommandFlags)" />
    Task<bool> SetStringAsync(string key, string value);
}