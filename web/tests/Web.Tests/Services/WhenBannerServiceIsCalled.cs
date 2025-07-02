using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Web.App.Cache;
using Web.App.Domain.Content;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Content;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenBannerServiceIsCalled
{
    private readonly Mock<IBannerApi> _bannerApi;
    private readonly Mock<ICacheEntry> _cacheEntry;
    private readonly Fixture _fixture = new();
    private readonly Mock<IMemoryCache> _memoryCache;
    private readonly IOptions<CacheOptions> _options;
    private readonly BannerService _service;

    public WhenBannerServiceIsCalled()
    {
        _bannerApi = new Mock<IBannerApi>();
        _cacheEntry = new Mock<ICacheEntry>();
        _memoryCache = new Mock<IMemoryCache>();
        _memoryCache
            .Setup(c => c.CreateEntry(It.IsAny<string>()))
            .Returns(_cacheEntry.Object);
        _options = Options.Create(new CacheOptions
        {
            Banners = _fixture.Build<CacheSettings>()
                .Without(c => c.Disabled)
                .Create()
        });
        _service = new BannerService(_bannerApi.Object, _memoryCache.Object, _options);
    }

    public static IEnumerable<object?[]> BannerInCacheTestData =>
        new List<object?[]>
        {
            new object?[]
            {
                new Banner { Title = "Title" }
            },
            new object?[]
            {
                null
            }
        };

    public static IEnumerable<object?[]> BannerNotInCacheTestData =>
        new List<object?[]>
        {
            new object?[]
            {
                "Banner__target", new string("Incorrect type")
            },
            new object?[]
            {
                "Incorrect key", new Banner { Title = "Title" }
            }
        };

    [Theory]
    [MemberData(nameof(BannerInCacheTestData))]
    public async Task ShouldGetBannerFromCacheIfPresent(object? banner)
    {
        // arrange
        const string target = nameof(target);

        _memoryCache
            .Setup(c => c.TryGetValue($"Banner__{target}", out banner))
            .Returns(true);

        // act
        var actual = await _service.GetBannerOrDefault(target);

        // assert
        Assert.Equal(banner, actual);
    }

    [Theory]
    [MemberData(nameof(BannerNotInCacheTestData))]
    public async Task ShouldGetBannerFromApiIfNotInCache(string existingCacheKey, object? existingCacheItem)
    {
        // arrange
        const string target = nameof(target);
        var banner = _fixture.Create<Banner>();

        _memoryCache
            .Setup(c => c.TryGetValue(existingCacheKey, out existingCacheItem))
            .Returns(false);

        _bannerApi.Setup(b => b.GetBanner(target)).ReturnsAsync(ApiResult.Ok(banner));

        // act
        var actual = await _service.GetBannerOrDefault(target);

        // assert
        Assert.Equal(banner, actual);
    }

    [Fact]
    public async Task ShouldAddBannerToCacheFromApiIfNotInCache()
    {
        // arrange
        const string target = nameof(target);
        object? nothing = null;
        var banner = _fixture.Create<Banner>();

        _memoryCache
            .Setup(c => c.TryGetValue(It.IsAny<string>(), out nothing))
            .Returns(false);

        _bannerApi.Setup(b => b.GetBanner(target)).ReturnsAsync(ApiResult.Ok(banner));

        // act
        await _service.GetBannerOrDefault(target);

        // assert
        _memoryCache.Verify(c => c.CreateEntry($"Banner__{target}"));
        _cacheEntry.VerifySet(c => c.Value = banner);
        _cacheEntry.VerifySet(c => c.AbsoluteExpirationRelativeToNow = It.Is<TimeSpan>(o => Convert.ToInt32(o.TotalMinutes) == _options.Value.Banners.AbsoluteExpiration));
        _cacheEntry.VerifySet(c => c.SlidingExpiration = It.Is<TimeSpan>(o => Convert.ToInt32(o.TotalMinutes) == _options.Value.Banners.SlidingExpiration));
    }

    [Fact]
    public async Task ShouldAddNullBannerToCacheFromNotFoundApiResultIfNotInCache()
    {
        // arrange
        const string target = nameof(target);
        object? nothing = null;

        _memoryCache
            .Setup(c => c.TryGetValue(It.IsAny<string>(), out nothing))
            .Returns(false);

        _bannerApi.Setup(b => b.GetBanner(target)).ReturnsAsync(ApiResult.NotFound);

        // act
        await _service.GetBannerOrDefault(target);

        // assert
        _memoryCache.Verify(c => c.CreateEntry($"Banner__{target}"));
        _cacheEntry.VerifySet(c => c.Value = null);
        _cacheEntry.VerifySet(c => c.AbsoluteExpirationRelativeToNow = It.Is<TimeSpan>(o => Convert.ToInt32(o.TotalMinutes) == _options.Value.Banners.AbsoluteExpiration));
        _cacheEntry.VerifySet(c => c.SlidingExpiration = It.Is<TimeSpan>(o => Convert.ToInt32(o.TotalMinutes) == _options.Value.Banners.SlidingExpiration));
    }

    [Fact]
    public async Task ShouldNotAddBannerToCacheFromApiIfNotInCacheButCacheDisabled()
    {
        // arrange
        const string target = nameof(target);
        object? nothing = null;
        var banner = _fixture.Create<Banner>();

        _bannerApi.Setup(b => b.GetBanner(target)).ReturnsAsync(ApiResult.Ok(banner));

        var service = new BannerService(_bannerApi.Object, _memoryCache.Object, Options.Create(new CacheOptions
        {
            Banners = new CacheSettings
            {
                Disabled = true
            }
        }));

        // act
        await service.GetBannerOrDefault(target);

        // assert
        _memoryCache.Verify(c => c.TryGetValue(It.IsAny<string>(), out nothing), Times.Never);
        _memoryCache.Verify(c => c.CreateEntry(It.IsAny<string>()), Times.Never);
    }
}