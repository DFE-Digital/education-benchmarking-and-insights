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

public class WhenCommercialResourcesServiceGetsCategoryLinks
{
    private const string Category1 = "category1";
    private const string Category2 = "category2";
    private const string SubCategory1 = "subCategory1";
    private const string SubCategory2 = "subCategory2";
    private readonly Mock<ICacheEntry> _cacheEntry;
    private readonly Mock<ICommercialResourcesApi> _commercialResourcesApi;
    private readonly Fixture _fixture = new();
    private readonly Mock<IMemoryCache> _memoryCache;
    private readonly IOptions<CacheOptions> _options;
    private readonly CommercialResourceCategorised _resource1;
    private readonly CommercialResourceCategorised _resource2;
    private readonly CommercialResourceCategorised _resource3;
    private readonly CommercialResourceCategorised[] _resources;
    private readonly CommercialResourcesService _service;

    public WhenCommercialResourcesServiceGetsCategoryLinks()
    {
        _commercialResourcesApi = new Mock<ICommercialResourcesApi>();
        _cacheEntry = new Mock<ICacheEntry>();
        _memoryCache = new Mock<IMemoryCache>();
        _memoryCache
            .Setup(c => c.CreateEntry(It.IsAny<string>()))
            .Returns(_cacheEntry.Object);
        _options = Options.Create(new CacheOptions
        {
            CommercialResources = _fixture.Create<CacheSettings>()
        });
        _service = new CommercialResourcesService(_commercialResourcesApi.Object, _memoryCache.Object, _options);

        _resource1 = new CommercialResourceCategorised
        {
            Title = "Title1",
            Url = "Url1",
            Category = new CategoryCollection
            {
                Items = [Category1, Category2]
            },
            SubCategory = new CategoryCollection
            {
                Items = []
            }
        };
        _resource2 = new CommercialResourceCategorised
        {
            Title = "Title2",
            Url = "Url2",
            Category = new CategoryCollection
            {
                Items = [Category1]
            },
            SubCategory = new CategoryCollection
            {
                Items = [SubCategory1]
            }
        };
        _resource3 = new CommercialResourceCategorised
        {
            Title = "Title3",
            Url = "Url3",
            Category = new CategoryCollection
            {
                Items = []
            },
            SubCategory = new CategoryCollection
            {
                Items = [SubCategory1, SubCategory2]
            }
        };
        _resources =
        [
            _resource1,
            _resource2,
            _resource3
        ];
    }

    [Fact]
    public async Task ShouldGetAndProjectResourcesFromCacheIfPresentWhenGettingCategoryLinks()
    {
        // arrange
        object? resources = _resources;
        var expected = new Dictionary<string, CommercialResourceLink[]>
        {
            {
                Category1, [_resource1, _resource2]
            },
            {
                Category2, [_resource1]
            }
        };

        _memoryCache
            .Setup(c => c.TryGetValue("commercial-resources", out resources))
            .Returns(true);

        // act
        var actual = await _service.GetCategoryLinks();

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ShouldGetAndProjectResourcesFromApiIfNotInCacheWhenGettingCategoryLinks()
    {
        // arrange
        object? nothing = null;
        var expected = new Dictionary<string, CommercialResourceLink[]>
        {
            {
                Category1, [_resource1, _resource2]
            },
            {
                Category2, [_resource1]
            }
        };

        _memoryCache
            .Setup(c => c.TryGetValue("commercial-resources", out nothing))
            .Returns(false);

        _commercialResourcesApi.Setup(b => b.GetCommercialResources()).ReturnsAsync(ApiResult.Ok(_resources));

        // act
        var actual = await _service.GetCategoryLinks();

        // assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public async Task ShouldAddResourcesToCacheFromApiIfNotInCacheWhenGettingCategoryLinks()
    {
        // arrange
        object? nothing = null;

        _memoryCache
            .Setup(c => c.TryGetValue(It.IsAny<string>(), out nothing))
            .Returns(false);

        _commercialResourcesApi.Setup(b => b.GetCommercialResources()).ReturnsAsync(ApiResult.Ok(_resources));

        // act
        await _service.GetCategoryLinks();

        // assert
        _memoryCache.Verify(c => c.CreateEntry("commercial-resources"));
        _cacheEntry.VerifySet(c => c.Value = It.Is<CommercialResourceCategorised[]>(o => o.Length == _resources.Length));
        _cacheEntry.VerifySet(c => c.AbsoluteExpirationRelativeToNow = It.Is<TimeSpan>(o => Convert.ToInt32(o.TotalMinutes) == _options.Value.CommercialResources.AbsoluteExpiration));
        _cacheEntry.VerifySet(c => c.SlidingExpiration = It.Is<TimeSpan>(o => Convert.ToInt32(o.TotalMinutes) == _options.Value.CommercialResources.SlidingExpiration));
    }

    [Fact]
    public async Task ShouldGetAndProjectResourcesFromCacheIfPresentWhenGettingSubCategoryLinks()
    {
        // arrange
        object? resources = _resources;
        var expected = new Dictionary<string, CommercialResourceLink[]>
        {
            {
                SubCategory1, [_resource2, _resource3]
            },
            {
                SubCategory2, [_resource3]
            }
        };

        _memoryCache
            .Setup(c => c.TryGetValue("commercial-resources", out resources))
            .Returns(true);

        // act
        var actual = await _service.GetSubCategoryLinks();

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ShouldGetAndProjectResourcesFromApiIfNotInCacheWhenGettingSubCategoryLinks()
    {
        // arrange
        object? nothing = null;
        var expected = new Dictionary<string, CommercialResourceLink[]>
        {
            {
                SubCategory1, [_resource2, _resource3]
            },
            {
                SubCategory2, [_resource3]
            }
        };

        _memoryCache
            .Setup(c => c.TryGetValue("commercial-resources", out nothing))
            .Returns(false);

        _commercialResourcesApi.Setup(b => b.GetCommercialResources()).ReturnsAsync(ApiResult.Ok(_resources));

        // act
        var actual = await _service.GetSubCategoryLinks();

        // assert
        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public async Task ShouldAddResourcesToCacheFromApiIfNotInCacheWhenGettingSubCategoryLinks()
    {
        // arrange
        object? nothing = null;

        _memoryCache
            .Setup(c => c.TryGetValue(It.IsAny<string>(), out nothing))
            .Returns(false);

        _commercialResourcesApi.Setup(b => b.GetCommercialResources()).ReturnsAsync(ApiResult.Ok(_resources));

        // act
        await _service.GetSubCategoryLinks();

        // assert
        _memoryCache.Verify(c => c.CreateEntry("commercial-resources"));
        _cacheEntry.VerifySet(c => c.Value = It.Is<CommercialResourceCategorised[]>(o => o.Length == _resources.Length));
        _cacheEntry.VerifySet(c => c.AbsoluteExpirationRelativeToNow = It.Is<TimeSpan>(o => Convert.ToInt32(o.TotalMinutes) == _options.Value.CommercialResources.AbsoluteExpiration));
        _cacheEntry.VerifySet(c => c.SlidingExpiration = It.Is<TimeSpan>(o => Convert.ToInt32(o.TotalMinutes) == _options.Value.CommercialResources.SlidingExpiration));
    }
}