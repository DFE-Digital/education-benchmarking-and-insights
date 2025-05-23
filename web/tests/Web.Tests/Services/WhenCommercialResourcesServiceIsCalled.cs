using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Web.App.Cache;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenCommercialResourcesServiceIsCalled
{
    private readonly Mock<ICommercialResourcesApi> _api = new();
    private readonly CommercialResources[] _commercialResources =
    [
        new CommercialResources
        {
            Category = "Teaching and Teaching support staff",
            SubCategory = "TestSubCategory",
            Title = "TestTitle",
            Url = "testUrl"

        }
    ];
    private const string CacheKey = "commercial-resources";

    private static (IMemoryCache mockCache, IOptions<CacheOptions> options) CreateCacheAndOptions()
    {
        var mockCache = new MemoryCache(new MemoryCacheOptions());
        var options = Options.Create(new CacheOptions
        {
            CommercialResources = new CacheSettings { SlidingExpiration = 60, AbsoluteExpiration = 3600 }
        });
        return (mockCache, options);
    }

    [Fact]
    public async Task GetYearsShouldReturnCachedResponseIfItExist()
    {
        var (mockCache, options) = CreateCacheAndOptions();

        mockCache.Set(CacheKey, _commercialResources);

        _api.Setup(api => api.GetCommercialResources())
            .ReturnsAsync(ApiResult.Ok(_commercialResources));

        var service = new CommercialResourcesService(_api.Object, mockCache, options);

        var actual = await service.GetResources();

        _api.Verify(api => api.GetCommercialResources(), Times.Never());
        Assert.IsType<CommercialResources[]>(actual);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Category, actual.FirstOrDefault()?.Category);
        Assert.Equal(_commercialResources.FirstOrDefault()?.SubCategory, actual.FirstOrDefault()?.SubCategory);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Title, actual.FirstOrDefault()?.Title);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Url, actual.FirstOrDefault()?.Url);
    }

    [Fact]
    public async Task GetYearsShouldSetCacheCorrectlyIfItDoesNotExist()
    {
        var (mockCache, options) = CreateCacheAndOptions();

        _api.Setup(api => api.GetCommercialResources())
            .ReturnsAsync(ApiResult.Ok(_commercialResources));

        var service = new CommercialResourcesService(_api.Object, mockCache, options);

        await service.GetResources();

        mockCache.TryGetValue(CacheKey, out var actual);

        var cachedYears = Assert.IsType<CommercialResources[]>(actual);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Category, cachedYears.FirstOrDefault()?.Category);
        Assert.Equal(_commercialResources.FirstOrDefault()?.SubCategory, cachedYears.FirstOrDefault()?.SubCategory);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Title, cachedYears.FirstOrDefault()?.Title);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Url, cachedYears.FirstOrDefault()?.Url);
    }

    [Fact]
    public async Task GetYearsShouldCallApiWhenCacheDoesNotExist()
    {
        var (mockCache, options) = CreateCacheAndOptions();

        _api.Setup(api => api.GetCommercialResources())
            .ReturnsAsync(ApiResult.Ok(_commercialResources));

        var service = new CommercialResourcesService(_api.Object, mockCache, options);

        var actual = await service.GetResources();

        _api.Verify(api => api.GetCommercialResources(), Times.Exactly(1));
        Assert.IsType<CommercialResources[]>(actual);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Category, actual.FirstOrDefault()?.Category);
        Assert.Equal(_commercialResources.FirstOrDefault()?.SubCategory, actual.FirstOrDefault()?.SubCategory);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Title, actual.FirstOrDefault()?.Title);
        Assert.Equal(_commercialResources.FirstOrDefault()?.Url, actual.FirstOrDefault()?.Url);
    }
}