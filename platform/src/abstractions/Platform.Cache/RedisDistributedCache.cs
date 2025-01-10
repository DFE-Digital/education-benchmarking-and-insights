using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using StackExchange.Redis;
namespace Platform.Cache;

/// <remarks>
///     From the
///     <a href="https://github.com/StackExchange/StackExchange.Redis/blob/main/docs/Basics.md#using-a-redis-database">docs</a>
///     :
///     The object returned from <c>GetDatabase</c> is a cheap pass-through object, and does not need to be stored.
/// </remarks>
public class RedisDistributedCache(ILogger<RedisDistributedCache> logger, IRedisConnectionMultiplexerFactory factory) : IDistributedCache
{
    public Lazy<Task<IConnectionMultiplexer>> Connection { get; } = new(factory.CreateAsync);

    /// <exception cref="RedisConnectionException"></exception>
    public async Task<string?> GetStringAsync(string key)
    {
        using (LoggerContext(key))
        {
            var db = await GetDatabase();
            logger.LogDebug("Getting string value for key {Key} from Redis", key);
            return await db.StringGetAsync(key);
        }
    }

    /// <exception cref="RedisConnectionException"></exception>
    public async Task<bool> SetStringAsync(string key, string value, When when = Cache.When.Always)
    {
        using (LoggerContext(key))
        {
            var db = await GetDatabase();
            logger.LogDebug("Setting string value for key {Key} in Redis ({When})", key, when);
            return await db.StringSetAsync(key, value, when: When(when));
        }
    }

    /// <exception cref="RedisConnectionException"></exception>
    public async Task<bool> SetStringsAsync(KeyValuePair<string, string>[] values, When when = Cache.When.Always)
    {
        var filteredValues = values
            .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
            .Select(kvp => new KeyValuePair<RedisKey, RedisValue>(kvp.Key, kvp.Value))
            .ToArray();
        var filteredKeys = filteredValues.Select(v => v.Key);

        using (LoggerContext(string.Join(",", filteredKeys)))
        {
            var db = await GetDatabase();
            logger.LogDebug("Setting string value(s) for key(s) {Keys} in Redis ({When})", filteredKeys, when);
            return await db.StringSetAsync(filteredValues, When(when));
        }
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        using (LoggerContext(key))
        {
            var result = await GetStringAsync(key);
            return string.IsNullOrWhiteSpace(result) ? default : FromBson<T>(result);
        }
    }

    public async Task<bool> SetAsync<T>(string key, T value, When when = Cache.When.Always)
    {
        using (LoggerContext(key))
        {
            return await SetStringAsync(key, ToBson(value), when);
        }
    }

    public async Task<bool> SetAsync<T>(KeyValuePair<string, T>[] values, When when = Cache.When.Always)
    {
        var filteredValues = values
            .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
            .ToArray();

        using (LoggerContext(string.Join(",", filteredValues.Select(v => v.Key))))
        {
            return await SetStringsAsync(filteredValues
                .Select(kvp => new KeyValuePair<string, string>(kvp.Key, ToBson(kvp.Value)))
                .ToArray(), when);
        }
    }

    /// <exception cref="RedisConnectionException"></exception>
    public async Task<long> DeleteAsync(params string[] keys)
    {
        var filteredKeys = keys
            .Where(k => !string.IsNullOrWhiteSpace(k))
            .Select(k => new RedisKey(k))
            .ToArray();
        if (filteredKeys.Length == 0)
        {
            return 0;
        }

        using (LoggerContext(string.Join(",", filteredKeys)))
        {
            var db = await GetDatabase();
            logger.LogDebug("Deleting key(s) {Keys} from Redis", filteredKeys);
            return await db.KeyDeleteAsync(filteredKeys);
        }
    }

    public async Task<T> GetSetAsync<T>(string key, Func<Task<T>> getter)
    {
        using (LoggerContext(key))
        {
            var cached = await GetAsync<T>(key);
            if (!EqualityComparer<T>.Default.Equals(cached, default))
            {
                logger.LogDebug("Returning cached object for {Key} from Redis", key);
                return cached!;
            }

            logger.LogDebug("Cached object for {Key} not found or `null`, so executing getter", key);
            var result = await getter();
            await SetAsync(key, result, Cache.When.NotExists);
            return result;
        }
    }

    public async Task FlushAsync()
    {
        if (!factory.Options.AllowAdmin)
        {
            throw new UnauthorizedAccessException("Unable to perform operation on Redis because admin mode is not enabled");
        }

        var server = await GetServer();
        logger.LogDebug("Flushing Redis database");
        await server.FlushDatabaseAsync();
    }

    private async Task<IDatabase> GetDatabase()
    {
        var connection = await Connection.Value;
        return connection.GetDatabase();
    }

    private async Task<IServer> GetServer()
    {
        ArgumentNullException.ThrowIfNull(factory.Options.Host);
        var connection = await Connection.Value;
        return connection.GetServer(factory.Options.Host);
    }

    private IDisposable? LoggerContext(string key) => logger.BeginScope(new Dictionary<string, object>
    {
        {
            "CacheKey", key
        }
    });

    private static string ToBson<T>(T value)
    {
        var data = new CacheData<T>(value);

        using var ms = new MemoryStream();
        using var dataWriter = new BsonDataWriter(ms);
        var serializer = new JsonSerializer();
        serializer.Serialize(dataWriter, data);
        return Convert.ToBase64String(ms.ToArray());
    }

    private T? FromBson<T>(string base64data)
    {
        var data = new byte[base64data.Length];
        if (!Convert.TryFromBase64String(base64data, data, out _))
        {
            logger.LogWarning("Cached value was not Base64 encoded");
            return default;
        }

        using var ms = new MemoryStream(data);
        using var reader = new BsonDataReader(ms);
        var serializer = new JsonSerializer();

        try
        {
            var cached = serializer.Deserialize<CacheData<T>>(reader);
            return cached == null ? default : cached.Data;
        }
        catch (Exception ex)
        {
            logger.LogWarning("Cached value was not valid Bson. {Message}", ex.Message);
            return default;
        }
    }

    private static StackExchange.Redis.When When(When when) => (StackExchange.Redis.When)when;
}