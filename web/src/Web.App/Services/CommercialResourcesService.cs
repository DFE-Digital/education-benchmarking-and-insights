using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Web.App.Cache;
using Web.App.Domain;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface ICommercialResourcesService
{
    Task<CommercialResources[]> GetResources();
}

public class CommercialResourcesService(
    ICommercialResourcesApi commercialResourcesApi,
    IMemoryCache memoryCache,
    IOptions<CacheOptions> options) : ICommercialResourcesService
{
    private readonly int _sliding = options.Value.ReturnYears.SlidingExpiration ?? 10;
    private readonly int _absolute = options.Value.ReturnYears.AbsoluteExpiration ?? 60;
    private const string CacheKey = "commercial-resources";

    public async Task<CommercialResources[]> GetResources()
    {
        if (memoryCache.TryGetValue(CacheKey, out var cached) && cached is CommercialResources[] resources)
        {
            return resources;
        }

        var data = await commercialResourcesApi
            .GetCommercialResources()
            .GetResultOrDefault<CommercialResources[]>() ?? [];

        MemoryCacheEntryOptions cacheEntryOptions = new()
        {
            SlidingExpiration = TimeSpan.FromMinutes(_sliding),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_absolute)
        };

        memoryCache.Set(CacheKey, data, cacheEntryOptions);

        return data;
    }
}