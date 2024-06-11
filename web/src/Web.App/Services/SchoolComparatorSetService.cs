using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Services;

public interface ISchoolComparatorSetService
{
    Task<SchoolComparatorSet> ReadComparatorSet(string urn);
    Task<UserDefinedSchoolComparatorSet> ReadUserDefinedComparatorSet(string urn, string identifier);
    UserDefinedSchoolComparatorSet ReadUserDefinedComparatorSet(string urn);
    UserDefinedSchoolComparatorSet SetUserDefinedComparatorSet(string urn, UserDefinedSchoolComparatorSet set);
    void ClearUserDefinedComparatorSet(string urn, string? identifier = null);
    UserDefinedCharacteristicViewModel? ReadUserDefinedCharacteristic(string urn);
    UserDefinedCharacteristicViewModel SetUserDefinedCharacteristic(string urn, UserDefinedCharacteristicViewModel characteristic);
    void ClearUserDefinedCharacteristic(string urn);
}

public class SchoolComparatorSetService(IHttpContextAccessor httpContextAccessor, IComparatorSetApi api) : ISchoolComparatorSetService
{
    public async Task<SchoolComparatorSet> ReadComparatorSet(string urn)
    {
        var key = SessionKeys.ComparatorSet(urn);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<SchoolComparatorSet>(key);

        return set ?? await SetComparatorSet(urn);
    }

    public async Task<UserDefinedSchoolComparatorSet> ReadUserDefinedComparatorSet(string urn, string identifier)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn, identifier);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<UserDefinedSchoolComparatorSet>(key);

        return set ?? await SetUserDefinedComparatorSet(urn, identifier);
    }

    public void ClearUserDefinedComparatorSet(string urn, string? identifier = null)
    {
        var key = string.IsNullOrWhiteSpace(identifier)
            ? SessionKeys.ComparatorSetUserDefined(urn)
            : SessionKeys.ComparatorSetUserDefined(urn, identifier);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Remove(key);
    }

    public UserDefinedSchoolComparatorSet ReadUserDefinedComparatorSet(string urn)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<UserDefinedSchoolComparatorSet>(key);

        return set ?? SetUserDefinedComparatorSet(urn, new UserDefinedSchoolComparatorSet());
    }

    public UserDefinedSchoolComparatorSet SetUserDefinedComparatorSet(string urn, UserDefinedSchoolComparatorSet set)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Set(key, set);

        return set;
    }

    public UserDefinedCharacteristicViewModel? ReadUserDefinedCharacteristic(string urn)
    {
        var key = SessionKeys.ComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        return context?.Session.Get<UserDefinedCharacteristicViewModel>(key);
    }

    public UserDefinedCharacteristicViewModel SetUserDefinedCharacteristic(string urn, UserDefinedCharacteristicViewModel set)
    {
        var key = SessionKeys.ComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Set(key, set);
        return set;
    }

    public void ClearUserDefinedCharacteristic(string urn)
    {
        var key = SessionKeys.ComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Remove(key);
    }

    private async Task<SchoolComparatorSet> SetComparatorSet(string urn)
    {
        var key = SessionKeys.ComparatorSet(urn);
        var context = httpContextAccessor.HttpContext;

        var set = await api.GetDefaultSchoolAsync(urn).GetResultOrThrow<SchoolComparatorSet>();

        context?.Session.Set(key, set);

        return set;
    }

    private async Task<UserDefinedSchoolComparatorSet> SetUserDefinedComparatorSet(string urn, string identifier)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn, identifier);
        var context = httpContextAccessor.HttpContext;

        var set = await api.GetUserDefinedSchoolAsync(urn, identifier).GetResultOrThrow<UserDefinedSchoolComparatorSet>();
        context?.Session.Set(key, set);

        return set;
    }
}