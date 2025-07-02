using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Web.App.Cache;
using Web.App.Domain.Content;
using Web.App.Infrastructure.Apis.Content;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface ICommercialResourcesService
{
    Task<Dictionary<string, CommercialResourceLink[]>> GetSubCategoryLinks();
    Task<Dictionary<string, CommercialResourceLink[]>> GetCategoryLinks();
}

public class CommercialResourcesService(
    ICommercialResourcesApi commercialResourcesApi,
    IMemoryCache memoryCache,
    IOptions<CacheOptions> options) : ICommercialResourcesService
{
    private const string CacheKey = "commercial-resources";
    private readonly int _absolute = options.Value.CommercialResources.AbsoluteExpiration ?? 60;
    private readonly int _sliding = options.Value.CommercialResources.SlidingExpiration ?? 10;

    public async Task<Dictionary<string, CommercialResourceLink[]>> GetSubCategoryLinks()
    {
        var resources = await GetCommercialResources();

        var expanded = resources
            .SelectMany(res => res.SubCategory.Items.Select(cat => new
            {
                SubCategory = cat,
                Resource = (CommercialResourceLink)res
            }));

        return expanded
            .GroupBy(x => x.SubCategory)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Resource).ToArray()
            );
    }

    public async Task<Dictionary<string, CommercialResourceLink[]>> GetCategoryLinks()
    {
        var resources = await GetCommercialResources();

        var expanded = resources
            .SelectMany(res => res.Category.Items.Select(cat => new
            {
                Category = cat,
                Resource = (CommercialResourceLink)res
            }));

        return expanded
            .GroupBy(x => x.Category)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Resource).ToArray()
            );
    }

    private async Task<CommercialResourceCategorised[]> GetCommercialResources()
    {
        if (memoryCache.TryGetValue(CacheKey, out var cached) && cached is CommercialResourceCategorised[] resources)
        {
            return resources;
        }

        var data = await GetData();
        if (_sliding <= 0 || _absolute <= 0)
        {
            return data;
        }

        var options = CreateMemoryCacheEntryOptions();

        memoryCache.Set(CacheKey, data, options);

        return data;
    }

    private async Task<CommercialResourceCategorised[]> GetData()
    {
        var data = await commercialResourcesApi
            .GetCommercialResources()
            .GetResultOrDefault<CommercialResourceCategorised[]>() ?? [];

        return data;
    }

    private MemoryCacheEntryOptions CreateMemoryCacheEntryOptions()
    {
        return new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(_sliding),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_absolute)
        };
    }
}