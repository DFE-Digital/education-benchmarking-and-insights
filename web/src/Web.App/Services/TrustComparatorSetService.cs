using Web.App.Domain;
using Web.App.Extensions;
namespace Web.App.Services;

public interface ITrustComparatorSetService
{
    TrustComparatorSetUserDefined ReadUserDefinedComparatorSet(string companyNumber);
    TrustComparatorSetUserDefined SetUserDefinedComparatorSet(string companyNumber, TrustComparatorSetUserDefined set);
}

public class TrustComparatorSetService(IHttpContextAccessor httpContextAccessor) : ITrustComparatorSetService
{
    public TrustComparatorSetUserDefined ReadUserDefinedComparatorSet(string companyNumber)
    {
        var key = SessionKeys.TrustComparatorSetUserDefined(companyNumber);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<TrustComparatorSetUserDefined>(key);

        return set ?? SetUserDefinedComparatorSet(companyNumber, new TrustComparatorSetUserDefined());
    }

    public TrustComparatorSetUserDefined SetUserDefinedComparatorSet(string companyNumber, TrustComparatorSetUserDefined set)
    {
        var key = SessionKeys.TrustComparatorSetUserDefined(companyNumber);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Set(key, set);

        return set;
    }
}