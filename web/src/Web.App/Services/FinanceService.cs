using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Web.App.Cache;
using Web.App.Domain;
using Web.App.Domain.Content;
using Web.App.Infrastructure.Apis.Content;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IFinanceService
{
    Task<FinanceYears> GetYears();
}

public class FinanceService(
    IYearsApi yearsApi,
    IMemoryCache memoryCache,
    IOptions<CacheOptions> options) : IFinanceService
{
    private readonly int _sliding = options.Value.ReturnYears.SlidingExpiration ?? 10;
    private readonly int _absolute = options.Value.ReturnYears.AbsoluteExpiration ?? 60;
    private const string CacheKey = "return-years";

    public async Task<FinanceYears> GetYears()
    {
        if (memoryCache.TryGetValue(CacheKey, out var cached) && cached is FinanceYears financeYears)
        {
            return financeYears;
        }

        var data = await yearsApi.GetCurrentReturnYears().GetResultOrThrow<FinanceYears>();

        MemoryCacheEntryOptions cacheEntryOptions = new()
        {
            SlidingExpiration = TimeSpan.FromMinutes(_sliding),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_absolute)
        };

        memoryCache.Set(CacheKey, data, cacheEntryOptions);

        return data;
    }
}
