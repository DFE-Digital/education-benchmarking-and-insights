using StackExchange.Redis;
namespace Platform.Cache;

public interface IDistributedCache
{
    Lazy<Task<IConnectionMultiplexer>> Connection { get; }

    /// <inheritdoc cref="IDatabase.StringGet(RedisKey, CommandFlags)" />
    Task<string?> GetStringAsync(string key);

    /// <inheritdoc cref="IDatabase.StringSet(RedisKey, RedisValue, TimeSpan?, bool, When, CommandFlags)" />
    Task<bool> SetStringAsync(string key, string value);

    /// <inheritdoc cref="IDatabase.StringSet(KeyValuePair&lt;RedisKey, RedisValue&gt;[], When, CommandFlags)" />
    Task<bool> SetStringsAsync(KeyValuePair<string, string>[] values);

    Task<T?> GetAsync<T>(string key);

    Task<bool> SetAsync<T>(string key, T value);

    Task<bool> SetAsync<T>(KeyValuePair<string, T>[] values);

    /// <inheritdoc cref="IDatabase.KeyDelete(RedisKey[], CommandFlags)" />
    Task<long> DeleteAsync(params string[] keys);
}