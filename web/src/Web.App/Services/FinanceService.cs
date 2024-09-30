using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Web.App.Cache;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
namespace Web.App.Services;

public interface IFinanceService
{
    Task<FinanceYears> GetYears();
}

public class FinanceService(
    IInsightApi insightApi,
    IMemoryCache memoryCache,
    IOptions<CacheOptions> cacheOptions) : IFinanceService
{
    private readonly CacheOptions _cacheOptions = cacheOptions.Value;
    private readonly string _cacheKey = cacheOptions.Value.CacheKey ?? throw new NullReferenceException(nameof(CacheOptions));

    public async Task<FinanceYears> GetYears()
    {
        FinanceYears data;
        if (memoryCache.TryGetValue(_cacheKey, out var cachedData))
        {
            data = cachedData as FinanceYears ?? throw new InvalidOperationException(nameof(FinanceYears));

            return data;
        }

        data = await insightApi.GetCurrentReturnYears().GetResultOrThrow<FinanceYears>();

        MemoryCacheEntryOptions cacheEntryOptions = new()
        {
            SlidingExpiration = TimeSpan.FromSeconds(_cacheOptions.SlidingExpirationInSeconds),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheOptions.AbsoluteExpirationInSeconds)
        };

        memoryCache.Set(_cacheKey, data, cacheEntryOptions);

        return data;
    }
}
