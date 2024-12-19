using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using StackExchange.Redis;
namespace Platform.Cache;

/// <remarks>
///     From the
///     <a href="https://github.com/StackExchange/StackExchange.Redis/blob/main/docs/Basics.md#using-a-redis-database">docs</a>
///     :
///     The object returned from <c>GetDatabase</c> is a cheap pass-thru object, and does not need to be stored.
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
            return await db.StringGetAsync(key);
        }
    }

    /// <exception cref="RedisConnectionException"></exception>
    public async Task<bool> SetStringAsync(string key, string value)
    {
        using (LoggerContext(key))
        {
            var db = await GetDatabase();
            return await db.StringSetAsync(key, value);
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

    public async Task<bool> SetAsync<T>(string key, T value)
    {
        using (LoggerContext(key))
        {
            return await SetStringAsync(key, ToBson(value));
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
            return default;
        }

        using (LoggerContext(string.Join(",", filteredKeys)))
        {
            var db = await GetDatabase();
            return await db.KeyDeleteAsync(filteredKeys);
        }
    }

    private async Task<IDatabase> GetDatabase()
    {
        var connection = await Connection.Value;
        return connection.GetDatabase();
    }

    private IDisposable? LoggerContext(string key) => logger.BeginScope(new Dictionary<string, object>
    {
        {
            "CacheKey", key
        }
    });

    private static string ToBson<T>(T value)
    {
        using var ms = new MemoryStream();
        using var dataWriter = new BsonDataWriter(ms);
        var serializer = new JsonSerializer();
        serializer.Serialize(dataWriter, value);
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
            return serializer.Deserialize<T>(reader);
        }
        catch (Exception ex)
        {
            logger.LogWarning("Cached value was not valid Bson. {Message}", ex.Message);
            return default;
        }
    }
}