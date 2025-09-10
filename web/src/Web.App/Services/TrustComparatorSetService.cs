using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Services;

public interface ITrustComparatorSetService
{
    Task<UserDefinedTrustComparatorSet> ReadUserDefinedComparatorSet(string companyNumber, string identifier, CancellationToken cancellationToken = default);
    UserDefinedTrustComparatorSet ReadUserDefinedComparatorSetFromSession(string companyNumber);
    UserDefinedTrustComparatorSet SetUserDefinedComparatorSetInSession(string companyNumber, UserDefinedTrustComparatorSet set);
    void ClearUserDefinedComparatorSetFromSession(string companyNumber, string? identifier = null);
    UserDefinedTrustCharacteristicViewModel? ReadUserDefinedCharacteristicFromSession(string companyNumber);
    void SetUserDefinedCharacteristicInSession(string companyNumber, UserDefinedTrustCharacteristicViewModel viewModel);
    void ClearUserDefinedCharacteristicFromSession(string companyNumber);
}

public class TrustComparatorSetService(IHttpContextAccessor httpContextAccessor, IComparatorSetApi api) : ITrustComparatorSetService
{
    public async Task<UserDefinedTrustComparatorSet> ReadUserDefinedComparatorSet(string companyNumber, string identifier, CancellationToken cancellationToken = default) =>
        //Do not add to session state. Locking on session state blocks requests
        await api.GetUserDefinedTrustAsync(companyNumber, identifier, cancellationToken).GetResultOrThrow<UserDefinedTrustComparatorSet>();

    public UserDefinedTrustComparatorSet ReadUserDefinedComparatorSetFromSession(string companyNumber)
    {
        var key = SessionKeys.TrustComparatorSetUserDefined(companyNumber);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<UserDefinedTrustComparatorSet>(key);

        return set ?? SetUserDefinedComparatorSetInSession(companyNumber, new UserDefinedTrustComparatorSet());
    }

    public UserDefinedTrustComparatorSet SetUserDefinedComparatorSetInSession(string companyNumber, UserDefinedTrustComparatorSet set)
    {
        var key = SessionKeys.TrustComparatorSetUserDefined(companyNumber);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Set(key, set);

        return set;
    }

    public void ClearUserDefinedComparatorSetFromSession(string companyNumber, string? identifier = null)
    {
        var key = string.IsNullOrWhiteSpace(identifier)
            ? SessionKeys.TrustComparatorSetUserDefined(companyNumber)
            : SessionKeys.TrustComparatorSetUserDefined(companyNumber, identifier);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Remove(key);
    }

    public UserDefinedTrustCharacteristicViewModel? ReadUserDefinedCharacteristicFromSession(string urn)
    {
        var key = SessionKeys.TrustComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        return context?.Session.Get<UserDefinedTrustCharacteristicViewModel>(key);
    }

    public void SetUserDefinedCharacteristicInSession(string urn, UserDefinedTrustCharacteristicViewModel set)
    {
        var key = SessionKeys.TrustComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Set(key, set);
    }

    public void ClearUserDefinedCharacteristicFromSession(string urn)
    {
        var key = SessionKeys.TrustComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Remove(key);
    }
}