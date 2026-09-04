namespace Web.App.ViewModels.Components;

public class ClarityViewModel(
    bool analyticsCookiesEnabled,
    string? clarityProjectId)
{
    public bool AnalyticsCookiesEnabled => analyticsCookiesEnabled;
    public string? ClarityProjectId => clarityProjectId;
}
