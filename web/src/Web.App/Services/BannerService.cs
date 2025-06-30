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
    private const string CacheKey = "banner";
    private readonly int _absolute = options.Value.Banner.AbsoluteExpiration ?? 60;
    private readonly int _sliding = options.Value.Banner.SlidingExpiration ?? 10;

    public async Task<Banner?> GetBannerOrDefault(string target)
    {
        if (memoryCache.TryGetValue(CacheKey, out var cached) && cached is Banner banner)
        {
            return banner;
        }

        var data = await bannerApi
            .GetBanner(target)
            .GetResultOrDefault<Banner>();
        var options = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(_sliding),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_absolute)
        };

        memoryCache.Set(CacheKey, data, options);
        return data;
    }
}