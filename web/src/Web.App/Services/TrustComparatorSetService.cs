using Web.App.Domain;
using Web.App.Extensions;
using Web.App.ViewModels;
namespace Web.App.Services;

public interface ITrustComparatorSetService
{
    UserDefinedTrustComparatorSet ReadUserDefinedComparatorSet(string companyNumber);
    UserDefinedTrustComparatorSet SetUserDefinedComparatorSet(string companyNumber, UserDefinedTrustComparatorSet set);
    UserDefinedTrustCharacteristicViewModel? ReadUserDefinedCharacteristic(string urn);
    void SetUserDefinedCharacteristic(string companyNumber, UserDefinedTrustCharacteristicViewModel viewModel);
    void ClearUserDefinedCharacteristic(string companyNumber);
}

public class TrustComparatorSetService(IHttpContextAccessor httpContextAccessor) : ITrustComparatorSetService
{
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