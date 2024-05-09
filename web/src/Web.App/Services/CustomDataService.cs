using Web.App.Domain;
using Web.App.Extensions;

namespace Web.App.Services;

public interface ICustomDataService
{
    CustomData? GetCustomData(string urn);
    void SetCustomData(string urn, CustomData data);
    
    // todo: clear data method
}

public class CustomDataService(IHttpContextAccessor httpContextAccessor) : ICustomDataService
{
    public CustomData? GetCustomData(string urn)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        return context?.Session.Get<CustomData>(key);
    }

    public void SetCustomData(string urn, CustomData data)
    {
        var key = SessionKeys.CustomData(urn);
        var context = httpContextAccessor.HttpContext;
        context?.Session.Set(key, data);
    }
}