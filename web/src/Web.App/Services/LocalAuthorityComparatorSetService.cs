using Web.App.Domain;
using Web.App.Extensions;

namespace Web.App.Services;

public interface ILocalAuthorityComparatorSetService
{
    UserDefinedLocalAuthorityComparatorSet ReadUserDefinedComparatorSetFromSession(string code);
    UserDefinedLocalAuthorityComparatorSet SetUserDefinedComparatorSetInSession(string code, UserDefinedLocalAuthorityComparatorSet set);
    void ClearUserDefinedComparatorSetFromSession(string code);
}

public class LocalAuthorityComparatorSetService(IHttpContextAccessor httpContextAccessor) : ILocalAuthorityComparatorSetService
{
    public UserDefinedLocalAuthorityComparatorSet ReadUserDefinedComparatorSetFromSession(string code)
    {
        var key = SessionKeys.LocalAuthorityComparatorSetUserDefined(code);
        var context = httpContextAccessor.HttpContext;

        var set = context?.Session.Get<UserDefinedLocalAuthorityComparatorSet>(key);
        return set ?? SetUserDefinedComparatorSetInSession(code, new UserDefinedLocalAuthorityComparatorSet());
    }

    public void ClearUserDefinedComparatorSetFromSession(string code)
    {
        var key = SessionKeys.LocalAuthorityComparatorSetUserDefined(code);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Remove(key);
    }

    public UserDefinedLocalAuthorityComparatorSet SetUserDefinedComparatorSetInSession(string code, UserDefinedLocalAuthorityComparatorSet set)
    {
        var key = SessionKeys.LocalAuthorityComparatorSetUserDefined(code);
        var context = httpContextAccessor.HttpContext;

        context?.Session.Set(key, set);
        return set;
    }
}