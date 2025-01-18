using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Platform.Cache;

[ExcludeFromCodeCoverage]
public static class CacheExtensions
{
    public static IServiceCollection AddRedis(this IServiceCollection serviceCollection)
    {
        var cacheHost = Environment.GetEnvironmentVariable("Cache__Host");
        var cachePort = Environment.GetEnvironmentVariable("Cache__Port");
        var cachePassword = Environment.GetEnvironmentVariable("Cache__Password");
        var cacheAllowAdmin = Environment.GetEnvironmentVariable("Cache__AllowAdmin");
        ArgumentNullException.ThrowIfNull(cacheHost);
        ArgumentNullException.ThrowIfNull(cachePort);

        serviceCollection.AddOptions<RedisCacheOptions>().Configure(x =>
        {
            x.Host = cacheHost;
            x.Port = cachePort;
            x.Password = cachePassword;
            x.AllowAdmin = cacheAllowAdmin == "true";
        });

        serviceCollection
            .AddSingleton<IRedisConnectionMultiplexerFactory, RedisConnectionMultiplexerFactory>()
            .AddSingleton<IDistributedCache, RedisDistributedCache>()
            .AddSingleton<ICacheKeyFactory, CacheKeyFactory>();

        return serviceCollection;
    }

    public static IHealthChecksBuilder AddRedis(this IHealthChecksBuilder healthChecks)
    {
        healthChecks.AddRedis(p =>
        {
            var cache = p.GetRequiredService<IDistributedCache>();
            return cache.Connection.Value.GetAwaiter().GetResult();
        });

        return healthChecks;
    }
}