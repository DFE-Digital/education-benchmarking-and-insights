using StackExchange.Redis;
namespace Platform.Cache;

public interface IDistributedCache
{
    Lazy<Task<IConnectionMultiplexer>> Connection { get; }

    /// <inheritdoc cref="IDatabase.StringGet(RedisKey, CommandFlags)" />
    Task<string?> GetStringAsync(string key);

    /// <inheritdoc cref="IDatabase.StringGetSet(RedisKey, RedisValue, CommandFlags)" />
    Task<string?> GetSetStringAsync(string key, string value);

    /// <inheritdoc cref="IDatabase.StringSet(RedisKey, RedisValue, TimeSpan?, bool, When, CommandFlags)" />
    Task<bool> SetStringAsync(string key, string value);

    Task<T?> GetAsync<T>(string key);

    Task<T?> GetSetAsync<T>(string key, T value);

    Task<bool> SetAsync<T>(string key, T value);
}