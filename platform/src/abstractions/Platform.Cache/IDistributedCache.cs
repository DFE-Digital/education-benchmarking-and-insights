using StackExchange.Redis;
namespace Platform.Cache;

public interface IDistributedCache
{
    Lazy<Task<IConnectionMultiplexer>> Connection { get; }

    /// <inheritdoc cref="IDatabase.StringGet(RedisKey, CommandFlags)" />
    Task<string?> GetStringAsync(string key);

    /// <inheritdoc cref="IDatabase.StringSet(RedisKey, RedisValue, TimeSpan?, bool, StackExchange.Redis.When, CommandFlags)" />
    Task<bool> SetStringAsync(string key, string value, When when = When.Always);

    /// <inheritdoc
    ///     cref="IDatabase.StringSet(KeyValuePair&lt;RedisKey, RedisValue&gt;[], StackExchange.Redis.When, CommandFlags)" />
    Task<bool> SetStringsAsync(KeyValuePair<string, string>[] values, When when = When.Always);

    Task<T?> GetAsync<T>(string key);

    Task<bool> SetAsync<T>(string key, T value, When when = When.Always);

    Task<bool> SetAsync<T>(KeyValuePair<string, T>[] values, When when = When.Always);

    /// <inheritdoc cref="IDatabase.KeyDelete(RedisKey[], CommandFlags)" />
    Task<long> DeleteAsync(params string[] keys);

    Task<T> GetSetAsync<T>(string key, Func<Task<T>> getter);

    /// <inheritdoc cref="IServer.FlushDatabase(int, CommandFlags)" />
    Task FlushAsync();
}

public enum When
{
    /// <summary>
    ///     The operation should occur whether or not there is an existing value.
    /// </summary>
    Always,

    /// <summary>
    ///     The operation should only occur when there is an existing value.
    /// </summary>
    Exists,

    /// <summary>
    ///     The operation should only occur when there is not an existing value.
    /// </summary>
    NotExists
}