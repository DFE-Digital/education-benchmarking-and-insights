using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Services;

public interface ISchoolComparatorSetService
{
    Task<SchoolComparatorSet?> ReadComparatorSet(string urn);
    Task<SchoolComparatorSet?> ReadComparatorSet(string urn, string identifier);
    Task<UserDefinedSchoolComparatorSet?> ReadUserDefinedComparatorSet(string urn, string identifier);
    UserDefinedSchoolComparatorSet ReadUserDefinedComparatorSetFromSession(string urn);
    UserDefinedSchoolComparatorSet SetUserDefinedComparatorSetInSession(string urn, UserDefinedSchoolComparatorSet set);
    void ClearUserDefinedComparatorSetFromSession(string urn, string? identifier = null);
    UserDefinedSchoolCharacteristicViewModel? ReadUserDefinedCharacteristic(string urn);
    void SetUserDefinedCharacteristic(string urn, UserDefinedSchoolCharacteristicViewModel characteristic);
    void ClearUserDefinedCharacteristic(string urn);
}

public class SchoolComparatorSetService(IHttpContextAccessor httpContextAccessor, IComparatorSetApi api) : ISchoolComparatorSetService
{
    public async Task<SchoolComparatorSet?> ReadComparatorSet(string urn) =>
        //Do not add to session state. Locking on session state blocks requests
        await api.GetDefaultSchoolAsync(urn).GetResultOrDefault<SchoolComparatorSet>();

    public async Task<SchoolComparatorSet?> ReadComparatorSet(string urn, string identifier) =>
        //Do not add to session state. Locking on session state blocks requests
        await api.GetCustomSchoolAsync(urn, identifier).GetResultOrDefault<SchoolComparatorSet>();

    public async Task<UserDefinedSchoolComparatorSet?> ReadUserDefinedComparatorSet(string urn, string identifier) =>
        //Do not add to session state. Locking on session state blocks requests
        await api.GetUserDefinedSchoolAsync(urn, identifier).GetResultOrDefault<UserDefinedSchoolComparatorSet>();

    public UserDefinedSchoolComparatorSet ReadUserDefinedComparatorSetFromSession(string urn)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<UserDefinedSchoolComparatorSet>(key);

        return set ?? SetUserDefinedComparatorSetInSession(urn, new UserDefinedSchoolComparatorSet());
    }

    public void ClearUserDefinedComparatorSetFromSession(string urn, string? identifier = null)
    {
        var key = string.IsNullOrWhiteSpace(identifier)
            ? SessionKeys.ComparatorSetUserDefined(urn)
            : SessionKeys.ComparatorSetUserDefined(urn, identifier);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Remove(key);
    }

    public UserDefinedSchoolComparatorSet SetUserDefinedComparatorSetInSession(string urn, UserDefinedSchoolComparatorSet set)
    {
        var key = SessionKeys.ComparatorSetUserDefined(urn);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Set(key, set);

        return set;
    }

    public UserDefinedSchoolCharacteristicViewModel? ReadUserDefinedCharacteristic(string urn)
    {
        var key = SessionKeys.ComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        return context?.Session.Get<UserDefinedSchoolCharacteristicViewModel>(key);
    }

    public void SetUserDefinedCharacteristic(string urn, UserDefinedSchoolCharacteristicViewModel set)
    {
        var key = SessionKeys.ComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Set(key, set);
    }

    public void ClearUserDefinedCharacteristic(string urn)
    {
        var key = SessionKeys.ComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Remove(key);
    }
}