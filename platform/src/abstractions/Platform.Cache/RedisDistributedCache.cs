using System.Text;
using System.Text.RegularExpressions;
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
public partial class RedisDistributedCache(ILogger<RedisDistributedCache> logger, IRedisConnectionMultiplexerFactory factory) : IDistributedCache
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

    public async Task<T?> GetAsync<T>(string key, CacheValueEncoding cacheValueEncoding = CacheValueEncoding.Json)
    {
        using (LoggerContext(key))
        {
            var result = await GetStringAsync(key);
            return string.IsNullOrWhiteSpace(result)
                ? default
                : cacheValueEncoding == CacheValueEncoding.Bson
                    ? FromBson<T>(result)
                    : FromJson<T>(result);
        }
    }

    public async Task<bool> SetAsync<T>(string key, T value, When when = Cache.When.Always, CacheValueEncoding cacheValueEncoding = CacheValueEncoding.Json)
    {
        using (LoggerContext(key))
        {
            return await SetStringAsync(key, cacheValueEncoding == CacheValueEncoding.Bson ? ToBson(value) : ToJson(value), when);
        }
    }

    public async Task<bool> SetAsync<T>(KeyValuePair<string, T>[] values, When when = Cache.When.Always, CacheValueEncoding cacheValueEncoding = CacheValueEncoding.Json)
    {
        var filteredValues = values
            .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key))
            .ToArray();

        using (LoggerContext(string.Join(",", filteredValues.Select(v => v.Key))))
        {
            return await SetStringsAsync(filteredValues
                .Select(kvp => new KeyValuePair<string, string>(kvp.Key, cacheValueEncoding == CacheValueEncoding.Bson ? ToBson(kvp.Value) : ToJson(kvp.Value)))
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

    public async Task DeleteAsync(string pattern)
    {
        if (InvalidCacheKeyPattern().IsMatch(pattern))
        {
            throw new ArgumentException("Invalid cache key pattern", nameof(pattern));
        }

        using (LoggerContext(pattern))
        {
            var db = await GetDatabase();
            logger.LogDebug("Getting and deleting key(s) matching {Pattern} from Redis", pattern);

            // scan first 1000 matching keys and then DEL all matches
            const string script = "return redis.call('DEL', unpack(redis.call('SCAN', 0, 'COUNT', 1000, 'MATCH', @pattern)[2]))";
            var prepared = LuaScript.Prepare(script);

            try
            {
                await db.ScriptEvaluateAsync(prepared, new
                {
                    pattern
                });
            }
            catch (RedisServerException ex) when (ex.Message.Contains("Wrong number of args calling Redis command From Lua script"))
            {
                // `SCAN` may return `(empty array)`, which `DEL` subsequently errors on if no keys found
                logger.LogInformation("No cache keys to delete matching {Pattern}", pattern);
            }
        }
    }

    public async Task<T> GetSetAsync<T>(string key, Func<Task<T>> getter, CacheValueEncoding cacheValueEncoding = CacheValueEncoding.Json)
    {
        using (LoggerContext(key))
        {
            var cached = await GetAsync<T>(key, cacheValueEncoding);
            if (!EqualityComparer<T>.Default.Equals(cached, default))
            {
                logger.LogDebug("Returning cached object for {Key} from Redis", key);
                return cached!;
            }

            logger.LogDebug("Cached object for {Key} not found or `null`, so executing getter", key);
            var result = await getter();
            await SetAsync(key, result, Cache.When.NotExists, cacheValueEncoding);
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

    /// <inheritdoc cref="IConnectionMultiplexer.GetDatabase" />
    private async Task<IDatabase> GetDatabase()
    {
        var connection = await Connection.Value;
        return connection.GetDatabase();
    }

    /// <inheritdoc cref="IConnectionMultiplexer.GetServer(string, object?)" />
    private async Task<IServer> GetServer()
    {
        ArgumentNullException.ThrowIfNull(factory.Options.Host);
        var connection = await Connection.Value;
        return connection.GetServer(factory.Options.Server);
    }

    private IDisposable? LoggerContext(string key) => logger.BeginScope(new Dictionary<string, object>
    {
        { "CacheKey", key }
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

    private static string ToJson<T>(T value)
    {
        var data = new CacheData<T>(value);

        using var ms = new MemoryStream();
        using var streamWriter = new StreamWriter(ms);
        using var dataWriter = new JsonTextWriter(streamWriter);
        var serializer = new JsonSerializer();
        serializer.Serialize(dataWriter, data);
        streamWriter.Flush();
        return Encoding.ASCII.GetString(ms.ToArray());
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
        reader.FloatParseHandling = FloatParseHandling.Decimal;
        var serializer = new JsonSerializer
        {
            FloatParseHandling = FloatParseHandling.Decimal
        };

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

    private T? FromJson<T>(string jsonData)
    {
        var data = Encoding.ASCII.GetBytes(jsonData);
        using var ms = new MemoryStream(data);
        using var streamReader = new StreamReader(ms);
        using var reader = new JsonTextReader(streamReader);
        var serializer = new JsonSerializer
        {
            FloatParseHandling = FloatParseHandling.Decimal
        };

        try
        {
            var cached = serializer.Deserialize<CacheData<T>>(reader);
            return cached == null ? default : cached.Data;
        }
        catch (Exception ex)
        {
            logger.LogWarning("Cached value was not valid Json. {Message}", ex.Message);
            return default;
        }
    }

    private static StackExchange.Redis.When When(When when) => (StackExchange.Redis.When)when;

    [GeneratedRegex(@"[^a-zA-Z0-9\-\|:\*]")]
    private static partial Regex InvalidCacheKeyPattern();
}