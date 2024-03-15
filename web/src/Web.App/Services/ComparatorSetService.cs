using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IComparatorSetService
{
    Task<ComparatorSet> ReadDefaultPupilComparatorSet(string urn);
}

public class ComparatorSetService(IHttpContextAccessor httpContextAccessor, IBenchmarkApi benchmarkApi) : IComparatorSetService
{
    public async Task<ComparatorSet> ReadDefaultPupilComparatorSet(string urn)
    {
        var key = DefaultPupilKey(urn);
        var context = httpContextAccessor.HttpContext;
        var set = context?.Session.Get<ComparatorSet>(key);
        if (set == null)
        {
            set = await benchmarkApi.GetDefaultPupilComparatorSet(urn).GetResultOrThrow<ComparatorSet>();
            context?.Session.Set(key, set);
        }

        return set;
    }

    private static string DefaultPupilKey(string urn) => SessionKeys.DefaultPupilComparatorSet(urn);
}