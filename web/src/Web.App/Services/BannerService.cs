using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Web.App.Cache;
using Web.App.Domain.Content;
using Web.App.Infrastructure.Apis.Content;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IBannerService
{
    Task<Banner?> GetBannerOrDefault(string target);
}

public class BannerService(IBannerApi bannerApi, IMemoryCache memoryCache, IOptions<CacheOptions> options) : IBannerService
{
    private const string CacheKeyFormat = "Banner__{0}";
    private readonly int _absolute = options.Value.Banners.AbsoluteExpiration ?? 60;
    private readonly int _sliding = options.Value.Banners.SlidingExpiration ?? 10;

    public async Task<Banner?> GetBannerOrDefault(string target)
    {
        var cacheKey = string.Format(CacheKeyFormat, target);
        if (memoryCache.TryGetValue(cacheKey, out var cached))
        {
            return cached as Banner;
        }

        var data = await bannerApi
            .GetBanner(target)
            .GetResultOrDefault<Banner>();
        var options = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(_sliding),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_absolute)
        };

        memoryCache.Set(cacheKey, data, options);
        return data;
    }
}