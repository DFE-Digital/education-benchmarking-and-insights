using Microsoft.Extensions.DependencyInjection;
namespace Platform.Cache.Configuration;

public static class Services
{
    public static void AddRedis(this IServiceCollection serviceCollection)
    {
        var cacheHost = Environment.GetEnvironmentVariable("Cache__Host");
        var cachePort = Environment.GetEnvironmentVariable("Cache__Port");
        var cachePassword = Environment.GetEnvironmentVariable("Cache__Password");
        ArgumentNullException.ThrowIfNull(cacheHost);
        ArgumentNullException.ThrowIfNull(cachePort);

        serviceCollection.AddOptions<RedisCacheOptions>().Configure(x =>
        {
            x.Host = cacheHost;
            x.Port = cachePort;
            x.Password = cachePassword;
        });

        serviceCollection
            .AddSingleton<IRedisConnectionMultiplexerFactory, RedisConnectionMultiplexerFactory>()
            .AddSingleton<IDistributedCache, RedisDistributedCache>();
    }

    public static void AddRedis(this IHealthChecksBuilder healthChecks)
    {
        healthChecks.AddRedis(p =>
        {
            var cache = p.GetService<IDistributedCache>();
            return cache!.Connection.Value.GetAwaiter().GetResult();
        });
    }
}