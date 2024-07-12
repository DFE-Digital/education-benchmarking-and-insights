namespace Web.App.ViewModels;

public class StaticCookiesViewModel(string cookieName, bool analyticsCookiesEnabled)
{
    public string CookieName => cookieName;
    public bool AnalyticsCookiesEnabled => analyticsCookiesEnabled;
}