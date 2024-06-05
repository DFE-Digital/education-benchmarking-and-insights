using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
namespace Web.App.Services;

public interface IComparatorSetService
{
    Task<ComparatorSet> ReadComparatorSet(string urn);
    Task<ComparatorSetUserDefined> ReadUserDefinedComparatorSet(string urn, string identifier);
    ComparatorSetUserDefined ReadUserDefinedComparatorSet(string urn);
    ComparatorSetUserDefined SetUserDefinedComparatorSet(string urn, ComparatorSetUserDefined set);
    void ClearUserDefinedComparatorSet(string urn, string identifier);
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

    public async Task<ComparatorSetUserDefined> ReadUserDefinedComparatorSet(string urn, string identifier)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn, identifier);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<ComparatorSetUserDefined>(key);

        return set ?? await SetUserDefinedComparatorSet(urn, identifier);
    }

    public void ClearUserDefinedComparatorSet(string urn, string identifier)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn, identifier);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Remove(key);
    }

    public ComparatorSetUserDefined ReadUserDefinedComparatorSet(string urn)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<ComparatorSetUserDefined>(key);

        return set ?? SetUserDefinedComparatorSet(urn, new ComparatorSetUserDefined());
    }

    public ComparatorSetUserDefined SetUserDefinedComparatorSet(string urn, ComparatorSetUserDefined set)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Set(key, set);

        return set;
    }

    private async Task<ComparatorSet> SetComparatorSet(string urn)
    {
        var key = SessionKeys.ComparatorSet(urn);
        var context = httpContextAccessor.HttpContext;

        var set = await api.GetDefaultSchoolAsync(urn).GetResultOrThrow<ComparatorSet>();

        context?.Session.Set(key, set);

        return set;
    }

    private async Task<ComparatorSetUserDefined> SetUserDefinedComparatorSet(string urn, string identifier)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn, identifier);
        var context = httpContextAccessor.HttpContext;

        var set = await api.GetUserDefinedSchoolAsync(urn, identifier).GetResultOrThrow<ComparatorSetUserDefined>();
        context?.Session.Set(key, set);

        return set;
    }
}