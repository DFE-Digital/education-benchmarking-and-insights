using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// ReSharper disable UnusedMethodReturnValue.Global

namespace Platform.Cache;

[ExcludeFromCodeCoverage]
public static class CacheExtensions
{
    public static IServiceCollection AddPlatformCache(this IServiceCollection serviceCollection)
    {
        var options = GetOptions();

        serviceCollection
            .AddSingleton(Options.Create(options))
            .AddSingleton<IRedisConnectionMultiplexerFactory, RedisConnectionMultiplexerFactory>()
            .AddSingleton<IDistributedCache, RedisDistributedCache>()
            .AddSingleton<ICacheKeyFactory, CacheKeyFactory>();

        return serviceCollection;
    }

    public static IHealthChecksBuilder AddPlatformCache(this IHealthChecksBuilder healthChecks)
    {
        healthChecks.AddRedis(p =>
        {
            var cache = p.GetRequiredService<IDistributedCache>();
            return cache.Connection.Value.GetAwaiter().GetResult();
        });

        return healthChecks;
    }

    private static RedisCacheOptions GetOptions()
    {
        var cacheHost = Environment.GetEnvironmentVariable("Cache__Host");
        var cachePort = Environment.GetEnvironmentVariable("Cache__Port");
        var cachePassword = Environment.GetEnvironmentVariable("Cache__Password");
        var cacheAllowAdmin = Environment.GetEnvironmentVariable("Cache__AllowAdmin");
        ArgumentNullException.ThrowIfNull(cacheHost);
        ArgumentNullException.ThrowIfNull(cachePort);

        return new RedisCacheOptions
        {
            Host = cacheHost,
            Port = cachePort,
            Password = cachePassword,
            AllowAdmin = cacheAllowAdmin == "true"
        };
    }
}