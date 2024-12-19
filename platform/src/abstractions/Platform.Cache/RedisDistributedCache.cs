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

    public async Task<string?> GetStringAsync(string key)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "CacheKey", key
                   }
               }))
        {
            var connection = await Connection.Value;
            return await connection.GetDatabase().StringGetAsync(key);
        }
    }

    public async Task<string?> GetSetStringAsync(string key, string value)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "CacheKey", key
                   }
               }))
        {
            var connection = await Connection.Value;
            return await connection.GetDatabase().StringGetSetAsync(key, value);
        }
    }

    public async Task<bool> SetStringAsync(string key, string value)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "CacheKey", key
                   }
               }))
        {
            var connection = await Connection.Value;
            return await connection.GetDatabase().StringSetAsync(key, value);
        }
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var result = await GetStringAsync(key);
        return string.IsNullOrWhiteSpace(result) ? default : FromBson<T>(result);
    }

    public async Task<T?> GetSetAsync<T>(string key, T value)
    {
        var result = await GetSetStringAsync(key, ToBson(value));
        return string.IsNullOrWhiteSpace(result) ? default : FromBson<T>(result);
    }

    public async Task<bool> SetAsync<T>(string key, T value) => await SetStringAsync(key, ToBson(value));

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
            logger.LogDebug("Cached value was not base64 encoded");
            return default;
        }

        using var ms = new MemoryStream(data);
        using var reader = new BsonDataReader(ms);
        var serializer = new JsonSerializer();
        return serializer.Deserialize<T>(reader);
    }
}