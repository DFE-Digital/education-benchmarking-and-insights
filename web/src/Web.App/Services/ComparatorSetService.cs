using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IComparatorSetService
{
    Task<ComparatorSet> ReadComparatorSet(string urn);
}

public class ComparatorSetService(IHttpContextAccessor httpContextAccessor, IComparatorSetApi api) : IComparatorSetService
{
    public async Task<ComparatorSet> ReadComparatorSet(string urn)
    {
        var key = SessionKeys.ComparatorSet(urn);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<ComparatorSet>(key);

        return set ?? await SetComparatorSet(urn);
    }

    private async Task<ComparatorSet> SetComparatorSet(string urn)
    {
        var key = SessionKeys.ComparatorSet(urn);
        var context = httpContextAccessor.HttpContext;

        var set = await api.GetDefaultAsync(urn).GetResultOrThrow<ComparatorSet>();

        context?.Session.Set(key, set);

        return set;
    }
}