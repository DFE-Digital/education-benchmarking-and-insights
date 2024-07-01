using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Services;

public interface ITrustComparatorSetService
{
    Task<UserDefinedTrustComparatorSet> ReadUserDefinedComparatorSet(string companyNumber, string identifier);
    UserDefinedTrustComparatorSet ReadUserDefinedComparatorSet(string companyNumber);
    UserDefinedTrustComparatorSet SetUserDefinedComparatorSet(string companyNumber, UserDefinedTrustComparatorSet set);
    void ClearUserDefinedComparatorSet(string companyNumber, string? identifier = null);
    UserDefinedTrustCharacteristicViewModel? ReadUserDefinedCharacteristic(string companyNumber);
    void SetUserDefinedCharacteristic(string companyNumber, UserDefinedTrustCharacteristicViewModel viewModel);
    void ClearUserDefinedCharacteristic(string companyNumber);
}

public class TrustComparatorSetService(IHttpContextAccessor httpContextAccessor, IComparatorSetApi api) : ITrustComparatorSetService
{
    public async Task<UserDefinedTrustComparatorSet> ReadUserDefinedComparatorSet(string companyNumber, string identifier)
    {
        //Do not add to session state. Locking on session state blocks requests
        return await api.GetUserDefinedTrustAsync(companyNumber, identifier).GetResultOrThrow<UserDefinedTrustComparatorSet>();
    }

    public UserDefinedTrustComparatorSet ReadUserDefinedComparatorSet(string companyNumber)
    {
        var key = SessionKeys.TrustComparatorSetUserDefined(companyNumber);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<UserDefinedTrustComparatorSet>(key);

        return set ?? SetUserDefinedComparatorSet(companyNumber, new UserDefinedTrustComparatorSet());
    }

    public UserDefinedTrustComparatorSet SetUserDefinedComparatorSet(string companyNumber, UserDefinedTrustComparatorSet set)
    {
        var key = SessionKeys.TrustComparatorSetUserDefined(companyNumber);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Set(key, set);

        return set;
    }

    public void ClearUserDefinedComparatorSet(string companyNumber, string? identifier = null)
    {
        var key = string.IsNullOrWhiteSpace(identifier)
            ? SessionKeys.TrustComparatorSetUserDefined(companyNumber)
            : SessionKeys.TrustComparatorSetUserDefined(companyNumber, identifier);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Remove(key);
    }

    public UserDefinedTrustCharacteristicViewModel? ReadUserDefinedCharacteristic(string urn)
    {
        var key = SessionKeys.TrustComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        return context?.Session.Get<UserDefinedTrustCharacteristicViewModel>(key);
    }

    public void SetUserDefinedCharacteristic(string urn, UserDefinedTrustCharacteristicViewModel set)
    {
        var key = SessionKeys.TrustComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Set(key, set);
    }

    public void ClearUserDefinedCharacteristic(string urn)
    {
        var key = SessionKeys.TrustComparatorSetCharacteristic(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Remove(key);
    }
}