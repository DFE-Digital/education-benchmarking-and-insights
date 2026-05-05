using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// ReSharper disable UnusedMethodReturnValue.Global

namespace Platform.Cache;

[ExcludeFromCodeCoverage]
public static class CacheExtensions
{
    public static IServiceCollection AddPlatformCache(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var options = GetOptions(configuration);

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

    private static RedisCacheOptions GetOptions(IConfiguration configuration)
    {
        var cacheHost = configuration["Cache__Host"];
        var cachePort = configuration["Cache__Port"];
        var cachePassword = configuration["Cache__Password"];
        var cacheAllowAdmin = configuration["Cache__AllowAdmin"];
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