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

public class WhenFinanceServiceIsCalled
{
    private readonly Mock<IInsightApi> _api = new();
    private readonly FinanceYears _financeYears = new() { Aar = 2023, Cfr = 2023 };
    private readonly CacheOptions _cacheOptions = new()
    {
        CacheKey = "ReturnYearsCache",
        SlidingExpirationInSeconds = 60,
        AbsoluteExpirationInSeconds = 3600
    };

    private (IMemoryCache memoryCache, IOptions<CacheOptions> mockOptions) CreateCacheAndOptions()
    {
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var mockOptions = Options.Create(_cacheOptions);
        return (memoryCache, mockOptions);
    }

    [Fact]
    public async Task GetYearsShouldReturnCachedResponseIfItExist()
    {
        var (mockCache, mockOptions) = CreateCacheAndOptions();

        MemoryCacheEntryOptions cacheEntryOptions = new()
        {
            SlidingExpiration = TimeSpan.FromSeconds(60),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
        };

        mockCache.Set(_cacheOptions.CacheKey!, _financeYears, cacheEntryOptions);

        _api.Setup(api => api.GetCurrentReturnYears())
            .ReturnsAsync(ApiResult.Ok(_financeYears));

        var service = new FinanceService(_api.Object, mockCache, mockOptions);

        var actual = await service.GetYears();

        _api.Verify(api => api.GetCurrentReturnYears(), Times.Exactly(0));
        Assert.IsType<FinanceYears>(actual);
        Assert.Equal(_financeYears.Aar, actual.Aar);
        Assert.Equal(_financeYears.Cfr, actual.Cfr);

    }

    [Fact]
    public async Task GetYearsShouldSetCacheCorrectlyIfItDoesNotExist()
    {
        var (mockCache, mockOptions) = CreateCacheAndOptions();

        _api.Setup(api => api.GetCurrentReturnYears())
            .ReturnsAsync(ApiResult.Ok(_financeYears));

        var service = new FinanceService(_api.Object, mockCache, mockOptions);

        await service.GetYears();

        mockCache.TryGetValue(_cacheOptions.CacheKey!, out var actual);

        var cachedYears = Assert.IsType<FinanceYears>(actual);
        Assert.Equal(_financeYears.Aar, cachedYears.Aar);
        Assert.Equal(_financeYears.Cfr, cachedYears.Cfr);
    }

    [Fact]
    public async Task GetYearsShouldCallApiWhenCacheDoesNotExist()
    {
        var (mockCache, mockOptions) = CreateCacheAndOptions();

        MemoryCacheEntryOptions cacheEntryOptions = new()
        {
            SlidingExpiration = TimeSpan.FromSeconds(60),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
        };

        _api.Setup(api => api.GetCurrentReturnYears())
            .ReturnsAsync(ApiResult.Ok(_financeYears));

        var service = new FinanceService(_api.Object, mockCache, mockOptions);

        var actual = await service.GetYears();

        _api.Verify(api => api.GetCurrentReturnYears(), Times.Exactly(1));
        Assert.IsType<FinanceYears>(actual);
        Assert.Equal(_financeYears.Aar, actual.Aar);
        Assert.Equal(_financeYears.Cfr, actual.Cfr);
    }
}