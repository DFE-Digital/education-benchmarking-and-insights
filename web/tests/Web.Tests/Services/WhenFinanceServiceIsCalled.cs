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
    private readonly FinanceYears _financeYears = new() { Aar = 2023, Cfr = 2023, S251 = 2024 };

    private const string CacheKey = "return-years";

    private static (IMemoryCache mockCache, IOptions<CacheOptions> options) CreateCacheAndOptions()
    {
        var mockCache = new MemoryCache(new MemoryCacheOptions());
        var options = Options.Create(new CacheOptions
        {
            ReturnYears = new CacheSettings { SlidingExpiration = 60, AbsoluteExpiration = 3600 }
        });
        return (mockCache, options);
    }

    [Fact]
    public async Task GetYearsShouldReturnCachedResponseIfItExist()
    {
        var (mockCache, options) = CreateCacheAndOptions();

        mockCache.Set(CacheKey, _financeYears);

        _api.Setup(api => api.GetCurrentReturnYears())
            .ReturnsAsync(ApiResult.Ok(_financeYears));

        var service = new FinanceService(_api.Object, mockCache, options);

        var actual = await service.GetYears();

        _api.Verify(api => api.GetCurrentReturnYears(), Times.Never());
        Assert.IsType<FinanceYears>(actual);
        Assert.Equal(_financeYears.Aar, actual.Aar);
        Assert.Equal(_financeYears.Cfr, actual.Cfr);
        Assert.Equal(_financeYears.S251, actual.S251);
    }

    [Fact]
    public async Task GetYearsShouldSetCacheCorrectlyIfItDoesNotExist()
    {
        var (mockCache, options) = CreateCacheAndOptions();

        _api.Setup(api => api.GetCurrentReturnYears())
            .ReturnsAsync(ApiResult.Ok(_financeYears));

        var service = new FinanceService(_api.Object, mockCache, options);

        await service.GetYears();

        mockCache.TryGetValue(CacheKey, out var actual);

        var cachedYears = Assert.IsType<FinanceYears>(actual);
        Assert.Equal(_financeYears.Aar, cachedYears.Aar);
        Assert.Equal(_financeYears.Cfr, cachedYears.Cfr);
        Assert.Equal(_financeYears.S251, cachedYears.S251);
    }

    [Fact]
    public async Task GetYearsShouldCallApiWhenCacheDoesNotExist()
    {
        var (mockCache, options) = CreateCacheAndOptions();

        _api.Setup(api => api.GetCurrentReturnYears())
            .ReturnsAsync(ApiResult.Ok(_financeYears));

        var service = new FinanceService(_api.Object, mockCache, options);

        var actual = await service.GetYears();

        _api.Verify(api => api.GetCurrentReturnYears(), Times.Exactly(1));
        Assert.IsType<FinanceYears>(actual);
        Assert.Equal(_financeYears.Aar, actual.Aar);
        Assert.Equal(_financeYears.Cfr, actual.Cfr);
        Assert.Equal(_financeYears.S251, actual.S251);
    }
}