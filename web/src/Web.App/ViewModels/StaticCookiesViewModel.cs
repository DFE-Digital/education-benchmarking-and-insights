namespace Web.App.ViewModels;

public class StaticCookiesViewModel(string cookieName, bool? analyticsCookiesEnabled, bool cookiesSaved)
{
    public string CookieName => cookieName;
    public bool? AnalyticsCookiesEnabled => analyticsCookiesEnabled;
    public bool CookiesSaved => cookiesSaved;
}